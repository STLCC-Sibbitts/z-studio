#region using
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;

#endregion

namespace ZLib.ZRubric
{
	public class ZRubric : ZToken
	{
		public new class Tags : ZToken.Tags
		{
			public static string Keys				= "Keys";
			public static string Submission			= "Submission";
		}
		#region properties
		private string m_rubric;		// string buffer
		private ZRubricSchema	m_schema;
		private string m_rubricFileName;
		private string m_rubricDir;
		public static ZProject		activeProject;
		public static ZSubmission	activeSubmission;
		public static ZFunctions	activeFunctions;
		public static ZOntologies	activeOntologies;
		public static ZPreferences	activePreferences;

		public string rubric
		{
			get
			{
				return m_rubric;
			}
			set
			{
				m_rubric = value;
				isLoading = true;
				Initialize(JObject.Parse(m_rubric));
#if false	// TODO: revisit this when I get 
				if (m_jToken != null)
				{
					(m_jToken as JObject).PropertyChanged += new PropertyChangedEventHandler(rubric_onPropertyChanged);
				}
#endif
				isLoading = false;

			}
		}
		public string rubricFileName
		{
			get { return m_rubricFileName; }
			set
			{
				// TODO: add exception handling to rubricFilaName set
				// see if this is already loaded
				if (m_rubricFileName == value)
					return;
				m_rubricFileName = value.Replace("*","");	// get rid of trailing asteric
				using (StreamReader infile = new StreamReader(m_rubricFileName))
				{
					rubric = infile.ReadToEnd();
				}
			}
		}

		public ZSteps steps
		{
			get
			{
				JToken jToken = SelectToken("/" + ZSteps.itemTag);
				ZSteps val = null; 
				if (jToken == null)
				{
					val = new ZSteps();
					(base.m_jToken as JObject).Add(ZSteps.itemTag, (JObject)val);
				}
				else
					val = new ZSteps((JArray)jToken);
				return val;
			}
			set { steps = value; }
		}
		public ZProject project
		{
			get
			{
				ZProject val = (ZProject)(m_jToken[ZProject.itemTag]);
				if (val == null)
				{
					val = new ZProject();
					(m_jToken as JObject).Add(ZProject.itemTag, (JArray)val);
				}
				return val;
			}
			set { project = value; }
		}


		#endregion

		#region state info
		private bool isLoading;
		private bool m_isDirty;
		public bool isDirty()
		{
			// check rubric to see what it thinks
			return m_isDirty;
		}
		void rubric_onPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (!isLoading)
				m_isDirty = true;
		}

		#endregion

		#region constructors and 'cast' stuff
		public override void Initialize()
		{
			base.Initialize();
			m_isDirty = false;

		}
		public override void Initialize(JToken jToken)
		{
			base.Initialize(jToken);
			ZRubric.activeProject = project;
			if ( activePreferences == null )
				LoadPreferences();
			if ( activeFunctions == null )
				LoadFunctions();
			if (activeOntologies== null)
				LoadOntologies();
			//			if (steps == null)
//				throw (new ApplicationException("no steps in this rubric:" + name));
		}
		public ZRubric(string rubricFolder)
		{
			m_rubricDir = rubricFolder;
			m_schema = null;	// new ZRubricSchema(rubricFolder +  @"\rubric.json.schema");
			rubricFileName = rubricFolder + @"\rubric.json";
            // look for the functions 
            //if (steps == null)
			//	throw (new ApplicationException("no steps in this rubric:" + name));
		}
		public ZRubric() { }
		public ZRubric(JToken obj) : base(obj) { }
		public ZRubric(ZRubricSchema schema, string filename)
		{
			m_schema = schema;
			rubricFileName = filename;
		}
		public static implicit operator JObject(ZRubric loc)
		{
			JObject jArray = null;
			try
			{
				jArray = loc.m_jToken as JObject;
			}
			catch (Exception ex)
			{
				string msg = ex.ToString();
			}
			return jArray;
		}
		#endregion

