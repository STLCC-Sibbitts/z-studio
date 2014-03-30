#region using
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using System.Diagnostics;
using ZLib;
using ZLib.ZRubric;

#endregion
#if false

{
	"Location":
	{	
		"Type":"Cell",
		"Worksheets":"Order Form", 
		"Cells":"$F$20",
		"Path":"Worksheets[Order Form].Cells[$F$20]" 
	}
}
#endif

namespace ZLib.ZRubric
{
	public class ZTarget : ZTargetSource
	{
		public ZTarget(ZTarget obj) : base()
		{
			text		= obj.text;
			type		= obj.type;
			property	= obj.property;
			location	= new ZLocation(obj.location);
		}
		public ZTarget(JObject jObj) : base(jObj) { }
		public ZTarget()
			: base(new JObject())
		{
		}
		public new class Tags : ZTargetSource.Tags
		{
			public const string Item		= "Target";
		}
		public double GradeIT(ZTask task)
		{
			double deduction = 0;

			return deduction;
		}
        public ZFormat format
        {
            get
            {
                ZFormat value = null;
                value = ZRubric.activeSubmission.GetFormat(this);
                return value;
            }
        }
		public ZContent content
		{
			get
			{
				ZContent value = null;
				value = ZRubric.activeSubmission.GetContent(this);
				return value;
			}
		}
		//public double GradeCreateFormula(ZTask task)
		//{
		//	double deduction = 0;
		//	ZContent	submissionContent = content;
		//	ZAnswer		answer = task.answer;
		//	// preferences? allow equivalent
		//	ZPreferences prefs = ZRubric.activeProject.preferences;
		//// TODO: create a ZDeduction class...

		//	// start with the easiest thing first, if the formula matches the expression, we're good
		//	if ( submissionContent.formula == answer.expression )
		//		return deduction;
		//	// check other standard options based on preferences
		//	// remove leading =
		//	ZExpr submissionExpr = new ZExpr(submissionContent.formula.Substring(1));
		//	ZExpr answerExpr = new ZExpr(answer.expression.Substring(1));
		//	// make this part of the ZDeduction collection for evaluating this task
		//	if (!prefs.partialCredit.enabled)
		//	{

		//	}
		//	else
		//	{
		//		string	defaultParms = "";
		//		string	functionName = "";
		//		string	defaultSep = "";
		//		// if we have a function, check for default arg inclusion
		//		if (answerExpr.rootNode.nodeType == ZExprNode.ZNodeType.zFunction)
		//		{
		//			// loop through the args and construct the revised answerExpression by including the default value
		//			if ( prefs.content.expressions.allowDefaultValueEntry.enabled )
		//			{
		//				string answerExprWithDefaults = "";
		//				string	sep = "";
		//				foreach ( ZArg arg in task.args )
		//				{
		//					if ( arg.id == "Function" )
		//					{
		//						answerExprWithDefaults = arg.expression + "(";
		//						functionName = arg.expression;
		//						continue;
		//					}
		//					// this means that no value was provided in the solution
		//					// TODO: update the json to see if I can just standardize the expression without
		//					//	putting null in explicitly
		//					if (arg.expression == NULL_VALUE || arg.expression.Length == 0)
		//					{
		//						answerExprWithDefaults += sep + arg.defaultValue;
		//						defaultParms += defaultSep + arg.id;
		//						defaultSep = ",";
		//					}
		//					else 
		//					{
		//						answerExprWithDefaults += sep + arg.expression;
		//					}
		//					sep = ",";
		//				}
		//				answerExprWithDefaults += ")";
		//				ZExpr answerExprWithDflt = new ZExpr(answerExprWithDefaults);
		//				// test for equivalency
		//				if (submissionExpr == answerExprWithDflt)
		//				{
		//					ZScenario scenario = new ZScenario();
		//					scenario.name = "DefaultValueEntry";
		//					ZAnswer dfltAnswer = new ZAnswer();
		//					dfltAnswer.expression = "=" + answerExprWithDefaults;
		//					dfltAnswer.type = answer.type;
		//					scenario.answers.Add(dfltAnswer);
		//					ZExpressionPreference allowDefaultValueEntry =ZRubric.activeProject.preferences.content.expressions.allowDefaultValueEntry;
		//					scenario.deduction = allowDefaultValueEntry.deduction;
		//					scenario.remediation = allowDefaultValueEntry.remediation;
		//					string feedback = allowDefaultValueEntry.remediation.feedback;
		//					scenario.remediation.feedback = string.Format( feedback, defaultParms, functionName);

		//					ZTaskDeduction taskDeduction = new ZTaskDeduction(task, scenario);
		//					ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
		//					Debug.Print(string.Format("ScenarioMatched:[{0}], Remediation: Category-[{1}]; Feedback[{2}]",
		//						scenario.name, scenario.remediation.category, scenario.remediation.feedback));
		//					deduction = taskDeduction.deduct;
		//					return deduction;
		//				}
		//			}

