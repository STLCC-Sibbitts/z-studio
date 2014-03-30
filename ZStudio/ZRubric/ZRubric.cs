#region using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
#endregion

namespace JsonExplorer
{

	//    public IEnumerable<JProperty> Properties()
	//{
	//  return ChildrenTokens.Cast<JProperty>();
	//}


	#region ZSteps

	public class ZStep : ZObject
	{
		public ZStep(ZObject obj) : base(obj) { }
		public ZStep(JObject jObj) : base(jObj) { }
		public ZTasks zTasks
		{
			get { return new ZTasks( ZChildren("Tasks") ); }
		}
	}
	public class ZSteps : ZObjects
	{
		public ZSteps(JObject rubric) : base(rubric["Steps"] as JArray) { }
		public ZSteps(JArray jArray) : base(jArray) { }
		public override IEnumerator GetEnumerator()
		{
			return (IEnumerator)new ZStepEnumerator(this);
		}
	}
	public class ZStepEnumerator : ZEnumerator<ZStep>
	{
		public ZStepEnumerator(ZObject zObject) : base(zObject) { }
		public override object Current
		{
			get { return new ZStep(m_zObject.jArray[m_index] as JObject); }
		}
	}
	#endregion

	#region ZTasks
	public class ZTask : ZObject
	{
		public ZTask(ZObject obj) : base(obj) { }
		public ZTask(JObject jObj) : base(jObj) { }
		public ZScenarios zScenarios
		{
			get { return new ZScenarios(ZChildren("Scenarios")); }
		}
	}
	public class ZTasks : ZObjects
	{
		public ZTasks(ZObject zObject) : base(zObject) { }
		public ZTasks(JArray jArray) : base(jArray) { }
		public override IEnumerator GetEnumerator()
		{
			return (IEnumerator)new ZTaskEnumerator(this);
		}
	}
	public class ZTaskEnumerator : ZEnumerator<ZTask>
	{
		public ZTaskEnumerator(ZObject zObject) : base(zObject) { }
		public override object Current
		{
			get { return new ZTask(m_zObject.jArray[m_index] as JObject); }
		}
	}
	#endregion

	#region ZScenarios

	public class ZScenario : ZObject
	{
		public ZScenario(ZObject obj) : base(obj) { }
		public ZScenario(JObject jObj) : base(jObj) { }
		public ZAnswers zAnswers
		{
			get { return new ZAnswers(ZChildren("Answers")); }
		}
	}
	public class ZScenarios : ZObjects
	{
		public ZScenarios(JArray jArray) : base(jArray) { }
		public ZScenarios(ZObject zObject) : base(zObject) { }
		public override IEnumerator GetEnumerator()
		{
			return (IEnumerator)new ZScenarioEnumerator(this);
		}
	}
	public class ZScenarioEnumerator : ZEnumerator<ZScenario>
	{
		public ZScenarioEnumerator(ZObject zObject) : base(zObject) { }
		public override object Current
		{
			get { return new ZScenario(m_zObject.jArray[m_index] as JObject); }
		}
	}

	#endregion
	#region ZAnswer
	public class ZAnswer : ZObject
	{
		public ZAnswer(ZObject obj) : base(obj) { }
		public ZAnswer(JObject jObj) : base(jObj) { }
	}
	public class ZAnswers : ZObjects
	{
		public ZAnswers(ZObject zObject) : base(zObject) { }
		public ZAnswers(JArray jArray) : base(jArray) { }
		public override IEnumerator GetEnumerator()
		{
			return (IEnumerator)new ZAnswerEnumerator(this);
		}
	}
	public class ZAnswerEnumerator : ZEnumerator<ZAnswer>
	{
		public ZAnswerEnumerator(ZObject zObject) : base(zObject) { }
		public override object Current
		{
			get { return new ZAnswer( m_zObject.jArray[m_index] as JObject); }
		}
	}
	#endregion