		// load the key files for this rubric
		public bool LoadKeys()
		{
			bool loadKeys = true;

			JToken jTokKeys = SelectToken(Tags.Keys);
			string keyDir = m_rubricDir + @"\keys";
			// if we don't find the keys here, add them
			if (jTokKeys == null)
			{
				(m_jToken as JObject).Add(new JProperty(Tags.Keys, new JArray()));
			}
			JArray keyArray = SelectToken(Tags.Keys) as JArray;
			JObject keyObj = null;
			JProperty propKey = null;
			JObject jKey = null;
			string doc = "";
			string keyKey = "";
            if (!Directory.Exists(keyDir))
                return loadKeys;
			foreach (string filename in Directory.EnumerateFiles(keyDir, "*.json"))
			{
				keyKey = filename.Substring(0, filename.LastIndexOf('.')).Substring(filename.LastIndexOf('\\')+1);
				using (StreamReader infile = new StreamReader(filename))
				{
					doc = infile.ReadToEnd();
				}
				keyObj = JToken.Parse(doc) as JObject;
				propKey = new JProperty(keyKey, keyObj);
				jKey = new JObject();
				jKey.Add(propKey);
				keyArray.Add(jKey);
			}
			return loadKeys;
		}
		private const string ontologyJsonFile = @"\src\data\ontology.json";
		public static bool LoadOntologies()
		{
			bool load = true;
			try
			{
				using (StreamReader inStream = new StreamReader(ontologyJsonFile))
				{
					string ontology = inStream.ReadToEnd();
					JObject ontologyObj = (JToken.Parse(ontology) as JObject);
					JArray fArray = null;
					JToken fTok	= ontologyObj.SelectToken(ZOntologies.itemTag);	// ontologyObj.First; // 
					if (fTok.Type == JTokenType.Array ) // JTokenType.Property && (fTok as JProperty).Name == ZOntology.itemTag && fTok.First.Type == JTokenType.Array) // )
					{
						fArray = fTok as JArray;
						activeOntologies = new ZOntologies(fArray);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message);
				load = false;
			}
			return load;
		}

        private const string functionsJsonFile = @"\src\data\Functions.json";
		public static bool LoadFunctions( )
		{
			bool load = true;
			try
			{
				using (StreamReader inStream = new StreamReader(functionsJsonFile))
				{
					string functions = inStream.ReadToEnd();
					JObject functionsObj = (JToken.Parse(functions) as JObject);
					JArray fArray = null;
					JToken fTok	= functionsObj.SelectToken(ZFunctions.itemTag);
					if (fTok.Type == JTokenType.Array)
					{
						fArray = fTok as JArray;
						activeFunctions = new ZFunctions(fArray);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message);
				load = false;
			}
			return load;
		}
        private const string preferencesFile = @"\src\data\Preferences.json";
        private static JToken userPreferences;
        public static bool LoadPreferences( )
        {
            bool load = true;
            try
            {
                using (StreamReader inStream = new StreamReader(preferencesFile))
                {
                    string json = inStream.ReadToEnd();
                    userPreferences = JToken.Parse(json);
                    JObject jObj = (userPreferences as JObject);
                    JArray fArray = null;
                    JToken fTok = jObj.SelectToken(ZPreferences.itemTag);
                    if (fTok.Type == JTokenType.Array)
                    {
                        fArray = fTok as JArray;
                        activePreferences = new ZPreferences(fArray);
                    }
                    ZContentPreferences content = activePreferences.content;
					//ZThresholdPreferences tPrefs = content.literal;
					//foreach (ZThresholdPreference tp in tPrefs)
					//{
					//	bool enabled = tp.enabled;
					//	string name = tp.name;
					//}
					//ZPreferences aPrefs = activePreferences.content.action;
					//foreach (ZPreference ap in aPrefs)
					//{
					//	bool enabled = ap.enabled;
					//	string name = ap.name;
					//}
					//ZPreferences ePrefs = activePreferences.content.expression;
					//foreach (ZPreference ep in ePrefs)
					//{
					//	bool enabled = ep.enabled;
					//	string name = ep.name;
					//}

                    ZMultiplierPreference deductions = activePreferences.deductions;
                    ZMultiplier dedMult = deductions.multipliers["Moderate"];

                    foreach (ZMultiplier mult in deductions.multipliers)
                    {
                        string name = mult.name;
                        double dbl = mult.factor;
                    }
                   // ZCoverages coverages =  activeMultipliers.coverages;
                }
            }
            catch (Exception ex)
            {
				Debug.Print(ex.Message);
				load = false;
            }
            return load;
        }
        public static bool SavePreferences( )
        {
            bool save = true;
            try
            {
                using (StreamWriter outStream = new StreamWriter(preferencesFile))
                {
                    string json = userPreferences.ToString();
                    outStream.Write(json);
                }
            }
            catch (Exception ex)
            {
				Debug.Print(ex.Message);
				save = false;
            }
            return save;
        }

        public string functions
		{
			get
			{
				return (activeFunctions == null ? "" : activeFunctions.ToString());
			}
			set
			{
				JObject functionsObj = (JToken.Parse(value) as JObject);
				JArray fArray = null;
				JToken fTok	= functionsObj.SelectToken(ZFunctions.itemTag);
				if (fTok.Type == JTokenType.Array)
				{
					fArray = fTok as JArray;
					activeFunctions = new ZFunctions(fArray);
				}
			}
		}
		public string submission
		{
			get
			{
				return (activeSubmission == null ? "" : activeSubmission.ToString());
			}
			set
			{
				JToken jTokSubmission = SelectToken(Tags.Submission);
				JObject submissionObj = JToken.Parse(value) as JObject;
				// if we don't find the keys here, add them
				if (jTokSubmission == null)
				{
					(m_jToken as JObject).Add(new JProperty(Tags.Submission, submissionObj));
				}
				else
				{
					jTokSubmission = submissionObj;
				}
				activeSubmission = new ZSubmission(submissionObj);
				// if we already have allocations, don't set them now
				JToken jtok = activeSubmission.SelectToken("Allocations");
				if ( jtok == null )
					activeSubmission.allocations = activeProject.allocations;
			}

		}
		public bool LoadSubmission(string submissionJsonFile)
		{
			bool loadSubmission = true;
			try
			{
				using (StreamReader inStream = new StreamReader(submissionJsonFile))
				{
					submission = inStream.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message);
				loadSubmission = false;
			}
			return loadSubmission;
		}
		public bool SaveGradedSubmission(string submissionJsonFile)
		{
			bool saveGradedSubmission = true;
			try
			{
				using (StreamWriter outStream = new StreamWriter(submissionJsonFile))
				{
					outStream.Write(submission);
					outStream.Close();
				}
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message);
				saveGradedSubmission = false;
			}
			return saveGradedSubmission;
		}
		public void GradeSubmission()
		{
			double grade = project.totalPts;
			// returns amount to be deducted from each step
			foreach (ZStep st in steps)
			{
				st.GradeSubmission();
				//break;
			}
		}
		public int ImportSteps(string filename)
		{
			int stepsImported = 0;
			// load it up by doing some simple parsing
			// If there is a beginning paragraph before the steps, assign this to the description
			// steps begin with a number in the first "column", everything else is part of the step
			// string stepsText = "";
			string work = "";
			//string line = "";
			int stepNumber = 0;
			//JObject newStep = null;
			int prevStepNumber = -1;
			string[] lineElements;
			using (StreamReader inStream = new StreamReader(filename))
			{
				string line;
				while ((line = inStream.ReadLine()) != null)
				{
					lineElements = line.Split('\t');
					// see if the first line element is an integer
					// TODO: may have to handle double, 1.1, 1.2, etc
					if (int.TryParse(lineElements[0], out stepNumber))
					{
						// if we had something in work, we need to add it to the rubric
						// if this is the first step number, whatever we loaded was the description
						// for this rubric
						if (prevStepNumber == -1)
						{
							project.description = work;
							work = "";
						}
						else
						{
							// we need to add this step
							++stepsImported;
							steps.Add(new ZStep(prevStepNumber, work));
						}
						prevStepNumber = stepNumber;
						work = lineElements[1];
					}
					else
					{
						// just accumulate what we have into work text
						if (work.Length > 0)
							work += "\n" + line;
						else
							work = line;
					}
				}
				++stepsImported;
				// add the last step
				if (work.Length > 0)
				{
					steps.Add(new ZStep(prevStepNumber, work));
				}
			}
			m_isDirty = true;
			return stepsImported;
		}

		#region Static RubricBits
		public static JArray Tasks(JObject step)
		{
			JArray tasks = step[ZTasks.itemTag] as JArray;
			if (tasks == null)
				throw (new ApplicationException("no tasks for this step:" + ((ZStep)step).name));
			return tasks;
		}
		public static JArray Scenarios(JObject task)
		{
			JArray scenarios = task[ZScenarios.itemTag] as JArray;
			if (scenarios == null)
				throw (new ApplicationException("no tasks for this step:" + ((ZTask)task).name));
			return scenarios;
		}
		//public static string OName(JToken tok)
		//{
		//	string oName = "";
		//	JObject obj = tok as JObject;
		//	JProperty prop = tok as JProperty;
		//	// if we have an object, grab the Name property, otherwise treat this as a property
		//	// and grab its name
		//	if (obj != null)
		//	{
		//		oName = tok[ZObject.Tags.Name].Value<string>();
		//	}
		//	else if (prop != null)
		//	{
		//		oName = prop.Name;
		//	}
		//	else if ( tok != null )
		//	{
		//		if ((prop = tok.Parent as JProperty) != null)
		//			oName = prop.Name;
		//	}
				
		//	return oName;
		//}
		#endregion

		#region myJsonPathStuff
		public static string GetJKey(JToken tok)
		{
			return GetJKey(tok, true);
		}
		public static string GetJKey(JToken tok, bool useIndexAsKey)
		{
			string jKey = "";
			// if I can get the property, it is the name of the object
			JProperty jProp = tok as JProperty;
			if (jProp != null)
				jKey = jProp.Name;
			else
			{
				// see if the current tok is a child in an array
				JArray aParent = tok.Parent as JArray;
				bool isChildToken = (aParent != null);
				JObject oCurrent = tok as JObject;
				if (oCurrent != null)
				{
					if (isChildToken)
					{
						int childIndex = -1;
						// figure out the index for this node
						for (JToken tPrevSibling = tok; tPrevSibling != null; tPrevSibling = tPrevSibling.Previous)
						{
							++childIndex;
						}
						if (jKey == "" || useIndexAsKey)
							jKey = "[" + childIndex.ToString() + "]";
						else
							jKey = "[" + jKey + "]";
					}
				}
			}

			return jKey;
		}
		public static string GetJPath(JToken tok)
		{
			return GetJPath(tok, true);
		}
		// return path for use in rubric
		public static string GetRPath(JToken tok)
		{
			string rPath = "";
			string partDelim = "";	// what delimiter to use 
			string part = "";
			// need to walk the hierarchy for this token, backwards
			for (JToken current = tok; current != null; current = current.Parent)
			{
				if ((current as JArray) != null) // treat arrays as anonymous and skip to previous token
					continue;

				// strip off brackets
				part = GetJKey(current, false).Split('[')[0];	// .Replace("[", "").Replace("]", "");
				if (part.Length > 0)
				{
					rPath = part + partDelim + rPath;
					partDelim = ".";	// what delimiter to use, after the first one
				}
			}
			// if we have a path, we will append the delimiter by default
			if (rPath.Length > 0)
				rPath += ".";
			return rPath;
		}
		public static string GetJPath(JToken tok, bool useIndexAsKey)
		{
			string jPath = "";
			string partDelim = ".";	// what delimiter to use 
			string part = "";
			// need to walk the hierarchy for this token, backwards
			for (JToken current = tok; current != null; current = current.Parent)
			{
				// partDelim = ".";
				bool isArray = ((current as JArray) != null);
				if (isArray)	// treat arrays as anonymous and skip to previous token
				{
					partDelim = "";		// no delim before array elements
					continue;
				}

				part = GetJKey(current, useIndexAsKey);
				if (part.Length > 0)
				{
					if (jPath.Length == 0)
						jPath = part;
					else
					{
						jPath = part + partDelim + jPath;
						partDelim = ".";	// reset to default delimiter
					}
				}
			}
			return jPath;
		}
		#endregion
	}
}
 