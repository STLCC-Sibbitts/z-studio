using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using System.Reflection;
#if false
// TODO: allow step to rollup values from each task
{
	"Name":"Step7",
	"Text":"In cell F20, enter an IF function that tests whether the order quantity in cell E20 is greater than 0(zero). If it is, return the value of cell E20 multiplied by cell D20; otherwise, return no text by entering \"\". AutoFill this formula into the range F21:F25",
	"Property":"Formula",
	"Pts":20,
	"Location":
	{
		"Worksheets":"Order Form", 
		"Type":"Cell",
		"Cells": [ "$F$20", "F21:F25" ],
		"Path":"Worksheets[Order Form].Cells[$F$20]" 
	},
	"Required":true,					
	"Type":"Formula",
	"Tasks":
	[
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
#endif
namespace ZLib.ZRubric
{
	public class ZStep : ZObject<ZStep>
	{
		public new class Tags : ZToken.Tags
		{
			public const string Text	= "Text";
			public const string Key		= "Key";
		}
		public ZStep(ZObject<ZStep> obj) : base(obj) { }
		public ZStep(JObject obj) : base(obj) { }
		public ZStep() { }
		public ZStep(int stepNumber, string stepText)
		{
			key = "{ParentID}";
			name = "Step {ParentID}";
			this.text = stepText;
		}
		private ZStepLocs m_stepLocs;
		public ZStepLocs stepLocs
		{
			get
			{
				// 
				if (m_stepLocs == null)
				{
					// grab the tagged data
					// rollup array
					string[]	array = GetStringArrayValue("Text");
					string		delim = "";
					string		value = "";
					foreach (string line in array)
					{
						value += delim + line;
						delim = "\n";
					}
					m_stepLocs = new ZStepLocs(value);
				}
				return m_stepLocs;
			}
		}
		public override void Initialize()
		{
			m_stepLocs = null;

		}
		public ZTasks tasks
		{
			get
			{
				JToken jToken = SelectToken(ZTasks.itemTag);
				ZTasks val = null; 
				if (jToken == null   )
				{

					val = new ZTasks();
					(base.m_jToken as JObject).Add(ZTasks.itemTag, (JArray)val);
					jToken = SelectToken(ZTasks.itemTag);
				}
				else
					val = new ZTasks((JArray)jToken);
				return val;
			}
			set { tasks = value; }
		}
		public string step
		{
			get { return text; }
			set { text = value; }
		}
		public string key
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZTags tags
		{
			get
			{
				string propName = PropName(MethodInfo.GetCurrentMethod());
				JToken jToken = SelectToken(propName);
//				jToken = SelectToken(ZTags.itemTag);
				ZTags val = null;
				if (jToken == null)
				{
					val = new ZTags();
					(base.m_jToken as JObject).Add(propName, (JArray)val);
				}
				else
					val = new ZTags((JArray)jToken);
				return val;
			}
			set { tags = value; }
		}

		public string text
		{
			get 
			{
				// grab text from stepLocs because that is where the step text is processed
				string	value = stepLocs.stepText;
				return value; 
			}
			//TODO: embed tags in text
			private set 
			{ 
				set(MethodInfo.GetCurrentMethod(), value.Split('\n') as string[]); 
			}
		}
		public double pts
		{
			get
			{
				double retVal = 0;
				// iterate through each of the tasks and add up their points
				foreach ( ZTask zTask in tasks )
				{
					retVal += zTask.pts;
				}
				return retVal;
			}
		}
		// This will just whip through the tasks, it will not return a deduction per se
		public void GradeSubmission()
		{
			// returns the amount deducted from this step
			foreach (ZTask task in tasks)
			{
				string t = string.Format("   task:'{0}', pts:{1:N2}, action:'{2}', targetType:'{3}', targetProperty:'{4}'\n",
					task.text, task.pts, task.mapping.action, task.target.type, task.target.property);
				task.GradeSubmission(this);
				//break;	// for now just grade the first task
			}
			return;
		}
	}
	public class ZSteps : ZObjects<ZSteps, ZStep>
	{
		public ZSteps(JObject rubric) : base(rubric[ZSteps.itemTag] as JArray) { }
		public ZSteps(ZObjects<ZSteps, ZStep> zObject) : base(zObject) { }
		public ZSteps(JArray jArray) : base(jArray) { }
		public ZSteps() { }
		public void Add(int stepNumber, ZStep step)
		{
			string key = stepNumber.ToString().PadLeft(2, '0');
			ZStep theStep = this[key];	// see if we have one
			JArray jArray = m_jToken as JArray;
			if (theStep == null)
			{
				// for now, assume we have the array and we're inserting things in order
				JProperty jProp = new JProperty(key, (JObject)step);
				jArray.Add(new JObject(jProp));
			}
			else
			{
				//TODO: verify this at some point
				throw new Exception(string.Format( "trying to add step that already exists[{0}]-[{1}]", stepNumber, step.text));
			}

		}
		public new ZStep this[string key]
		{
			get
			{
				ZStep theStep = null;
				foreach (ZStep s in this)
				{
					if (s.key == key)
					{
						theStep = s;
						break;
					}
				}
				return theStep;
			}
		}
	}

}