		//		}
		//		else
		//		{
		//			// test for equivalency
		//			if (submissionExpr == answerExpr)
		//				return deduction;
		//			// something else is going on
		//		}
		//		// the begExpression is a standard alternative and usually is present when the formula
		//		// is modified in a subsequent step, full credit is given if the formula matches the original value
		//		if (submissionContent.formula == answer.begExpression)
		//			return deduction;
		//		// check to see if no formula and literal value was entered
		//		if (submissionContent.formula.Length == 0 && submissionContent.value == answer.literalValue.ToString())
		//		{
		//			deduction = task.pts;
		//			// TODO: do something with remediation
		//		}
		//		// now check each of the scenarios
		//		bool scenarioMatched = true;
		//		foreach (ZScenario scenario in task.zScenarios)
		//		{
		//			scenarioMatched = true;
		//			// need to check each of the answers, there may be more than one required
		//			Debug.Print(string.Format("Checking Scenario:[{0}], Notes:[{1}]",
		//				scenario.name, scenario.notes));
		//			// TODO: when checking each answer that involves a function:
		//			//			1) check preferences
		//			//				a) equivalent expression
		//			//				b) allow entry of default value for function arguments
		//			//					check args 1 at a time
		//			foreach (ZAnswer scenarioAnswer in scenario.answers)
		//			{
		//				Debug.Print(string.Format("    Checking Type:[{0}], Expression:[{1}]",
		//					scenarioAnswer.type, scenarioAnswer.expression));
		//				if (scenarioAnswer.expression != submissionContent.formula)
		//				{
		//					scenarioMatched = false;
		//				}
		//			}
		//			if (scenarioMatched)
		//			{
		//				ZTaskDeduction taskDeduction = new ZTaskDeduction(task, scenario);
		//				ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
		//				Debug.Print(string.Format("ScenarioMatched:[{0}], Remediation: Category-[{1}]; Feedback[{2}]",
		//					scenario.name, scenario.remediation.category, scenario.remediation.feedback));
		//				deduction = scenario.deduct;
		//				break;
		//			}
		//		}
		//		if (!scenarioMatched)
		//		{
		//			Debug.Print(string.Format("NoScenarioMatch Found, Task:[{0}] - [{1}]", task.name, task.text));
		//			deduction = task.pts;
		//		}
		//	}
		//	return deduction;
		//}
		//public double GradeFormula(ZTask task)
		//{
		//	double deduction = 0;
		//	switch (task.mapping.action)
		//	{
		//		case ZMapping.Actions.Create:
		//			deduction = GradeCreateFormula(task);
		//			break;
		//		case ZMapping.Actions.Modify:
		//			break;
		//	}

		//	return deduction;
		//}
		//	public double GradeTask(ZTask task)
		//	{
		//		double deduction = 0;

		//		if (type == ZTargetSource.Types.Content)
		//		{
		//			switch(property)
		//			{
		//				case ZTargetSource.Properties.Formula:
		//					deduction = GradeFormula(task);
		//					break;
		//				case ZTargetSource.Properties.FormulaR1C1:
		//					break;
		//				case ZTargetSource.Properties.Text:
		//					break;
		//			}
		//		}
		//		else if (type == ZTarget.Types.Format)
		//		{
		//			switch (property)
		//			{
		//				case ZTargetSource.Properties.Font:
		//					break;
		//			}
		//		}
		//		else
		//		{
		//		}

		//		return deduction;
		//	}
		//}
	}
	public class ZSource : ZTargetSource
	{
		public ZSource(ZObject<ZTargetSource> obj) : base(obj) { }
		public ZSource(JObject jObj) : base(jObj) { }
		public ZSource()
			: base(new JObject())
		{
		}
		public new class Tags : ZTargetSource.Tags
		{
			public const string Item		= "Source";
		}
	}
	public class ZTargetSource : ZObject<ZTargetSource>
	{
		public ZTargetSource(ZObject<ZTargetSource> obj) : base(obj) { }
		public ZTargetSource(JObject jObj) : base(jObj) { }
		public ZTargetSource()
			: base(new JObject())
		{
			type = Types.Content;	// default target type
		}

		public new class Tags : ZObject<ZTargetSource>.Tags
		{
			public const string Text		= "Text";
			public const string Type		= "Type";
			public const string Property	= "Property";
		}
		public class Types
		{
			public const string Format		= "Format";
			public const string Content		= "Content";
		}
		public new class Properties
		{
			public const string Formula		= "Formula";
			public const string FormulaR1C1 = "FormulaR1C1";
			public const string Text		= "Text";
			public const string Font		= "Font";
		}
		public string text
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}

