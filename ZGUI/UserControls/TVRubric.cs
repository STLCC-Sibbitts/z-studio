
#region Using directives
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Configuration;
using ZLib.ZRubric;
#endregion



namespace ZGUI
{
	public partial class TVRubric : TreeView
	{
		#region properties
		public string documentFileName;
		private ZRubric m_zRubric;
		private ZPropertiesControl m_zProps;
		private TabControl m_tcDetails;
		private ZStepControl m_zStepControl;
		#endregion
		void rubric_onPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			TreeNode currentNode = this.tvJson.SelectedNode;

		}

		public TVRubric( )
		{
			InitializeComponent();
			m_zProps = null;
			m_zStepControl = null;
			m_tcDetails = null;
			tvJson.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvJson_AfterSelect);
		}
		private void tvJson_AfterSelect(object sender, TreeViewEventArgs e)
		{
			Debug.Print("Selected tvJson node");
			m_zProps.zToken = e.Node.Tag as ZToken;
			// populate zStepControl
			ZStep zstep = null;
			// ZTask zTask = null;
			// ZScenario zScenario = null;
			string nodeType = m_zProps.zToken.GetType().ToString();
			m_tcDetails.Show();
			if (nodeType == typeof(ZStep).ToString())
			{
				m_tcDetails.SelectTab("tabStep");
				zstep = m_zProps.zToken as ZStep;
				m_zStepControl.zStep = zstep;
			}
			else if (nodeType == typeof(ZTask).ToString())
			{
				m_tcDetails.SelectTab("tabTask");
			}
			else if (nodeType == typeof(ZScenario).ToString())
			{
				m_tcDetails.SelectTab("tabScenario");
			}
			else
			{
				m_tcDetails.Hide();
			}
			// m_oRubric.PropertyChanged += new PropertyChangedEventHandler(rubric_onPropertyChanged);

		}
		public ZPropertiesControl zProps
		{
			get { return m_zProps; }
			set
			{
				m_zProps = value;
			}
		}
		public TabControl tcDetails
		{
			get { return m_tcDetails; }
			set { m_tcDetails = value; }
		}
		public ZStepControl zStep
		{
			get { return m_zStepControl; }
			set { m_zStepControl = value; }
		}
		public ZRubric zRubric
		{
			get { return m_zRubric; }
			set 
			{
				tvJson.Nodes.Clear();	// clear whatever was here before
				m_zRubric = value;
				if (value == null)
					return;
				string rubricName = m_zRubric.project.name;
				TreeNode tnRoot = tvJson.Nodes.Add("Project - " + rubricName);
				tnRoot.Name = rubricName;
				tnRoot.Tag = m_zRubric;
				TreeNode tnProperties = tnRoot.Nodes.Add("Properties");
				TreeNode tnPreferences = tnRoot.Nodes.Add("Preferences");
				TreeNode tnSteps = tnRoot.Nodes.Add("Steps");
				bool stepsLoaded = LoadSteps(tnSteps);
				tvJson.ExpandAll();
				TreeNode tnUserPreferences = tvJson.Nodes.Add("User Preferences");
			}
		}
		private bool LoadSteps(TreeNode tnRoot)
		{
			// not going to use separate folder for steps and tasks or scenarios now
//			TreeNode tnSteps = AddFolder( tnRoot, zRubric.steps);
			TreeNode tnStep = null;
			TreeNode tnTask = null;
			// TreeNode tnTasks = null, tnScenarios = null;
			string jPath = "";

			foreach (ZStep step in zRubric.steps )
			{
				tnStep = AddNode(tnRoot, step);
				jPath = step.jPath;
				Debug.WriteLine("Steps:" + jPath);
				ZTasks tasks = step.tasks;
//				tnTasks = AddFolder(tnStep, tasks) ;

				foreach (ZTask task in tasks)
				{
					tnTask = AddNode(tnStep, task);

					ZScenarios scenarios = task.zScenarios;
//					tnScenarios = AddFolder(tnTask, scenarios);
					foreach (ZScenario scenario in scenarios)
					{
						jPath = scenario.jPath;
						Debug.WriteLine("Scenario:" + jPath + " = " + scenario.name);
						TreeNode tnScenario = AddNode(tnTask, scenario);
					}
				}
			}
			return true;
		}
		private TreeNode AddNode(TreeNode tn, ZStep step)
		{
			TreeNode tNode = tn.Nodes.Add(step.name + ") " + step.text);
			tNode.Tag = step;
			return tNode;
		}
		private TreeNode AddNode(TreeNode tn, ZTask task)
		{
			string	id = task.id;
			TreeNode tNode = tn.Nodes.Add(task.text);
			tNode.Tag = task;
			return tNode;
		}
		private TreeNode AddNode(TreeNode tn, ZScenario scenario)
		{
			TreeNode tNode = tn.Nodes.Add(scenario.name);
			tNode.Tag = scenario;
			return tNode;
		}

		private void RefreshNodeProperties(TreeNode tn)
		{
			ZToken obj = tn.Tag as ZToken;

		}
		private TreeNode AddFolder(TreeNode tn, ZToken obj)
		{
			string name = obj.name;
			TreeNode tNode = tn.Nodes.Add(name);
			tNode.Name = name;
			tNode.Tag = obj;
			return tNode;
		}

		#region old stuff
		//private TreeNode AddTextNode(TreeNode tn, JToken tok)
		//{
		//    return AddValueNode(tn,tok,"Text");
		//}
		//private TreeNode AddTextNode(TreeNode tn, ZObject obj)
		//{
		//    return AddValueNode(tn, obj, "Text");
		//}
		//private TreeNode AddValueNode(TreeNode tn, ZObject zObject, string name)
		//{
		//    TreeNode valNode = null;
		//    JValue jVal = zObject[name];
		//    if (jVal != null)
		//    {
		//        valNode = tn.Nodes.Add(name + ":" + jVal.Value<string>());
		//        valNode.Tag = jVal;
		//    }
		//    return valNode;
		//}

		//private TreeNode AddValueNode(TreeNode tn, JToken tok, string name)
		//{
		//    TreeNode valNode = null;
		//    JValue jVal = tok.Value<JValue>(name);
		//    if (jVal != null)
		//    {
		//        valNode = tn.Nodes.Add(name + ":" + jVal.Value<string>());
		//        valNode.Tag = jVal;
		//    }
		//    return valNode;
		//}
		//private TreeNode AddArrayNode(TreeNode tn, JToken tok, string name, out JToken oTok)
		//{
		//    oTok = tok[name];
		//    TreeNode oNode = tn.Nodes.Add(name);
		//    oNode.Name = name;
		//    oNode.Tag = tok;
		//    return oNode;
		//}
		//// tok.Value<string>(name)
		//private TreeNode AddObjectNode(TreeNode tn, JToken tok, string name, JToken oTok)
		//{
		//    TreeNode oNode = tn.Nodes.Add(name);
		//    oNode.Name = name;
		//    oNode.Tag = tok;
		//    return oNode;
		//}
		//private TreeNode AddObjectNode(TreeNode tn, JToken tok, string name)
		//{
		//    JToken oTok = tok[name];
		//    string tokName = tok.Value<string>(name);
		//    return AddObjectNode(tn, tok, tokName, oTok);
		//}
		//private TreeNode AddNode(TreeNode tn, JToken tok, string name)
		//{
		//    TreeNode tNode = tn.Nodes.Add(name);
		//    tNode.Name = name;
		//    tNode.Tag = tok;
		//    return tNode;
		//}
		
		//private void AddAnswers(TreeNode tnScenario, ZScenario zScenario)
		//{
		//    //JToken answers = null;
		//    //TreeNode tnAnswers = AddArrayNode(tnScenario, tokScenario, "Answer", out answers);
		//    //if (answers.Type == JTokenType.Array)
		//    //{
		//    //    int ansIndex = 0;
		//    //    foreach (JToken answer in answers.Children())
		//    //    {
		//    //        AddAnswer(tnAnswers,answer,++ansIndex);
		//    //        string jPath = ans GetJPath(answer);
		//    //    }
		//    //}
		//    //else
		//    //{
		//    //    AddAnswer(tnAnswers, answers);
		//    //}
		//}
		//private TreeNode AddListNode(TreeNode tnList, int listIndex)
		//{
		//    return tnList.Nodes.Add("[" + listIndex.ToString() + "]");
		//}

		//private TreeNode AddListNodeValue(TreeNode tnList, int listIndex, JToken value)
		//{
		//    TreeNode tnListNodeValue = tnList.Nodes.Add("[" + listIndex.ToString() + "]" + value.Value<string>());
		//    tnListNodeValue.Tag = value;
		//    return tnListNodeValue;
		//}
		//private TreeNode AddNodeValue(TreeNode tnList, JToken value)
		//{
		//    TreeNode tnNodeValue = tnList.Nodes.Add( value.Value<string>());
		//    tnNodeValue.Tag = value;
		//    return tnNodeValue;
		//}

		//private void AddAnswer(TreeNode tnAnswers, JToken tokAnswer)
		//{
		//    TreeNode tnAnswer = AddObjectNode(tnAnswers, tokAnswer, "Path");
		//    AddAnswerValues(tnAnswer, tokAnswer);
		//}
		//private void AddAnswer(TreeNode tnAnswers, JToken tokAnswer, int answerIndex)
		//{
		//    TreeNode tnAnswerListNode = AddListNode(tnAnswers,answerIndex);
		//    TreeNode tnAnswer = AddObjectNode(tnAnswerListNode, tokAnswer, "Path");
		//    AddAnswerValues(tnAnswer, tokAnswer);
		//}

		//private void AddAnswerValues(TreeNode tnAnswer, JToken tokAnswer)
		//{
		//    JToken values = null;				
		//    TreeNode tnValues = AddArrayNode(tnAnswer, tokAnswer, "Value", out values);
		//    if ( values.Type == JTokenType.Array)
		//    {
		//        int valIndex = 0;
		//        foreach(JToken value in values.Children())
		//        {
		//            AddAnswerValue(tnValues, value,++valIndex);
		//        }
		//    }
		//    else
		//    {
		//        AddAnswerValue(tnValues, values );
		//    }
		//}
		//private void AddAnswerValue(TreeNode tnAnswerValues, JToken tokAnswerValue)
		//{
		//    TreeNode tnAnswer = AddNodeValue(tnAnswerValues, tokAnswerValue);
		//}
		//private void AddAnswerValue(TreeNode tnAnswerValues, JToken tokAnswerValue, int answerValueIndex)
		//{
		//    TreeNode tnAnswer = AddListNodeValue(tnAnswerValues, answerValueIndex, tokAnswerValue);
		//}
		#endregion
	}
}
