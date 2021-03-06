﻿// This is the first version
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using ZLib;
using ZLib.ZRubric;
using ZGUI;
using Excel2Json;
//using Grader;
using System.Reflection;
using System.Linq.Expressions;

#if false

{
	"Name":"7a",
	"Text":"In cell F20, enter an IF function that tests whether the order quantity in cell E20 is greater than 0(zero). If it is, return the value of cell E20 multiplied by cell D20; otherwise, return no text by entering \"\"",
	"Location":
	{	
		"Type":"Cell",
		"Worksheets":"Order Form", 
		"Cells":"$F$20",
		"Path":"Worksheets[Order Form].Cells[$F$20]" 
	},
	"Type":"Content",	
	"Required":true,					
	"Property":"Formula",
	"Expression":"=IF(E20>0,D20*E20,\"\")"
	"Value":45.50,
	"Scenarios": 
	[
		{
			"Name":"MissingFalse",
			"Deduct":40,
			"Custom":false
			"Type":"Technical",
			"Feedback":"false condition omitted, review IF",
			"Answer":
			{
				"Expression":"=IF({^Expression(1)},{^Expression(2)})"
			}
		}
	]
}


{
		"Name":"7a",
		"Text":"In cell F20, enter an IF function that tests whether the order quantity in cell E20 is greater than 0(zero). If it is, return the value of cell E20 multiplied by cell D20; otherwise, return no text by entering \"\"",
		"Location":
		{	
			"Type":"Cell",
			"Worksheets":"Order Form", 
			"Cells":"$F$20",
			"Path":"Worksheets[Order Form].Cells[$F$20]" 
		},
		"Type":"Content",	
		"Required":true,					
		"Property":"Formula",
		"Expression":"=IF(E20>0,D20*E20,\"\")"
		"Value":45.50,
		"Scenarios": 
		[
			{
				"Name":"MissingFalse",
				"Deduct":40,
				"Custom":false
				"Type":"Technical",
				"Feedback":"false condition omitted, review IF",
				"Answer":
				{
					"Expression":"=IF({^Expression(1)},{^Expression(2)})"
				}
			},
			{	
				"Name":"NoIf",
				"Deduct":80,
				"Custom":false
				"Type":"Conceptual",
				"Feedback":"Need to use IF, not just enter formula...",
				"Answer":
				{
					"Expression":"={^Expression(2)}"
				}
			},
			{	
				"Name":"EnteredValue",
				"Deduct":100,
				"Custom":false
				"Type":"Conceptual.Literal",
				"Feedback":"Never enter the value...",
				"Answer":
				{
					"Value":"{^Value}"
				}
			}
			{	
				"Name":"PartialCredit", 
				"Deduct":20,
				"Custom":true
				"Type":"Technical",
				"Text":"testing price instead of quantity",
				"Credit":"Partial", 
				"Feedback":"results are correct, but expression tests the price instead of the order quantity",
				"Answer":
				{
					"Expression":"=IF(D20>0,D20*E20,\"\")"
				}
			},
		]
	}