		public string type
		{
			get { return GetStringValue(Tags.Type); }
			set { SetValue(Tags.Type, value); }
		}
		public string property
		{
			get { return GetStringValue(Tags.Property); }
			set { SetValue(Tags.Property, value); }
		}
		public ZLocation location
		{
			get
			{
				JToken jToken = SelectToken(ZLocation.Tags.Item);
				ZLocation val = null;
				if (jToken == null)
				{
					val = new ZLocation();
					(base.m_jToken as JObject).Add(ZLocation.Tags.Item, (JObject)val);
				}
				else
					val = new ZLocation(jToken as JObject);
				return val;
			}
			set { SetValue(ZLocation.Tags.Item, value); }
		}
	}
	// ZLocation - used to tie a task to the resulting document

	public class ZLocation : ZObject<ZLocation>
	{
		public ZLocation(ZObject<ZLocation> obj) : base(obj) { }
		public ZLocation(JObject jObj) : base(jObj) { }
		public ZLocation(ZLocation location) : base()
		{
			type			= location.type;
			address			= location.address;
			taggedAddress	= location.taggedAddress;
			context			= location.context;
			taggedContext	= location.taggedContext;
		}
		public ZLocation()
			: base(new JObject())
		{
			type = Types.Cell;	// default location type
		}

		public new class Tags : ZObject<ZLocation>.Tags
		{
			public const string Item		= "Location";
			public const string Items		= Item + "s";
			public const string Type		= "Type";
			public const string Address		= "Address";
			public const string Context		= "Context";
#region old stuff to be removed
			public const string Worksheets	= "Worksheets";
			public const string Cells		= "Cells";
			public const string Path		= "Path";
			public const string RelPath		= "RelPath";
#endregion
		}
		public class Types
		{
			public const string Cell		= "Cell";
			public const string Range		= "Range";
			public const string Worksheet	= "Worksheet";
			public const string Workbook    = "Workbook";
		}
		public string type
		{
			get { return GetStringValue(Tags.Type); }
			set { SetValue(Tags.Type, value); }
		}
		public string qualifiedAddress
		{
			get 
			{
				// TODO this will change based on what's selected
				return context + "." + address; 
			}
		}
		public string address
		{
			get { return GetStringValue(Tags.Address); }
			set { SetValue(Tags.Address, value); }
		}
		public string taggedAddress
		{
			get
			{
				string[] valueArray = (string[])GetStringArrayValue("Tags.Address");
				string value = "";
				if (valueArray == null || valueArray.Length == 0)
				{
					if (valueArray == null)
					{
						valueArray = new string[0];
					}
					// this makes sure we have resolved the value
				}
				// now foldup the array into a single string
				foreach (string str in valueArray)
					value += str;
				string theAddress = ResolveStringValue("{^Address}");
				value = value.Replace("{^Address}",ResolveStringValue("{^Address}"));
				return value;
			}
			set 
			{
				//SetValue("Tags.Address", value);
			
				JObject jTok = SelectToken("Tags") as JObject;
				if (jTok == null)
				{
					jTok = new JObject();
					(this.m_jToken as JObject).Add("Tags", jTok);
					jTok.Add("Address",value);
				}
				else
					(jTok.SelectToken("Address") as JValue).Value = value;
			}
		}
		public string context
		{
			get { return GetStringValue(Tags.Context); }
			set { SetValue(Tags.Context, value); }
		}
		public string[] taggedContext
		{
			get
			{
				string[] value = (string[])get(MethodInfo.GetCurrentMethod());
				if (value == null || value.Length == 0)
				{
					if (value == null)
					{
						value = new string[0];
					}
					// this makes sure we have resolved the value
				}
				return value;
			}
			set 
			{
				JArray array = SelectToken("TaggedContext") as JArray;
				if (array == null)
				{
					array = new JArray();
					(this.m_jToken as JObject).Add("TaggedContext", array);
				}
				foreach ( String str in value )
					array.Add(new JValue(str));
			}
		}
		#region old stuff to be deleted
		public string worksheets
		{
			get { return GetStringValue(Tags.Worksheets); }
			set { SetValue(Tags.Worksheets, value); }
		}
		public string cells
		{
			get { return GetStringValue(Tags.Cells); }
			set { SetValue(Tags.Cells, value); }
		}
		public string path
		{
			get 
			{ 
				string thePath = string.Format("Worksheets[{0}].Cells[{1}]", context, address);
				return thePath; 
			}
			// set { SetValue(Tags.Path, value); }
		}
		public string relPath
		{
			get { return GetStringValue(Tags.RelPath); }
			set { SetValue(Tags.RelPath, value); }
		}
		#endregion
	}

}