	#region ZProperty
	public class ZProperty : ZObject
	{
		public ZProperty(ZObject obj) : base(obj) { }
		public ZProperty(JToken jTok) : base(jTok) { }
	}
	public class ZProperties : ZObject, IEnumerable
	{
		public ZProperties(ZObject zObject) : base(zObject)
		{
			if (m_jToken.Type != JTokenType.Object)
				throw (new ApplicationException("oops, we didn't get an object"));
		}
		public ZProperties(JToken jToken) : base(jToken)
		{
		}
		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)new ZPropertyEnumerator(this);
		}
	}
	public class ZPropertyEnumerator : ZOEnumerator<ZProperty>
	{
		public ZPropertyEnumerator(ZObject zObject) : base(zObject) { }
		public override object Current
		{
			get 
			{
				JToken jTok = m_zObject.Property(m_index) as JToken;
				ZProperty	zProp = new ZProperty(jTok);
				return zProp;
			}
		}
	}
	#endregion

	#region ZObjects
	public abstract class ZObjects : ZObject, IEnumerable
	{
		public ZObjects(ZObject zObject)
			: base(zObject)
		{
			if (m_jToken.Type != JTokenType.Array)
				throw (new ApplicationException("oops, we didn't get an array"));
		}
		public ZObjects(JToken jToken)
			: base(jToken)
		{
			if (jToken.Type != JTokenType.Array)
				throw (new ApplicationException("oops, we didn't get an array"));
		}
		public abstract IEnumerator GetEnumerator();
    }

	public abstract class ZEnumerator<T> : IEnumerator
	{
		protected int m_index;
		protected ZObject m_zObject;
		public ZEnumerator(ZObject zObject)
		{
			m_zObject = zObject;
			m_index = -1;
		}
		public void Reset()
		{
			m_index = -1;
		}
		public abstract object Current { get; }
		
		public bool MoveNext()
		{
			return (++m_index < m_zObject.jArray.Count());
		}
	}
	public abstract class ZOEnumerator<T> : IEnumerator
	{
		protected int m_index;
		protected ZObject m_zObject;
		public ZOEnumerator(ZObject zObject)
		{
			m_zObject = zObject;
			m_index = -1;
		}
		public void Reset()
		{
			m_index = -1;
		}
		public abstract object Current { get; }

		public bool MoveNext()
		{
			return (++m_index < m_zObject.Properties.Count());
		}
	}

	public abstract class ZObject
	{
		protected JToken m_jToken;
		public ZObject(ZObject other)
		{
			m_jToken = other.m_jToken;
		}
		public ZObject( )
		{
			m_jToken = null;
		}

		public ZObject (JToken jToken)
		{
			m_jToken = jToken;
		}
		public JToken Property(int ix)
		{
			JToken tok = null;
			JObject jObj = m_jToken as JObject;
			if (jObj != null)
			{
				tok = Properties[ix];
			}

			return tok;
		}

		public List<JToken> Properties
		{
			get
			{
				List<JToken> properties = null;
				JObject jObj = m_jToken as JObject;
				if (jObj != null)
				{
					properties = jObj.Children().ToList<JToken>(); ;
				}

				return properties;
			}
		}

		public JValue this[string propertyName]
		{
			get
			{
				JValue jVal = null;
				JObject jObj = m_jToken as JObject;
				if (jObj != null)
				{
					jVal = jObj.Value<JValue>(propertyName);
				}
				return jVal; 
			}
		}

		public JTokenType Type
		{
			get 
			{
				if (m_jToken == null)
					return JTokenType.Null;
				return m_jToken.Type; 
			}
		}
		public JArray jArray
		{
			get
			{
				JArray jArray = m_jToken as JArray;
				return jArray;
			}
		}
		public ZProperties zProperties
		{
			get 
			{
				return new ZProperties(m_jToken); 
			}
		}
		public JArray ZChildren(string name)
		{
			JArray jArray = null;
			// make sure this is an object for this call
			JObject jObj = m_jToken as JObject;
			if ( jObj != null )
				jArray = jObj[name] as JArray;
			return jArray; 
		}
		public string name
		{
			get
			{
				return ZRubric.OName(m_jToken);
			}
		}
		public string value
		{
			get
			{
				string val = "";
				JProperty prop = m_jToken as JProperty;
				if ( prop != null )
				{
					// if we have an object, grab its string rep
					if (prop.Value.Type == JTokenType.Object || prop.Value.Type == JTokenType.Array)
						val = prop.Value.ToString();
					else
					{
						JValue jVal = prop.Value as JValue;
						val = jVal.Value.ToString();
					}
				}
				return val;
			}
		}
		public string jPath
		{
			get { return ZRubric.GetJPath(m_jToken); }
		}
		public string rPath
		{
			get { return ZRubric.GetRPath(m_jToken); }
		}
	}


	#endregion

	public class ZRubric : ZObject
	{

		#region properties
		private JObject m_oRubric;	// json buffer
		private string m_rubric;		// string buffer
		private ZRubricSchema	m_schema;

		private string m_rubricFileName;
		private string m_documentFileName;
		private string m_name;				// name associated with the rubric
		private ZSteps m_steps;

		public ZSteps steps
		{
			get { return m_steps; }
		}
		public JObject oRubric
		{
			get { return m_oRubric; }
		}
		public string documentFileName
		{
			get { return m_documentFileName; }
			set { m_documentFileName = value; }
		}
		public string name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		public string rubricFileName
		{
			get { return m_rubricFileName; }
			set
			{
				// TODO: add exception handling to rubricFilaName set
				m_rubricFileName = value;
				using (StreamReader infile = new StreamReader(m_rubricFileName))
				{
					m_rubric = infile.ReadToEnd();
				}
				m_oRubric = JObject.Parse(m_rubric);
				List<JToken> properties = m_oRubric.Children().ToList<JToken>();

				if (m_oRubric != null)
				{
					JToken tokName = null;
					tokName = m_oRubric["File"];
					documentFileName = tokName.Value<string>();

					name = OName(m_oRubric);
				}
			}
		}
		#endregion

		public ZRubric(ZRubricSchema schema, string filename) 
		{
			m_schema = schema;
			rubricFileName = filename;
			base.m_jToken = m_oRubric;
			m_steps = new ZSteps( m_oRubric );
			if (m_steps == null)
				throw (new ApplicationException("no steps in this rubric" + OName(m_oRubric)));
		}
		#region Static RubricBits
		public static JArray Tasks(JObject step)
		{
			JArray tasks = step["Tasks"] as JArray;
			if (tasks == null)
				throw (new ApplicationException("no tasks for this step:" + OName(step)));
			return tasks;
		}
		public static JArray Scenarios(JObject task)
		{
			JArray scenarios = task["Scenarios"] as JArray;
			if (scenarios == null)
				throw (new ApplicationException("no tasks for this step:" + OName(task)));
			return scenarios;
		}
		public static string OName(JToken tok)
		{
			string oName = "";
			JObject obj = tok as JObject;
			JProperty prop = tok as JProperty;
			// if we have an object, grab the Name property, otherwise treat this as a property
			// and grab its name
			if (obj != null)
			{
				oName = tok["Name"].Value<string>();
			}
			else if (prop != null)
			{
				oName = prop.Name;
			}
			else if ( tok != null )
			{
				if ((prop = tok.Parent as JProperty) != null)
					oName = prop.Name;
			}
				
			return oName;
		}
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