#endif
//Input: "This is some sample text" 
//Match: "This is some sample text"
//   Group 0: This is some sample text - pos:0/len:24
//	  Capture 0: This is some sample text - pos:0/len:24]
//   Group 1:  - pos:0/len:0
//   Group 2:  - pos:0/len:0
//   Group 3:  - pos:0/len:0
//   Group 4:  - pos:0/len:0
//   Group 5:  - pos:0/len:0
//======================================
//Input: "This is {some} sample text" 
//Match: "This is {some} sample text"
//   Group 0: This is {some} sample text - pos:0/len:26
//	  Capture 0: This is {some} sample text - pos:0/len:26]
//   Group 1: {some} sample text - pos:8/len:18
//	  Capture 0: {some} sample text - pos:8/len:18]
//   Group 2: {some - pos:8/len:5
//	  Capture 0: {some - pos:8/len:5]
//   Group 3: } sample text - pos:13/len:13
//	  Capture 0: } sample text - pos:13/len:13]
//   Group 4:  - pos:0/len:0
//   Group 5: some - pos:9/len:4
//	  Capture 0: some - pos:9/len:4]
//======================================
//Input: "This is {so{me}} sample text" 
//Match: "This is {so{me}} sample text"
//   Group 0: This is {so{me}} sample text - pos:0/len:28
//	  Capture 0: This is {so{me}} sample text - pos:0/len:28]
//   Group 1: {so{me}} sample text - pos:8/len:20
//	  Capture 0: {so{me}} sample text - pos:8/len:20]
//   Group 2: {me - pos:11/len:3
//	  Capture 0: {so - pos:8/len:3]
//	  Capture 1: {me - pos:11/len:3]
//   Group 3: } sample text - pos:15/len:13
//	  Capture 0: } - pos:14/len:1]
//	  Capture 1: } sample text - pos:15/len:13]
//   Group 4:  - pos:0/len:0
//   Group 5: so{me} - pos:9/len:6
//	  Capture 0: me - pos:12/len:2]
//	  Capture 1: so{me} - pos:9/len:6]
//======================================
//Input: "This is {so{me}} {sam{ple}} text" 
//Match: "This is {so{me}} {sam{ple}} text"
//   Group 0: This is {so{me}} {sam{ple}} text - pos:0/len:32
//	  Capture 0: This is {so{me}} {sam{ple}} text - pos:0/len:32]
//   Group 1: {sam{ple}} text - pos:17/len:15
//	  Capture 0: {so{me}}  - pos:8/len:9]
//	  Capture 1: {sam{ple}} text - pos:17/len:15]
//   Group 2: {ple - pos:21/len:4
//	  Capture 0: {so - pos:8/len:3]
//	  Capture 1: {me - pos:11/len:3]
//	  Capture 2: {sam - pos:17/len:4]
//	  Capture 3: {ple - pos:21/len:4]
//   Group 3: } text - pos:26/len:6
//	  Capture 0: } - pos:14/len:1]
//	  Capture 1: }  - pos:15/len:2]
//	  Capture 2: } - pos:25/len:1]
//	  Capture 3: } text - pos:26/len:6]
//   Group 4:  - pos:0/len:0
//   Group 5: sam{ple} - pos:18/len:8
//	  Capture 0: me - pos:12/len:2]
//	  Capture 1: so{me} - pos:9/len:6]
//	  Capture 2: ple - pos:22/len:3]
//	  Capture 3: sam{ple} - pos:18/len:8]
//======================================
//Input: "This is {so{me}} {sam{p{l}e}} text" 
//Match: "This is {so{me}} {sam{p{l}e}} text"
//   Group 0: This is {so{me}} {sam{p{l}e}} text - pos:0/len:34
//	  Capture 0: This is {so{me}} {sam{p{l}e}} text - pos:0/len:34]
//   Group 1: {sam{p{l}e}} text - pos:17/len:17
//	  Capture 0: {so{me}}  - pos:8/len:9]
//	  Capture 1: {sam{p{l}e}} text - pos:17/len:17]
//   Group 2: {l - pos:23/len:2
//	  Capture 0: {so - pos:8/len:3]
//	  Capture 1: {me - pos:11/len:3]
//	  Capture 2: {sam - pos:17/len:4]
//	  Capture 3: {p - pos:21/len:2]
//	  Capture 4: {l - pos:23/len:2]
//   Group 3: } text - pos:28/len:6
//	  Capture 0: } - pos:14/len:1]
//	  Capture 1: }  - pos:15/len:2]
//	  Capture 2: }e - pos:25/len:2]
//	  Capture 3: } - pos:27/len:1]
//	  Capture 4: } text - pos:28/len:6]
//   Group 4:  - pos:0/len:0
//   Group 5: sam{p{l}e} - pos:18/len:10
//	  Capture 0: me - pos:12/len:2]
//	  Capture 1: so{me} - pos:9/len:6]
//	  Capture 2: l - pos:24/len:1]
//	  Capture 3: p{l}e - pos:22/len:5]
//	  Capture 4: sam{p{l}e} - pos:18/len:10]
//======================================
namespace TestStuff
{
	public partial class frmTester : Form
	{
		static string cellPattern = @"(\$?[A-Za-z]{1,3}\$?[1-9]{1}[0-9]*)|(R[1-9]{1}[0-9]*C[1-9]{1}[0-9]*)|(R(\[-?[1-9]{1}[0-9]*\])?C(\[-?[1-9]{1}[0-9]*\])?)";
		// reg expr to isolate variables embedded in strings
		static string exprVarPattern = @"^[^{}]*(((?'Open'{)[^{}]*)+((?'Close-Open'})[^{}]*)+)*(?(Open)(?!))$";
		public enum testingStuff { First, second, third };
		public frmTester()
		{
			InitializeComponent();
			txtStep.Select(5, 5);
			txtStep.SelectionColor = Color.Red;
			txtStep.DeselectAll();
			testingStuff ts = testingStuff.second;
			string tester = testingStuff.First.ToString();
			tester = ts.ToString();
            // C:\src\data\NordellEx2IP4\testCases\valuesText\threshold2
            // initialize the tvTestCases
            TreeNode tn1;
            foreach (string dir1 in Directory.EnumerateDirectories(@"H:\src\data\NordellEx2IP4\testCases"))
            {
                string pn = Directory.GetParent(dir1).FullName + @"\";
                string fn = dir1.Replace(pn, "");
                tn1 = tvTestCases.Nodes.Add(fn);
                tn1.Tag = dir1;
                PopulateNode(tn1);
            }
            ZRubric.LoadFunctions();
            ZRubric.LoadPreferences();
			ZRubric.LoadOntologies();
			ZOntology excelOntology = ZRubric.activeOntologies["Excel2013"];
			ZObjectives objectives = excelOntology.objectives;

			ZObjective objective = objectives["EX281"];
			ZResources resources = ZRubric.activePreferences.resources;
			ZResourceProviders rps = excelOntology.resourceProviders;
			// lets see what we have for this objective
			ZResourceProvider rp = null;
			ZObjectiveMappings oms = null;
			foreach (ZResource res in resources)
			{
				string name = res.id;
				rp = rps[0];	// this finds the resource provider stuff for the current resource
				rp = rps[name];
				if (rp != null)
				{
					oms = rp.objectiveMappings;
					// TODO: handle multiple mappings for same id, like EX281
					List<ZObjectiveMapping> omList = oms[objective.id];
					foreach (ZObjectiveMapping om in omList)
					{
						ZObjectiveResources ors = rp.Resources(om.type);
						string omName = om.name;
						ZObjectiveResource or = ors[om.name];
						int index = or.index;
					}
				}
			}

			

		}
        private void PopulateNode(TreeNode  treeNode)
        {
            TreeNode tn1 = null;
            foreach (string dir1 in Directory.EnumerateDirectories(treeNode.Tag.ToString()))
            {
                string pn = Directory.GetParent(dir1).FullName + @"\";
                string fn = dir1.Replace(pn, "");
                tn1 = treeNode.Nodes.Add(fn);
                tn1.Tag = dir1;
                PopulateNode(tn1);
            }
            // only tag as eligible node if no subdirectories
            if (tn1 == null && File.Exists(treeNode.Tag.ToString() + @"\rubric.json"))
            {
                // flag this as an eligible node
                treeNode.Text += "*";
            }
        }
		public void ProcessTestCase(TreeNode tn)
		{
			if (tn.Text.EndsWith("*"))
			{
				// by default, grade it,
				if ( radReportIt.Checked )
					DoReportIt(tn.Tag as string);
				else
					DoGradeIt(tn.Tag as string);
			}
			else
				MessageBox.Show("Testcase not implemented at this time.");

		}
        private void tvTestCases_AfterSelect(object sender, TreeViewEventArgs e)
        {
			ProcessTestCase(e.Node);
        }

#region regex
		public class RXFields
		{
			public const string Parm			= "parm";
			public const string Index			= "index";
			public const string VarName			= "varName";
			public const string ElementName		= "elementName";
			public const string ParentVarName	= "parentVarName";
			public const string PathRef			= "pathRef";
		}
		static string parmPattern			= @"(\((?<"  + RXFields.Parm			+ @">\d+)\)?)";
		static string indexPattern			= @"(\[(?<"  + RXFields.Index			+ @">[\w\d_]+)\]?)";
		static string varNamePattern		= @"(^(?<"   + RXFields.VarName			+ @">[^\^\/\*\(\[]+))";
		static string elementNamePattern	= @"(\.(?<"  + RXFields.ElementName		+ @">[\w\d_]+))";
		static string parentVarNamePattern  = @"(^\^(?<" + RXFields.ParentVarName	+ @">[^\^\*\(\[]+))";
		static string pathRefPattern		= @"(^\/(?<" + RXFields.PathRef			+ @">[^\^\*\(\[]+))";

		static string stepTaskPattern		= @"(?'preText'[^\[]*)(\[\{(?'key'[^\}]*)\})(?'text'[^\]]*)\](?'postText'[^\[]*)";
		static string stepTagPattern		= @"(?'preText'[^<]*)(<\{(?'key'[^\}]*)\})(?'text'[^>]*)>(?'postText'[^<]*)";

		public string DoRegExpr(string theExpr, string taskPattern = "", string tagPattern="")
		{
			// use square brackets for tasks, with tags nested inside
			string results = "";
			// test resolving nested 
#if true
			txtOut.Text = "";
			//Match m = Regex.Match(theExpr, pattern);
			//Match m = Regex.Match(theExpr, pattern);
			int	taskNum = 0;
			int	taskStart	= 0;
			int	taskLen		= 0;
			foreach (Match taskMatch in Regex.Matches(theExpr, taskPattern))
			{
				string taskKey		= taskMatch.Groups["taskKey"].Value;
				string taskValue	= taskMatch.Groups["taskValue"].Value;
				string preTaskText	= taskMatch.Groups["preTaskText"].Value;
				string postTaskText	= taskMatch.Groups["postTaskText"].Value;
				Group taskGroup		= taskMatch.Groups["taskValue"];
				results				+= preTaskText;
				taskStart			= results.Length;
				string text = String.Format("taskNum[{0}]: key='{1}', pos={2}", taskNum, taskKey, taskStart); 
				txtOut.Text += Environment.NewLine + text;
				int		tagNum		= 0;
				int		tagStart	= 0;
				int		tagLen		= 0;
				foreach (Match tagMatch in Regex.Matches(taskValue, tagPattern))
				{
					string tagKey		= tagMatch.Groups["tagKey"].Value;
					string tagValue		= tagMatch.Groups["taggedText"].Value;
					string preTagText	= tagMatch.Groups["preTagText"].Value;
					string postTagText	= tagMatch.Groups["postTagText"].Value;
					results				+= preTagText;
					tagStart			= results.Length;
					tagLen				= tagValue.Length;
					results				+= tagValue + postTagText;
					text = String.Format("    tagNum[{0}]: key='{1}', value='{2}', pos={3}, len={4}", tagNum++, tagKey, tagValue, taskStart, tagLen);
					txtOut.Text += Environment.NewLine + text;
				}
				taskLen				= results.Length - taskStart;
				taskValue			= results.Substring(taskStart);
				text = String.Format("taskNum[{0}]: key='{1}', pos={2}, len={3}{4}{5}", taskNum++, taskKey, taskStart, taskLen, Environment.NewLine, taskValue);
				txtOut.Text += Environment.NewLine + text;
				results				+= postTaskText;
			}
			txtOut.Text += Environment.NewLine + "========================================================================";
			txtOut.Text += Environment.NewLine + results;


#endif
#if false
			Match m = Regex.Match(theExpr, exprVarPattern);
			if (m.Success)
			{
				txtOut.Text += String.Format("Input: \"{0}\" \nMatch: \"{1}\"", theExpr, m) + Environment.NewLine;
				int grpCtr = 0;
				foreach (Group grp in m.Groups)
				{
					txtOut.Text += String.Format("   Group {0}: {1} - pos:{2}/len:{3}", 
						grpCtr, grp.Value, grp.Index, grp.Length) + Environment.NewLine;
					grpCtr++;
					int capCtr = 0;
					foreach (Capture cap in grp.Captures)
					{
						txtOut.Text += String.Format("      Capture {0}: {1} - pos:{2}/len:{3}]", 
							capCtr, cap.Value, cap.Index.ToString("D"), cap.Length.ToString("D")) + Environment.NewLine;
						capCtr++;
					}
				}

			}
			else
			{
				txtOut.Text += String.Format("Match failed.") + Environment.NewLine;
			}
			txtOut.Text += "======================================" + Environment.NewLine;
#endif
			return results;
		}
#endregion
		public bool ZLiteralDeltaHandler(ZExprDeltaEventArgs zExprDeltaEventArgs)
		{
//			ZPreference zPref =  rubric.project.preferences.content["Literal"]["Typos"];
			bool allowed = false;
			//allowed = true;
			string tagValue = "";
			ZExprNode foundNode = zExprDeltaEventArgs.zExprDelta.rExprNode;
			ZExprNode expectedNode = zExprDeltaEventArgs.zExprDelta.lExprNode;
			if (expectedNode.isTagged)
			{
				tagValue = expectedNode.tag;
				if (expectedNode.isLiteral)
				{
					// now we need to check to see the distance should be checked
					int	levDist = expectedNode.text.LevenshteinDistance(foundNode.text);
					string soundsLikeDiffs =  SoundsLike.diffs;
					decimal levDistPct = expectedNode.text.LevDistPct(foundNode.text);
					if ( levDist <= 2 || levDistPct <= .10M )
						allowed = true;
					txtOut.Text += string.Format("expectedText: [{3}], foundText: [{4}], levDist {0}, " 
						+ "levDistPct: [{1:n2}], allowed: [{2}]" + Environment.NewLine,
							levDist, levDistPct, allowed, expectedNode.text, foundNode.text);
				}
				else if (foundNode.isFunctionArg)
				{
					ZExprNode zFunNode = foundNode.parent;
				}
			}
			zExprDeltaEventArgs.zExprDelta.Allowed = allowed;

			return allowed;
		}

		public bool ZExprDeltaHandler(ZExprDeltaEventArgs zExprDeltaEventArgs)
		{
			bool allowed = false;
			//allowed = true;
			string tagValue = "";
			ZExprNode badNode = zExprDeltaEventArgs.zExprDelta.lExprNode;
			if (badNode.isTagged)
			{
				tagValue = badNode.tag;
				if (badNode.isFunction)
				{
				}
				else if (badNode.isFunctionArg)
				{
					ZExprNode zFunNode = badNode.parent;
				}
			}
			zExprDeltaEventArgs.zExprDelta.Allowed = allowed;
			
			return allowed;
		}
        private void DoFormula(string formula)
		{
			string lExprString = "", rExprString = "";
			bool	equivalent = false;
			string []tags = this.txtRubric.Text.Split('\n');
			string []taggedExpression = this.txtStep.Text.Split('\n');
			ZRubric.LoadFunctions();	// make sure these are loaded
			if (taggedExpression.Length > 0 && tags.Length > 0)
			{
				ZExprNode taggedExprNode = ZExpr.Parse(taggedExpression, tags);
			}
			else
			{
				ZExprNode formNode = ZExpr.Parse(formula);
			}
			// see if we are doing a comparison, ie there are two lines
			if (formula.Contains("\n"))
			{
				// initialize the delta handler
				ZExpr.ExprDeltaHandler = ZExprDeltaHandler;
				lExprString = formula.Substring(0,formula.IndexOf("\n"));
				rExprString = formula.Substring(formula.IndexOf("\n")+1);
				ZExpr lExpr = null;
				
				if (taggedExpression.Length > 0 && tags.Length > 0)
					lExpr = new ZExpr(lExprString, tags);
				else
					lExpr = new ZExpr(lExprString);

				ZExpr rExpr = new ZExpr(rExprString);
				equivalent = (lExpr == rExpr);

				return;
			}

			ZExpr zExpr = new ZExpr(formula);
			string t = zExpr.expression;
			ZExprNode zNode = zExpr.rootNode;
			int nodeCounter = 0;
			txtOut.Text += String.Format("Input: '{0}', isExpression:{1}", formula, zNode.isExpression.ToString()) + Environment.NewLine;

			// here's some code to process the function args
#if false
					// grab function args list and use it to iterate through the possible arguments
					ZFunction zFunction = ZRubric.ZRubric.activeFunctions[lNode.text];
					// iterate through each of the children and make sure they are equivalent
					// iterate through the entire list
					// TODO: create dictionary to link instructions to various elements, eventually use guids, for now, encoded text
					// use the position of the child to grab the appropriate function arg
					ZFunctionArg	zFunArg = null;
					ZExprNode		defaultFunArgNode = null, lFunArgNode = null, rFunArgNode = null;
					int				functionArgCount = zFunction.functionArgs.Count;
					for (int funArgIdx = 0; funArgIdx < functionArgCount; ++funArgIdx)
					{
						// we can do some additional checking here now
						zFunArg = zFunction.functionArgs[funArgIdx];
						defaultFunArgNode = zFunArg.defaultExprNode;
#endif
			ZFunction zFunction = null;
			ZFunctionArg zFunArg = null;
			ZExprNode defaultFunArgNode = null, lFunArgNode = null, rFunArgNode = null;
			int functionArgCount = -1;

			SortedDictionary<int,string> exprNodes = new SortedDictionary<int,string>();

			foreach (ZExprNode zExprNode in zNode.children)
			{
				if ( zExprNode.nodeType == ZExprNode.ZNodeType.zFunction)
				{
					zFunction = ZRubric.activeFunctions[zExprNode.text];
					if (zFunction != null)
						functionArgCount = zFunction.functionArgs.Count;
				}
				else
					zFunction = null;
				txtOut.Text += String.Format("   Node{0}: text:{1}, type:{2}",
					nodeCounter++, zExprNode.text, zExprNode.nodeType.ToString()) + Environment.NewLine;
				exprNodes.Add(zExprNode.position, zExprNode.text);
				int childCounter = 0;
				string childText = "";
				foreach (ZExprNode childNode in zExprNode.children)
				{
					txtOut.Text += String.Format("      Node{0}: text:{1}, type:{2}/{3:D}, {4}:'{5}', pos:{6}",
						childCounter, childNode.text, childNode.nodeType.ToString(), childNode.nodeType,
						(childNode.isExpression?"expr":"literal"),childNode.expression, childNode.position) + Environment.NewLine;
					childText = childNode.text;
					if (zFunction != null)
					{
						zFunArg = zFunction.functionArgs[childCounter];
						childText = zFunArg.name + ":" + childText;
					}

					exprNodes.Add(childNode.position, childText);
					++childCounter;
				}
				for ( childCounter = 0; childCounter < exprNodes.Count; ++childCounter)
				{
					txtOut.Text += String.Format("      Node{0}: position:{1}, text:{2}",
						childCounter, exprNodes.Keys.ElementAt(childCounter).ToString(),
						exprNodes.Values.ElementAt(childCounter).ToString())  + Environment.NewLine; 

				}
			}
			txtOut.Text += "======================================" + Environment.NewLine;
		}
#if false
{
	"Location":
	{	
		"Type":"{^ParentName}",
		"Worksheets":"Order Form", 
		"Cells":"$F$20",
		"Path":"Worksheets[Order Form].Cells[$F$20]" 
	}
}


#endif
		private void DoLevenshtein(string strings)
		{
			string lExprString = "", rExprString = "";
			bool equivalent = false;
			string[] tags = this.txtRubric.Text.Split('\n');
			string[] taggedExpression = this.txtStep.Text.Split('\n');
			//if (taggedExpression.Length > 0 && tags.Length > 0)
			//{
			//	ZExprNode taggedExprNode = ZExpr.Parse(taggedExpression, tags);
			//}
			//else
			//{
			//	ZExprNode formNode = ZExpr.Parse(strings);
			//}
			// see if we are doing a comparison, ie there are two lines
			if (strings.Contains("\n"))
			{
				//string functionsPath = @"..\..\..\data\Functions.json";
				//ZRubric.LoadFunctions( functionsPath );
				//functionsPath =  @"h:\z-studio\_ZStudio\data\Functions.json";
				//// initialize the delta handler
				ZExpr.ExprDeltaHandler = ZLiteralDeltaHandler;
				lExprString = strings.Substring(0, strings.IndexOf("\n"));
				rExprString = strings.Substring(strings.IndexOf("\n")+1);
				ZExpr lExpr = null;

				if (taggedExpression.Length > 0 && tags.Length > 0)
					lExpr = new ZExpr(lExprString, tags);
				else
					lExpr = new ZExpr(lExprString);

				ZExpr rExpr = new ZExpr(rExprString);
				equivalent = (lExpr == rExpr);

				return;
			}
		}
		// TODO: when matching expressions and checking scenarios, consider "{*}" as a wildcard
		private void DoRubric(string rubric)
		{
			JToken	tokRubric = JToken.Parse(rubric);
			JObject		oRoot = tokRubric as JObject;
			JToken		jTokKeys = oRoot.SelectToken("Keys");
			JProperty	propKey = null;
			JProperty	propKeys = null;
			JObject		jKey = null;
			JArray		keyArray = null;
			JObject		keyObj = null;
			string		keyDir = @"H:\z-studio\_ZStudio\data\NordellEx2IP4\keys";
#if false
			// if we don't find the keys here, add them
			if (jTokKeys == null)
			{
				oRoot.Add(new JProperty("Keys",new JArray()));
			}
			keyArray = oRoot.SelectToken("Keys") as JArray;
			string	doc = "";
			string	keyKey = "";
			foreach (string filename in Directory.EnumerateFiles(keyDir, "*.json"))
			{
				keyKey = filename.Substring(0,filename.LastIndexOf('.')).Substring(filename.LastIndexOf('\\')+1);
				using (StreamReader infile = new StreamReader(filename))
				{
					doc = infile.ReadToEnd();
				}
				keyObj = JToken.Parse(doc) as JObject;
				propKey = new JProperty(keyKey,keyObj);
				jKey = new JObject();
				jKey.Add(propKey);
				keyArray.Add(jKey);
			}

			// try it now
			jTokKeys = oRoot.SelectToken("Keys[03.Beg]");
			jTokKeys = oRoot.SelectToken("Keys[03.Beg].Worksheets[Employees].Cells[$I$4].Content.Formula");
			if (jTokKeys != null)
			{
				//jTokKeys = (jTokKeys as JProperty);
				//jKey = (jTokKeys as JObject);
				txtOut.Text += (jTokKeys as JValue).Value.ToString() + Environment.NewLine;
			}
#endif
			// Let's see if we can parse this as a rubric
			ZProject zProj = null;
			ZRubric zRubric = new ZRubric(tokRubric);
			zProj = ZRubric.activeProject;
			JProperty jProp = tokRubric.Children().ElementAt(0) as JProperty;
			ZLocation zLoc = null;
			ZTask task = null;
			if (jProp.Name == "a")
			{
				task = new ZTask((JObject)jProp.Value);
				ZScenario scenario = task.zScenarios[0];
				ZAnswer answer = scenario.answers[0];
				string val = answer.expression;
				txtOut.Text += String.Format("   answer.expression:{0}, scenario:{1}",
						val, scenario.name) + Environment.NewLine;
			}
			else if ( jProp.Name == ZProject.itemTag )
			{
				ZSteps steps = new ZSteps((JArray)oRoot.SelectToken(ZSteps.itemTag));
				// TODO: when dumping the rubric to Excel for a report, include the mapping and deduction text values as well
				// TODO: add ZAction
				// TODO: preferences for typos
				// TODO: preferences for entering value in lieu of expression
				// TODO_now: use template excel file when dumping/reporting rubric
				// TODO: add remediation "key" to ontology based on:
				//			Location.Type
				//			Target/Source.Type
				//			Target/Source.Property
				// TODO_now: figure out way to pinpoint target when it relates to an element/argument of an expression
				//			Mapping.Action
				//			Answer. ** Add different types of answers
				//				Expression
				//					Type
				//						Basic Formula - cell references
				//						Basic Function
				//							Args
				//						Formula/Function - Function/Formula
				//					Before
				//		basically extract attributes that can be used to query (SPARQL) against 
				//			learning objectives
				//			different remediation resources
				//			
				foreach (ZStep step in steps)
				{
					txtOut.Text += step.jPath + ":pts:" + step.pts + " - " + step.text + Environment.NewLine;
					foreach (ZTask zTask in step.tasks)
					{
						txtOut.Text += zTask.jPath + ":pts:" + zTask.pts + " - " + zTask.text  + Environment.NewLine;
						txtOut.Text += "    " + "answer: " + Environment.NewLine;
						txtOut.Text += "        " + "type: " + zTask.answer.type  + Environment.NewLine;
						txtOut.Text += "        " + "literalValue: " + zTask.answer.value + Environment.NewLine;
						txtOut.Text += "        " + "expression: " + zTask.answer.expression + Environment.NewLine;
						foreach (string pref in zTask.answer.preferenceOptions)
						{
							txtOut.Text += "    prefOpt" + pref + Environment.NewLine;
						}
						//foreach (ZArg arg in zTask.args)
						//{
						//	txtOut.Text += arg.jPath + " - " + arg.text  + Environment.NewLine;
						//	txtOut.Text += "        " + arg.expression + Environment.NewLine;
						//	foreach (string pref in arg.preferenceOptions)
						//	{
						//		txtOut.Text += "        prefOpt" + pref + Environment.NewLine;
						//	}
						//}
						foreach (ZScenario scenario in zTask.zScenarios)
						{
							txtOut.Text += scenario.jPath + ":deductPts:" + scenario.deduct + " - " + scenario.name  + Environment.NewLine;
							foreach (ZAnswer answer in scenario.answers)
							{
								txtOut.Text += "    " + "answer: " + Environment.NewLine;
								txtOut.Text += "        " + "type: " + answer.type  + Environment.NewLine;
								txtOut.Text += "        " + "literalValue: " + answer.value + Environment.NewLine;
								txtOut.Text += "        " + "expression: " + answer.expression + Environment.NewLine;
							}
						}
					}
				}
				// if ( jProp.Name == ZLocation.Tags.Item )
				zProj = new ZProject(jProp.Value as JToken);
				{
					string txt = zProj.type;
					txtOut.Text += String.Format("   type:{0}, name:{1}",
							txt, zProj.name) + Environment.NewLine;
					ZMultipliers zms = zProj.multipliers;
//					double ro = zms.difficulties.easy;
//					ro = (zProj.allocations["LO"].ppe * zms[ZDifficulties.itemTag, "Easy"]) * (1 - zms[ZDeductions.itemTag, "Minor"]);
//					ro = zms.coverages.normal;
					//double ro = zms.RO2LO;
					//double easy = zProj.multipliers.difficulties.easy; //  zms.difficulties.factor(ZMultiplier.Difficulties.Easy);
//					double ncePct = zProj.allocations.NCE.pct;
				}
			}
			else if (jProp.Name == ZLocation.itemTag )
			{
					// if ( jProp.Name == ZLocation.Tags.Item )
					zLoc = new ZLocation(jProp.Value as JObject);
					{
						string txt = zLoc.type;
						txtOut.Text += String.Format("   type:{0}, path:{1}",
								txt, zLoc.path) + Environment.NewLine;
					}
			}
			else if ( jProp.Name == ZTasks.itemTag )
			{
					JArray jArray = jProp.Value as JArray;
					ZTasks zTasks = new ZTasks(jArray);
					int	taskCtr = 0;
					foreach (ZTask zTask in zTasks)
					{
						string txt = zTask.target.property;
						txtOut.Text += String.Format("   Node{0}: text:{1}, type:{2}, path:{3}",
								taskCtr++, txt, ZTasks.itemTag, zTask.target.location.path) + Environment.NewLine;
					}
			}

			//ZExpr zExpr = new ZExpr(formula);
			//ZExprNode zNode = zExpr.rootNode;
			//int nodeCounter = 0;
			//txtOut.Text += String.Format("Input: '{0}', isExpression:{1}", formula, zNode.isExpression.ToString()) + Environment.NewLine;
			//foreach (ZExprNode zExprNode in zNode.children)
			//{
			//	txtOut.Text += String.Format("   Node{0}: text:{1}, type:{2}",
			//		nodeCounter++, zExprNode.text, zExprNode.nodeType.ToString()) + Environment.NewLine;
			//	int childCounter = 0;
			//	foreach (ZExprNode childNode in zExprNode.children)
			//	{
			//		txtOut.Text += String.Format("      Node{0}: text:{1}, type:{2}/{3:D}, {4}:'{5}'",
			//			childCounter++, childNode.text, childNode.nodeType.ToString(), childNode.nodeType,
			//			(childNode.isExpression?"expr":"literal"), childNode.expression) + Environment.NewLine;
			//	}
			//}
			txtOut.Text += "======================================" + Environment.NewLine;
		}
		public class RXRangeFields
		{
			public const string SheetName		= "sheetName";
			public const string AbsoluteColumn	= "absCol";
			public const string ColumnName		= "colName";
			public const string AbsoluteRow		= "absRow";
			public const string RowNumber		= "rowNumber";
		}
		static string sheetNamePattern = @"((^(?<" + RXRangeFields.SheetName + @">[\w\d_]+)\!)?)";
		static string absoluteColumnPattern = @"((?<" + RXRangeFields.AbsoluteColumn + @">\$)?)";
		static string columnNamePattern = @"((?<" + RXRangeFields.ColumnName + @">[A-Za-z]+))";
		static string absoluteRowPattern = @"((?<" + RXRangeFields.AbsoluteRow + @">\$)?)";
		static string rowNumberPattern = @"((?<" + RXRangeFields.RowNumber + @">\d+))";

		private void DoCellRange(string cellRange)
		{
			string results = cellRange;
			// test resolving nested 
			ZCellRef	cellRef = null;
			ZRangeRef	rangeRef = null;
			if (ZRangeRef.TryParse(cellRange, out rangeRef))
			{
				txtOut.Text += String.Format("Match success. Found range: '") + rangeRef.ToString() + "'" 
					+ ", with " + rangeRef.Count + " cells in it."
					+ Environment.NewLine;
				foreach ( ZCellRef cr in rangeRef )
				{
					txtOut.Text += String.Format("  cell: '") + cr.ToAbsolute().ToString() + "'" + Environment.NewLine;
				}
			}
			else if (ZCellRef.TryParse(cellRange, out cellRef))
				txtOut.Text += String.Format("Match success. Found cell: '") + cellRef.ToString() + "'" + Environment.NewLine;


			//Match m = Regex.Match(cellRange, sheetNamePattern 
			//			+ absoluteColumnPattern 
			//			+ columnNamePattern
			//			+ absoluteRowPattern
			//			+ rowNumberPattern);
			//if (m.Success)
			//{
			//	string sheetName		= m.Groups[RXRangeFields.SheetName].Value;
			//	string absoluteColumn	= m.Groups[RXRangeFields.AbsoluteColumn].Value;
			//	string columnName		= m.Groups[RXRangeFields.ColumnName].Value;
			//	string absoluteRow		= m.Groups[RXRangeFields.AbsoluteRow].Value;
			//	string rowNumber		= m.Groups[RXRangeFields.RowNumber].Value;
			//	int	colIndex = 0;
			//	foreach (char ch in columnName.ToUpper().ToCharArray() )
			//	{
			//		colIndex = (colIndex * 26) + (ch - 'A' + 1);
			//	}

				// if we have more, we can call this again
			//if ( cellRange.Length > m.Length )
			//{
			//	results = cellRange.Substring(m.Length);
			//	if ( results.StartsWith(":") )
			//		DoCellRange(results.Substring(1));
			//}

				//txtOut.Text += String.Format("Input: '{0}', {1}: '{2}', index: '{3}', parm: '{4}'", theExpr,
			//	(vName.Length > 0 ? RXFields.VarName : (parentName.Length > 0 ? RXFields.ParentVarName : 
			//	(contentName.Length > 0 ? RXFields.ContentVarName : "unknown"))),
			//	(vName.Length > 0 ? vName: (parentName.Length > 0 ? parentName : 
			//	(element.Length > 0 ? element: (element.Length > 0 ? element : 
			//	(contentName.Length > 0 ? contentName : "unknown"))))),

//					iVal, parmVal ) + Environment.NewLine;
			//			}
			else
			{
				txtOut.Text += String.Format("Match failed.") + Environment.NewLine;
			}
			txtOut.Text += "======================================" + Environment.NewLine;

		}
		private void cmdParse_Click(object sender, EventArgs e)
		{
			if (radFormula.Checked)
				DoFormula(txtStep.Text);
			else if (radRubric.Checked)
				DoRubric(txtRubric.Text);
			else if (radCellRange.Checked)
				DoCellRange(txtStep.Text);
            else if (radGradeIt.Checked)
            {			
				txtRubric.Text = @"H:\src\data\NordellEx2IP4";
				if (tvTestCases.SelectedNode is TreeNode)
					txtRubric.Text += @"\testCases\" + tvTestCases.SelectedNode.FullPath.Replace("*","");

                DoGradeIt(txtRubric.Text);
            }
			else if (radReportIt.Checked)
			{
				txtRubric.Text = @"H:\src\data\NordellEx2IP4";
				if (tvTestCases.SelectedNode is TreeNode)
					txtRubric.Text += @"\testCases\" + tvTestCases.SelectedNode.FullPath.Replace("*", "");

				DoReportIt(txtRubric.Text);
			}
			else if (radDumpRange.Checked)
				DumpRange();
			else if (radLeven.Checked)
				DoLevenshtein(txtStep.Text);
			else if (radStepTags.Checked)
			{
				string[] taggedSteps = txtStep.Text.Split('\n');
				locs = new ZStepLocs(txtStep.Text);
				txtRubric.Text = locs.stepText;
				// Color[] tagColors = { Color.Red, Color.Green, Color.DarkBlue, Color.Orange, Color.DarkViolet, Color.DarkOliveGreen, Color.DarkGoldenrod };
				// color code the step text, just like Excel, also grey or bold the text for this task
				Font taggedFont = new Font(txtStep.Font, FontStyle.Bold | FontStyle.Underline);
				foreach (ZStepLoc stepLoc in locs.Values)
				{
					txtRubric.Select(stepLoc.startPos, stepLoc.length);
					txtRubric.SelectionFont = taggedFont;
					txtRubric.SelectionColor = stepLoc.color;
					txtRubric.DeselectAll();
				}
			}
			else
			{
				string[] taggedSteps	= txtStep.Text.Split('\n');
				string taskPattern		= taggedSteps[0];
				string tagPattern		= taggedSteps[1];
				string expr				= taggedSteps[2];
				for (int taggedIndex = 3; taggedIndex < taggedSteps.Length; ++taggedIndex)
				{
					expr += "\n" + taggedSteps[taggedIndex];
				}
				DoRegExpr(expr, taskPattern, tagPattern);
			}

		}
		private ZStepLocs locs;

		private void DumpRange()
		{
			ZExcelToJson zexcel = new ZExcelToJson();
			txtOut.Text += zexcel.SelectionToJson();
			txtOut.Text += "----------------------------------------------";
		}
		#region gradeit
		private void DoReportIt(string currentFolder)
		{
			DialogResult markupSubmission = System.Windows.Forms.DialogResult.Yes; // No;
			ZExcelToJson zexcel = null;
//			if (currentFolder.Length == 0)
			{
				zexcel = new ZExcelToJson();
//				markupSubmission = MessageBox.Show("Markup current Excel file?", "Markup submission", MessageBoxButtons.YesNo);
				// load the rubric for this submission folder
				currentFolder = zexcel.currentFolder;
			}
			ZRubric rubric = new ZRubric(currentFolder);
			rubric.LoadSubmission(currentFolder + @"\gradedSubmission.json");

			// report basics in output window
			// TODO: provide option to export
			double deduct;
			string t;
			// let's see if we can figure out how much needs to be deducted
			double grade = ZRubric.activeProject.totalPts;
			foreach (ZTaskDeduction taskDeduction in ZRubric.activeSubmission.taskDeductions)
			{
				deduct = taskDeduction.deduct;
				t = string.Format("\ntask:'{0}', pts:{1:N2}, errorType:'{6}/{7}', name:'{2}', deduction:{3:N2}, ptsDeducted:{4:N2}, feedback:'{5}'\n",
						taskDeduction.task.name + taskDeduction.task.text, 
						taskDeduction.task.pts, 
						taskDeduction.scenario.name,
						taskDeduction.deduct,
					taskDeduction.pointsDeducted,
					taskDeduction.scenario.remediation.feedback,
					taskDeduction.task.mapping.category, taskDeduction.scenario.deduction.type);
				txtOut.Text += t;
				t = "Workbook: '" + taskDeduction.task.target.location.context + ", address: '" + taskDeduction.task.target.location.address + "\n"; 
				ZStep theStep = rubric.steps[taskDeduction.stepID];
				string testString = theStep.text;
				grade -= taskDeduction.pointsDeducted;

			}
			txtOut.Text += string.Format("Grade is {0:N2} out of {1:N2}\n", grade, ZRubric.activeProject.totalPts);
			txtOut.Text += "==============================================================\n";

			if (zexcel != null && markupSubmission == System.Windows.Forms.DialogResult.Yes)
			{
				// TODO: start going through task deductions and marking up the excel submission
				zexcel.MarkupSubmission(rubric, ZRubric.activeSubmission.taskDeductions);
			}

		}
		// This will attach to the currently opened excel file and grade it
		private void DoGradeIt(string currentFolder )
		{
            DialogResult refreshSubmission = System.Windows.Forms.DialogResult.No;
            ZExcelToJson zexcel = null;
            if (currentFolder.Length == 0)
            {
                zexcel = new ZExcelToJson();
                refreshSubmission = MessageBox.Show("Refresh submission from current Excel file?", "Refresh submission", MessageBoxButtons.YesNo);
                // load the rubric for this submission folder
                currentFolder = zexcel.currentFolder;
            }
            
            ZRubric rubric = new ZRubric(currentFolder);
			//XmlDocument xmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode(rubric.rubric);
			//xmlDoc.Save(zexcel.currentFolder + @"\project.xml");
#if false
			ZStep firstStep = rubric.steps[0];
			ZTags tags = firstStep.tags;
			ZTag tag = tags[1];
			string strTag = tag[2];
			strTag = tags[1][2];
			ZTask firstTask = firstStep.tasks[0];
			string taggedExpr = firstTask.answer.taggedExpression;
			string[] stepTags = new string[tags.Count];
			int	tagCount = 0;
			foreach ( ZTag stepTag in tags )
			{
				stepTags[tagCount++] = stepTag[-1];	// returns the entire tag 
			}
			ZExprNode taggedExprNode = ZExpr.Parse(taggedExpr,stepTags);
			ZFunction zFunction = ZRubric.activeFunctions["IF"];
			foreach (ZFunctionArg functionArg in zFunction.functionArgs)
			{
				string id = functionArg.id;
				string theType = functionArg.type;
				string text = functionArg.text;
				bool reqd = functionArg.required;
				bool outputVal = functionArg.outputValue;
				text = functionArg.notes;
			}
			ZPreference zPref = rubric.project.preferences.content.expression.functionArg_DefaultValue;
			zPref = rubric.project.preferences.content.expression["FunctionArg_Range"];
			zPref = rubric.project.preferences.content["Action"]["FunctionArg_Range"];
			zPref = rubric.project.preferences.content["Literal"]["Typos"];
			ZFunctionArg valueIfTrue = zFunction.functionArgs["Value_if_true"];
			string whatever = valueIfTrue.defaultValue;
			ZExprNode dfltNode = valueIfTrue.defaultExprNode;
			whatever = valueIfTrue.id;
#endif
			if (rubric.LoadKeys())
			{
				if (zexcel != null && refreshSubmission == System.Windows.Forms.DialogResult.Yes)
				{
					string doc = zexcel.SaveSubmission();
					rubric.submission = doc;
				}
				else
					rubric.LoadSubmission(currentFolder + @"\submission.json");
			}
			//ZPreferences preferences = ZRubric.activePreferences;
			//ZPreference option = preferences.partialCredit;
			//bool enabled = preferences.partialCredit.enabled;


#if false
			//foreach (ZStep step in rubric.steps)
			//{
			//	txtOut.Text += step.jPath + ":pts:" + step.pts + " - " + step.text + Environment.NewLine;
			//	foreach (ZTask zTask in step.tasks)
			//	{
			//		txtOut.Text += zTask.jPath + ":pts:" + zTask.pts + " - " + zTask.text  + Environment.NewLine;
			//		txtOut.Text += "    " + "answer: " + Environment.NewLine;
			//		txtOut.Text += "        " + "type: " + zTask.answer.type  + Environment.NewLine;
			//		txtOut.Text += "        " + "literalValue: " + zTask.answer.literalValue + Environment.NewLine;
			//		txtOut.Text += "        " + "expression: " + zTask.answer.expression + Environment.NewLine;
			//		foreach (string pref in zTask.answer.preferenceOptions)
			//		{
			//			txtOut.Text += "    prefOpt" + pref + Environment.NewLine;
			//		}
			//		foreach (ZArg arg in zTask.args)
			//		{
			//			txtOut.Text += arg.jPath + " - " + arg.text  + Environment.NewLine;
			//			txtOut.Text += "        " + arg.expression + Environment.NewLine;
			//			foreach (string pref in arg.preferenceOptions)
			//			{
			//				txtOut.Text += "        prefOpt" + pref + Environment.NewLine;
			//			}
			//		}
			//		//foreach (ZScenario scenario in zTask.zScenarios)
			//		//{
			//		//	txtOut.Text += scenario.jPath + ":deductPts:" + scenario.deduct + " - " + scenario.name  + Environment.NewLine;
			//		//	foreach (ZAnswer answer in scenario.answers)
			//		//	{
			//		//		txtOut.Text += "    " + "answer: " + Environment.NewLine;
			//		//		txtOut.Text += "        " + "type: " + answer.type  + Environment.NewLine;
			//		//		txtOut.Text += "        " + "literalValue: " + answer.literalValue + Environment.NewLine;
			//		//		txtOut.Text += "        " + "expression: " + answer.expression + Environment.NewLine;
			//		//	}
			//		//}
			//	}
			//}
#endif

			double grade = rubric.project.totalPts;
			rubric.GradeSubmission();
			string t = string.Format("allocations - NCE:{0:N2}, EE:{1:N2}, LO:'{2:N2}'\n"
				,ZRubric.activeProject.allocations.NCE.max
				,ZRubric.activeProject.allocations.EE.max
				,ZRubric.activeProject.allocations.LO.max);
			txtOut.Text += t;
            double deduct;
			// setup ontology related stuff
			string ontologyName = ZRubric.activeProject.ontology;
			ZOntology excelOntology = ZRubric.activeOntologies[ontologyName];
			ZResources resources = ZRubric.activePreferences.resources;
			ZResourceProviders rps = excelOntology.resourceProviders;
			// lets see what we have for this objective
			ZResourceProvider rp = null;
			ZObjectiveMappings oms = null;
			foreach (ZResource res in resources)
			{
				string name = res.id;
				rp = rps[0];	// this finds the resource provider stuff for the current resource
				rp = rps[name];
				if (rp != null)
				{
					oms = rp.objectiveMappings;
					// TODO: handle multiple mappings for same id, like EX281
					List<ZObjectiveMapping> omList = oms[""]; //objective.id];
					foreach (ZObjectiveMapping om in omList)
					{
						ZObjectiveResources ors = rp.Resources(om.type);
						string omName = om.name;
						ZObjectiveResource or = ors[om.name];
					}
				}
			}

			// let's see if we can figure out how much needs to be deducted
			foreach (ZTaskDeduction taskDeduction in ZRubric.activeSubmission.taskDeductions)
			{
                deduct = taskDeduction.deduct;
                t = string.Format("\ntask:'{0}', pts:{1:N2}, action:'{2}', targetType:'{3}', targetProperty:'{4}', \n    deduction:{5:N2}, ptsDeducted:{6:N2}, feedback:'{7}'\n",
					taskDeduction.task.text, 
					taskDeduction.task.pts, 
					taskDeduction.task.mapping.action,
					taskDeduction.task.target.type, 
					taskDeduction.task.target.property, 
					taskDeduction.deduct, 
                    taskDeduction.pointsDeducted,
                    taskDeduction.scenario.remediation.feedback);
				string t1 = taskDeduction.task.target.location.taggedAddress;
				ZStep dStep = rubric.steps[taskDeduction.stepID];
				//TODO: get back to original task target with tagged information
				string taskID = taskDeduction.origTaskID;
				ZTask dTask = dStep.tasks[taskID];
				//TODO - we're working, now I can drill back into the stuff I need to for the output
				t1 = dTask.target.location.taggedAddress;
				txtOut.Text += t;
				t = string.Format("\n    scenario'{2}', deductionType:{0}, deductionCategory:'{1}', tag:'{3}'\n",
					taskDeduction.scenario.deduction.type, taskDeduction.scenario.deduction.category, 
					taskDeduction.scenario.name, taskDeduction.scenario.remediation.tag);

				txtOut.Text += t;
                grade -= taskDeduction.pointsDeducted;

			}
			txtOut.Text += string.Format("Grade is {0:N2} out of {1:N2}\n", grade, rubric.project.totalPts);
			txtOut.Text += "==============================================================\n";
            rubric.SaveGradedSubmission(currentFolder + @"\gradedSubmission.json");
#if false
			foreach (ZStep st in rubric.steps)
			{
				txtOut.Text += string.Format("{0} - pts:{1:N2}\n", st.step, st.pts );
				foreach (ZTask tsk in st.tasks)
				{
					string t = string.Format("   task:'{0}', pts:{1:N2}, action:'{2}', targetType:'{3}', targetProperty:'{4}'\n", 
						tsk.text, tsk.pts, tsk.mapping.action, tsk.target.type, tsk.target.property);
					txtOut.Text += t;
				}
			}

			ZRubric rubric = new ZRubric(zexcel.currentFolder);
			//jTokKeys = rubric.SelectToken("Steps[02]").First.Value<JObject>();
			//JToken jTask = (jTokKeys as JObject).Value<JToken>("Tasks");
			//JObject jObj = (jTokKeys as JObject);
			//jTask = jTokKeys.First as JToken;;
			//jTask = jTokKeys.First.SelectToken("Tasks");
			//ZSteps stp = rubric.steps;
			if (rubric.LoadKeys())
			{
				zexcel.ToJson();
				doc = zexcel.ToString();
				bool loaded = rubric.LoadSubmission(doc);
			}
			foreach (ZStep st in rubric.steps)
			{
				txtOut.Text += st.step + "\n";
				foreach (ZTask tsk in st.tasks)
				{
					string t = string.Format("   task:'{0}', expression:'{1}', original:'{2}'\n", tsk.text, tsk.expression, tsk.originalExpression);
					txtOut.Text += t;
				}
			}
#if false		
			//JToken tokRubric = null; // JToken.Parse(rubric);
			//JObject oRoot = tokRubric as JObject;
			//JToken jTokKeys = oRoot.SelectToken("Keys");
			//JProperty propKey = null;
			//JProperty propKeys = null;
			//JObject jKey = null;
			//JArray keyArray = null;
			//JObject keyObj = null;
			//string keyDir = @"H:\z-studio\_ZStudio\data\NordellEx2IP4\keys";

			// open the original file for a particuar step
			// If the Beg (original) for this step doesn't exist, just use the Beg.json file
			// If loading the End (final) for a step doesn't exist, just use the End.json file
			//string originalJson = zexcel.currentFolder + @"\keys\03.Beg.json";
			//using (StreamReader infile = new StreamReader(originalJson))
			//{
			//	doc = infile.ReadToEnd();
			//}
#endif
			//JObject docJson = JObject.Parse(doc);
			// try it now
//			jTokKeys = rubric.SelectToken("Keys[03.Beg]");
//			jTokKeys = rubric.SelectToken("Submission.Worksheets[Employees].Cells[$I$4].Content.Formula");
			jTokKeys = rubric.SelectToken("Steps[0]");
//			jTokKeys = (jTokKeys as JProperty).Value.SelectToken("Name");	//[a].Content.Original.Expression");
			//if (jTokKeys != null)
			//{
			//	txtOut.Text += (jTokKeys as JValue).Value.ToString() + Environment.NewLine;
			//}
			ZStep step = rubric.steps[0];
			ZTask task = step.tasks[0];
			string zPath = "Submission.Worksheets[Employees].Cells[$J$13].Content.Value";
			jTokKeys = rubric.SelectToken(zPath);
			if (jTokKeys != null)
			{
				txtOut.Text += string.Format("Found zPath {0}", zPath) + Environment.NewLine;
				txtOut.Text += (jTokKeys as JValue).Value.ToString() + Environment.NewLine;
			}
#if false
			//JProperty jProp = tokRubric.Children().ElementAt(0) as JProperty;
			//ZLocation zLoc = null;
			//ZTask task = null;
			//switch (jProp.Name)
			//{
			//	case "a":
			//		task = new ZTask((JObject)jProp.Value);
			//		ZScenario scenario = task.zScenarios[0];
			//		ZAnswer answer = scenario.zAnswers[0];
			//		string val = answer.expression;
			//		txtOut.Text += String.Format("   answer.expression:{0}, scenario:{1}",
			//				val, scenario.name) + Environment.NewLine;

			//		break;
			//	case ZLocation.Tags.Item:
			//		// if ( jProp.Name == ZLocation.Tags.Item )
			//		zLoc = new ZLocation(jProp.Value as JObject);
			//		{
			//			string txt = zLoc.type;
			//			txtOut.Text += String.Format("   type:{0}, path:{1}",
			//					txt, zLoc.path) + Environment.NewLine;
			//		}

			//		break;
			//	case ZTask.Tags.Items:
			//		JArray jArray = jProp.Value as JArray;
			//		ZTasks zTasks = new ZTasks(jArray);
			//		int taskCtr = 0;
			//		foreach (ZTask zTask in zTasks)
			//		{
			//			string txt = zTask.property;
			//			txtOut.Text += String.Format("   Node{0}: text:{1}, type:{2}, path:{3}",
			//					taskCtr++, txt, ZTask.Tags.Item, zTask.location.path) + Environment.NewLine;
			//		}
			//		break;
			}
#endif
			txtOut.Text += "======================================" + Environment.NewLine;
#endif
		}

		#endregion

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            frmPreferences userPreferences = new frmPreferences();
            DialogResult result = userPreferences.ShowDialog(this);
            // perform some action based on result
            if (result == DialogResult.Cancel)
                return;
            // apply updates
        }

		private void txtRubric_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtRubric_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Point p = new Point(e.X, e.Y);
			// if this is a right click, see if we can determine what is under the mouse pointer
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				int charPos = txtRubric.GetCharIndexFromPosition(p);
				// find the step loc and then select the whole thing
				ZStepLoc stepLoc = locs.Find(charPos);
				if ( stepLoc != null )
					txtRubric.Select(stepLoc.startPos,stepLoc.length);
			}
		}
		ZExcelToJson zExcelToJson = null;

		private void cmdAddTask_Click(object sender, EventArgs e)
		{
			if ( zExcelToJson == null )
				zExcelToJson = new ZExcelToJson();
			// create step from txtStep first
			string taskID	= "task0";
			string taskKey	= "a";
			ZStep zStep = new ZStep(6, "[{" + taskID + "}" + txtStep.Text + "]");
			ZGUI.UserControls.frmZTaskAdd frmTaskAdd = new ZGUI.UserControls.frmZTaskAdd(zExcelToJson, zStep, taskKey, taskID );
			DialogResult dr = frmTaskAdd.ShowDialog(this);
			string value = "";
			if ( dr == System.Windows.Forms.DialogResult.OK )
			{
				value = frmTaskAdd.targetLocationAddress;
				value = frmTaskAdd.targetLocationContext;
			   }
		}
	}
}
