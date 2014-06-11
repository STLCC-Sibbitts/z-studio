using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;

#if false
{"Tasks":[
{
	"Name":"7a",
	"Text":"In cell F20, enter an IF function that tests whether the order quantity in cell E20 is greater than 0(zero). If it is, return the value of cell E20 multiplied by cell D20; otherwise, return no text by entering \"\"",
	"Location":
	{	
		"Name":"{^Name}",
		"Type":"Cell",
		"Worksheets":"Order Form", 
		"Cells":"$F$20",
		"Path":"Worksheets[Order Form].Cells[$F$20]" 
	},
	"Target":"Content",	
	"Required":true,					
	"Property":"{Expression(0)}{Expression(1)}",
	"Expression":"=IF(E20>0,D20*E20,\"\")",
	"Value":45.50,
	"Scenarios": 
	[
		{
			"Name":"MissingFalse",
			"Deduct":40,
			"Custom":false,
			"Type":"Technical",
			"Feedback":"false condition omitted, review IF",
			"Answer":
			{
				"Expression":"=IF({^Expression(1)},{^Expression(2)})"
			}
		}
	]
}
]}

#endif

namespace ZLib.ZRubric
{
    //"Target" : {
    //    "Text": "{^Text}",
    //    "Address": {
    //        "Type" : "Cell",
    //        "Tag" : "$I$4",
    //        "Context":"Employees"
    //    },
    //    "Type":"Content",
    //    "Property":"Formula"
    //},

	//public class ZTarget : ZToken
	//{
	//	//"Mapping":	{
	//	//    "Category":"LO",
	//	//    "Difficulty":"Hard",
	//	//    "Action":"Create"
	//	//},
	//	public new class Tags : ZToken.Tags
	//	{
	//		public const string Item        = "Target";
	//		public const string Text        = "Category";
	//		public const string Type        = "Type";
	//		public const string Property    = "Property";
	//	}
	//	//public ZTarget(ZToken obj) : base(obj) { }
	//	public ZTarget(JObject jObj) : base(jObj) { }
	//	public ZTarget() { }
	//	public string text
	//	{
	//		get { return GetStringValue(Tags.Text); }
	//		set { SetValue(Tags.Text, value); }
	//	}
	//	public string type
	//	{
	//		get { return GetStringValue(Tags.Type); }
	//		set { SetValue(Tags.Type, value); }
	//	}
	//	public string property
	//	{
	//		get { return GetStringValue(Tags.Property); }
	//		set { SetValue(Tags.Property, value); }
	//	}
	//	public ZTarget target
	//	{
	//		get 
	//		{
	//			JObject jObject = m_jToken.Value<JObject>(ZTarget.Tags.Item);
	//			ZTarget value = new ZTarget(jObject);
	//			return value; 
	//		}
	//		//set { SetValue(ZAddress.Tags.Item, value); }
	//	}
	//}
#if false	// older definition of ZAddress    
    public class ZAddress : ZToken
    {
        //    "Address": {
        //        "Type" : "Cell",
        //        "Tag" : "$I$4",
        //        "Context":"Employees"
        //    },
        public new class Tags : ZToken.Tags
        {
            public const string Item    = "Address";
            public const string Items   = Item + "s";
            public const string Type    = "Type";
            public const string Tag     = "Tag";
            public const string Context = "Action";
        }
        public class Type
        {
            public const string Cell        = "Cell";
            public const string Range       = "Range";
            public const string Worksheet   = "Worksheet";
            public const string Workbook    = "Workbook";
        }
        public ZAddress(ZToken obj) : base(obj) { }
        public ZAddress(JObject jObj) : base(jObj) { }
        public ZAddress() { }
        public string type
        {
            get { return GetStringValue(Tags.Type); }
            set { SetValue(Tags.Type, value); }
        }
        public string tag
        {
            get { return GetStringValue(Tags.Tag); }
            set { SetValue(Tags.Tag, value); }
        }
        public string context
        {
            get { return GetStringValue(Tags.Context); }
            set { SetValue(Tags.Context, value); }
        }
    }
#endif

    public class ZContent : ZObject<ZContent>
    {
        //"Content": {
        //    "LiteralValue":0.01,
        //    "Expression":"=VLOOKUP(H4,Bonus!$A$4:$B$8,2)",
        //    "BegExpression" : "{/Keys[02.End].Worksheets[{^Target.Worksheet}].Cells[{^Target.Address}].{^Target.Type}.{^Target.Property}}"
        //}

        public new class Tags : ZToken.Tags
        {
			public const string Text			= "Text";
			public const string Value			= "Value";
			public const string Formula			= "Formula";
			public const string FormulaR1C1		= "FormulaR1C1";
			public const string LiteralValue	= "LiteralValue";
			public const string Expression		= "Expression";
			public const string BegExpression   = "BegExpression";
        }
		public ZContent(ZObject<ZContent> obj) : base(obj) { }
        public ZContent(JObject jObj) : base(jObj) { }
        public ZContent() { }

		public string text
		{
			get
			{
				string value = GetStringValue(propName(MethodInfo.GetCurrentMethod())); 
				return value;
			}
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
		}
		public new string value
		{
			get { return GetStringValue(propName(MethodInfo.GetCurrentMethod())); }
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
		}
		public string formula
		{
			get { return GetStringValue(propName(MethodInfo.GetCurrentMethod())); }
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
		}
		public string formulaR1C1
		{
			get { return GetStringValue(propName(MethodInfo.GetCurrentMethod())); }
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
		}


		//public object literalValue
		//{
		//	get { return GetObjectValue(Tags.LiteralValue); }
		//	set { SetValue(Tags.LiteralValue, value); }
		//}
		public string expression
		{
			get { return GetStringValue(Tags.Expression); }
			set { SetValue(Tags.Expression, value); }
		}
		public string begExpression
		{
			get { return GetStringValue(Tags.BegExpression); }
			set { SetValue(Tags.BegExpression, value); }
		}
	}
	//public class ZAction 

    public class ZMapping : ZObject<ZMapping>
    {
        //"Mapping":	{
        //    "Category":"LO",
        //    "Difficulty":"Hard",
        //    "Action":"Create"
        //},
        public new class Tags : ZToken.Tags
        {
            public const string Action      = "Action";
        }
		public class Actions
		{
			public const string Apply	= "Apply";
			public const string Copy	= "Copy";
			public const string Create	= "Create";
			public const string Modify	= "Modify";

		}
		public ZMapping(ZObject<ZMapping> obj) : base(obj)
		{

		}
		public ZMapping(JObject jObj) : base(jObj) { }
        public ZMapping(){}
		public string defaultScenarioPath
		{
			get { return action + "." + context; }
		}

		public string category
		{
			get { return GetStringValue(propName(MethodInfo.GetCurrentMethod())); }
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
		}
		public string objective
		{
			get { return GetStringValue(propName(MethodInfo.GetCurrentMethod())); }
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
		}
		public bool review
		{
			get 
			{
				bool value = false;
				try
				{
					value = GetBoolValue(propName(MethodInfo.GetCurrentMethod())); 
				}
				catch
				{
				}
				return value;
			}
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
		}
		public string action
        {
			get { return GetStringValue(propName(MethodInfo.GetCurrentMethod())); }
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
		}
        public string difficulty
        {
            get { return GetStringValue( propName(MethodInfo.GetCurrentMethod())); }
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
        }
		public string context
		{
			get { return GetStringValue(propName(MethodInfo.GetCurrentMethod())); }
			set { SetValue(propName(MethodInfo.GetCurrentMethod()), value); }
		}
	}
	public class ZTask : ZObject<ZTask>
	{
		// copy constructor, do a deep copy
		public ZTask(ZTask task)
			: base(new JObject())
		{
			// start with a fresh one
			string str = task.text;
			name = task.name;
			text = str;
			ZTarget tgt = target;	// make sure we know what happens here
			target = new ZTarget(task.target);
#if USE_THIS
			if (false)
			{
				target.text = task.target.text;
				target.location.type = task.target.location.type;
				target.location.address = task.target.location.address;
				target.location.context = task.target.location.context;
				target.property = task.target.property;
				target.type = task.target.type;
			}
#endif
			answer = new ZAnswer(task.answer);
#if USE_THIS
			if (false)
			{
				answer.type = task.answer.type;
				answer.expression = task.answer.expression;
				answer.begExpression = task.answer.begExpression;
				answer.value = task.answer.value;
			}
#endif
			mapping = task.mapping;
//			args.jArray.RemoveAll();
			//args.jArray = new JArray();
			//args.jArray.RemoveAll();
			//for ( int argIndex = 0; argIndex < task.args.Count; ++argIndex)
			//{
			//	ZArg arg = new ZArg(task.args[argIndex]);
			//	args.Add(arg);
			//}
		}
		public ZTask(ZObject<ZTask> obj) : base(obj) 
		{ 
		}
		public ZTask(JObject jObj) : base(jObj) { }
		public ZTask() { }
		// TODO: not sure if I want to do this initialize here or not
		public override void Initialize()
		{
 			base.Initialize();
			//// set default values
			//name = "a";
			//text =  "Complete step";
			//pts = 0;	// will be determined by weight and category values from header
			//target = Targets.Content;
			//property = Properties.Formula;
			//weight = 1; 
			//category = ZWeightCategory.Categories.Primary;
			//location = new ZLocation();
			//stepLocation = new ZStepLoc();
			//zScenarios.Add(new ZScenario());	// add default scenario
		}
		// TODO: link to objective ontology and weights (default)
		public new class Tags : ZToken.Tags
		{
			public const string Text			= "Text";
			public const string Property		= "Property";
			public const string Target			= "Target";
			public const string Source			= "Source";

			public const string Expression      = "Expression";
			public const string R1C1Expression  = "R1C1Expression";
			public const string  Value			= "Value";
			public const string Category		= "RubricCategory";
			public const string Weight			= "Weight";
			public const string  Pts			= "Pts";
			public const string  StepLocation	= "StepLocation";
		}
		//public class Targets
		//{
		//	public const string Content			= "Content";
		//	public const string Format			= "Format";
		//}
		//public new class Properties : ZToken.Tags
		//{
		//	public const string Formula			= "Formula";
		//	public const string Value			= "Value";
		//	public const string DisplayValue	= "DisplayValue";
		//}
		public string taggedText
		{
			get
			{
				string value = GetStringValue(Tags.Text,false);
				return value;
			}
			set { SetValue(Tags.Text, value); }
		}
		public string text
		{
			get 
			{ 
				string value = GetStringValue(Tags.Text); 
				return value;
			}
			set { SetValue(Tags.Text, value); }
		}
		public ZSource source
		{
			get
			{
				JToken jToken = SelectToken(Tags.Source);
				ZSource val = null;
				if (jToken == null)
				{
					val = new ZSource();
					(base.m_jToken as JObject).Add(Tags.Source, (JObject)val);
				}
				else
					val = new ZSource(jToken as JObject);
				return val;
			}
			set
			{
				JObject jVal = base.m_jToken[Tags.Source] as JObject;
				// if we don't have it, add it
				if (jVal == null)
					(m_jToken as JObject).Add(Tags.Source, (JToken)value);
				else
					base.m_jToken[Tags.Source] = (JToken)value;
			}
		}
		public ZTarget target
        {
            get
            {
				JToken jToken = SelectToken(Tags.Target);
				ZTarget val = null;
				if (jToken == null)
				{
					val = new ZTarget();
					(base.m_jToken as JObject).Add(Tags.Target, (JObject)val);
				}
				else
					val = new ZTarget( jToken as JObject);
				return val;
            }
            set
            {
				JObject jVal = base.m_jToken[Tags.Target] as JObject;
                // if we don't have it, add it
                if (jVal == null)
					(m_jToken as JObject).Add(Tags.Target, (JToken)value);
                else
					base.m_jToken[Tags.Target] = (JToken)value;
            }
        }
#if false		// this is old stuff
        public string r1c1Expression
        {
            get
            {
                JToken jTok = m_jToken.SelectToken(Tags.R1C1Expression);
                JToken jVal = null;
                if ((jTok as JObject).TryGetValue(Tags.R1C1Expression, out jVal))
                    return ResolveStringValue(jVal.ToString());
                return "";
            }
            set { SetValue(Tags.R1C1Expression, value); }
        }
        public string expression
		{
			get
			{
				JToken jTok = m_jToken.SelectToken(Tags.Expression);
				JToken jVal = null;
				if (jTok != null &&  (jTok as JObject).TryGetValue(Tags.Expression, out jVal))
					return ResolveStringValue(jVal.ToString());
				return "";
			}
			set { SetValue(Tags.Expression, value); }
		}
		public string originalExpression
		{
			get
			{
				JToken jTok = m_jToken.SelectToken("Original." + ZContent.Tags.Expression );
				JToken jVal = null;
				if (jTok != null &&  (jTok as JObject).TryGetValue(Tags.Expression, out jVal))
					return ResolveStringValue( jVal.ToString());
				return "";
			}
			set { SetValue(Tags.Expression, value); }
		}
		public new string value
		{
			get { return GetStringValue(Tags.Value); }
			set { SetValue(Tags.Value, value); }
		}

		public string category
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); } // GetStringValue(Tags.Category); }
			set { SetValue(Tags.Category, value); }
		}
		public double weight
		{
			get { return GetDoubleValue(Tags.Weight); }
			set { SetValue(Tags.Weight, value); }
		}
#endif
		public double pts
		{
			get 
			{
				double value = ZRubric.activeProject.pts(mapping);
				return value; 
			}
			set { SetValue(Tags.Pts, value); }
		}

		//public ZStepLoc stepLocation
		//{
		//	get
		//	{
		//		ZStepLoc val = (ZStepLoc)(m_jToken[Tags.StepLocation] as JObject);
		//		return val;
		//	}
		//	set
		//	{
		//		JObject jVal = base.m_jToken[Tags.StepLocation] as JObject;
		//		// if we don't have a text attribute, add it
		//		if (jVal == null)
		//			(m_jToken as JObject).Add(Tags.StepLocation, (JObject)value);
		//		else
		//			base.m_jToken[Tags.StepLocation] = value;
		//	}
		//}
		public ZArgs args
		{
			get
			{
				JToken jToken = this[ZArgs.itemTag];
				if ( jToken == null )
					jToken = SelectToken(ZArgs.itemTag);
				ZArgs val = null;
				if (jToken == null)
				{
					if (!(base.m_jToken as JObject).TryGetValue(ZArgs.itemTag, out jToken))
					{
						val = new ZArgs();
						(base.m_jToken as JObject).Add(ZArgs.itemTag, (JToken)val);
					}
//					jToken = this[ZArgs.itemTag];
				}
				val = new ZArgs((JArray)jToken);
				return val;
			}
			//set 
			//{ 
			//	args = value; 

			//	JArray jVal = base.m_jToken[Tags.a.Target] as JObject;
			//	// if we don't have it, add it
			//	if (jVal == null)
			//		(m_jToken as JObject).Add(Tags.Target, (JToken)value);
			//	else
			//		base.m_jToken[Tags.Target] = (JToken)value;
			//}
		}

		public ZScenarios zScenarios
		{
			get
			{
				JToken jToken = null;	// this[ZScenarios.itemTag];
				if ( jToken == null )
					jToken = SelectToken(ZScenarios.itemTag);
				ZScenarios val = null;
				if (jToken == null)
				{
					if (!(base.m_jToken as JObject).TryGetValue(ZScenarios.itemTag, out jToken))
					{
						val = new ZScenarios();
						(base.m_jToken as JObject).Add(ZScenarios.itemTag, (JToken)val);
					}
					//jToken = new ZScenarios();
				}
				val = new ZScenarios((JArray)jToken);
				
				return val;
			}
			set { zScenarios = value; }
		}
		public ZAnswer answer
		{
			get
			{
				JToken jToken = SelectToken(ZAnswer.itemTag);
				ZAnswer val = null;
				if (jToken == null)
				{
					val = new ZAnswer();
					(base.m_jToken as JObject).Add(ZAnswer.itemTag, (JObject)val);
				}
				else
					val = new ZAnswer(jToken as JObject);
				return val;

			}
			set 
			{ 
				JObject jVal = base.m_jToken[ZAnswer.itemTag] as JObject;
				// if we don't have it, add it
				if (jVal == null)
					(m_jToken as JObject).Add(ZAnswer.itemTag, (JToken)value);
				else
					base.m_jToken[ZAnswer.itemTag] = (JToken)value;
			}
		}
		public ZMapping mapping
		{
			get
			{
				ZMapping val = (ZMapping)((JObject)GetObjectValue(ZMapping.itemTag));
				if (val == null)
				{
					val = new ZMapping();
					(m_jToken as JObject).Add(ZMapping.itemTag, (JObject)val);
				}
				return val;
			}
			set 
			{ 
				JObject jVal = base.m_jToken[ZMapping.itemTag] as JObject;
				// if we don't have it, add it
				if (jVal == null)
					(m_jToken as JObject).Add(ZMapping.itemTag, (JToken)value);
				else
					base.m_jToken[ZMapping.itemTag] = (JToken)value;
				//mapping = value; 
			}
		}
		public ZStep parentStep
		{
			get
			{
				ZStep step = null;
				JToken parent = m_jToken.Parent;
				if (parent != null)
				{
					JToken key = parent.SelectToken("Key");
					while (key == null && parent.Parent != null)
					{
						parent = parent.Parent;
						key = parent.SelectToken("Key");
					}
					step = new ZStep(parent as JObject);
				}
				else
					step = new ZStep();
				return step;
			}
		}
		public ZStep activeStep;
		public void GradeSubmission(ZStep step)
		{
			activeStep = step;
			// returns the amount deducted from this task
			string t = string.Format("   task:'{0}', pts:{1:N2}, action:'{2}', targetType:'{3}', targetProperty:'{4}'\n",
					text, pts, mapping.action, target.type, target.property);
			// this is where the rubber hits the road based on what is being graded
			// first decision is based on the target type
			if (target.type == ZTargetSource.Types.Content)
			{
				switch (target.property)
				{
					case ZTargetSource.Properties.Formula:
						GradeFormula();
						break;
					case ZTargetSource.Properties.FormulaR1C1:
						GradeFormula();
						break;
					case ZTargetSource.Properties.Text:
						GradeValue();
						break;
				}
			}
			else if (target.type == ZTarget.Types.Format)
			{
                GradeFormat();
			}
			else
			{
			}
			activeStep = null;	// done with this
			return;
		}
        public void GradeFormat()
        {
            switch (mapping.action)
            {
                case ZMapping.Actions.Apply:
                    GradeApplyFormat();
                    break;
                case ZMapping.Actions.Modify:
                    break;
            }
        }
        public void GradeFormula()
		{
			switch (mapping.action)
			{
				case ZMapping.Actions.Create:
					GradeCreateModifyFormula();
					break;
				case ZMapping.Actions.Modify:
					GradeCreateModifyFormula();
					break;
				case ZMapping.Actions.Copy:
					GradeCopyFormula();
					break;
			}
		}
		public void GradeValue()
		{
			switch (mapping.action)
			{
				case ZMapping.Actions.Create:
					GradeCreateValue();
					break;
				case ZMapping.Actions.Modify:
					break;
			}
		}
		public bool ZLiteralDeltaHandler(ZExprDeltaEventArgs zExprDeltaEventArgs)
		{
            ZThresholdPreference preference = ZRubric.activeProject.preferences.content.thresholdPreference(mapping.defaultScenarioPath) as ZThresholdPreference;
			bool allowed = false;
			//allowed = true;
			string tagValue = "";
			string feedback = "";
			ZExprNode foundNode = zExprDeltaEventArgs.zExprDelta.rExprNode;
			ZExprNode expectedNode = zExprDeltaEventArgs.zExprDelta.lExprNode;
			if (expectedNode.isTagged)
			{
				tagValue = expectedNode.tag;
			}
			if (expectedNode.isLiteral && preference != null)
			{
				ZScenario scenario = new ZScenario();
				scenario.name = zExprDeltaEventArgs.deltaName;
				ZAnswer dfltAnswer = new ZAnswer();
				scenario.answers.Add(dfltAnswer);

				// now we need to check to see the distance should be checked
				int levDist = expectedNode.text.LevenshteinDistance(foundNode.text);
				string soundsLikeDiffs =  SoundsLike.diffs;
				decimal levDistPct = expectedNode.text.LevDistPct(foundNode.text);
				// only add the partial credit feedback if partial credit is awarded
				if (levDist <= preference.threshold || levDistPct <= .10M)
				{
					allowed = true;
					scenario.deduction = preference.deduction;
					feedback = preference.remediation.partialCreditFeedback.Replace("{_expected_}", expectedNode.text).Replace("{_found_}",foundNode.text);
				}
				else
				{
					// take a full deduction for whatever the mapping category is for this task
					scenario.deduction = new ZDeduction(ZDeduction.Types.Full,mapping.category);
					feedback = preference.remediation.feedback.Replace("{_expected_}", expectedNode.text).Replace("{_found_}", foundNode.text);
				}
				scenario.remediation.feedback = feedback;
				scenario.remediation.tag = tagValue;
				ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
				ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
			}
			zExprDeltaEventArgs.zExprDelta.Allowed = allowed;

			return allowed;
		}

		public void GradeCreateValue()
		{
			ZContent	submissionContent = target.content[0];
			// start with the easiest thing first, if the values match, we're good
			if ( submissionContent.value == answer.value )
				return;
			// check other standard options based on preferences
			//TODO: check for ="literal value"
            ZScenario scenario = new ZScenario();
            ZAnswer dfltAnswer = new ZAnswer();
            ZDeduction dfltDeduction = new ZDeduction(ZDeduction.Types.Full, this.mapping.category);
			scenario.name = mapping.defaultScenarioPath;
			scenario.answers.Add(dfltAnswer);
			scenario.deduction = dfltDeduction;

			// preferences? allow equivalent
			ZPreferences prefs = ZRubric.activePreferences;
			// grab preference by mapping
			ZThresholdPreference valuePreference = ZRubric.activePreferences.content.thresholdPreference(mapping.defaultScenarioPath) as ZThresholdPreference;
			// make this part of the ZDeduction collection for evaluating this task
            string feedback = "";
            string answerText = answer.value;
            string submittedText = submissionContent.text;
			// default feedback
			if (submittedText.Length == 0)
			{
				scenario.name += "." + ZScenario.Errors.Missing;
				feedback = valuePreference.remediation.missingFeedback.Replace("{_expected_}", answerText);
			}
			else
            {
				feedback = valuePreference.remediation.feedback;
				feedback = valuePreference.remediation.feedback.Replace("{_expected_}", answerText).Replace("{_found_}", submittedText);
				if (prefs.partialCredit.enabled)
                {
                    // now we need to check to see the distance should be checked
                    int levDist = answerText.LevenshteinDistance(submittedText);
					// distance pct is hardcoded to 10% for now, in the threshold preference class and is retrieved as an integer
					// threshold for distance is the max of the threshold min or 10% of the string // length
					int	answerThreshold = answerText.Length / valuePreference.thresholdPct;
					if ( valuePreference.threshold > answerThreshold )
						answerThreshold = valuePreference.threshold;
                    string soundsLikeDiffs = SoundsLike.diffs;
                    decimal levDistPct = answer.value.LevDistPct(submittedText);
                    // only add the partial credit feedback if partial credit is awarded
					if (levDist <= answerThreshold)
                    {
                        scenario.deduction = valuePreference.deduction;
						feedback = valuePreference.remediation.partialCreditFeedback.Replace("{_expected_}", answerText).Replace("{_found_}", submittedText);
						// add note about partial credit
						scenario.remediation.tag = valuePreference.remediation.tag;
                    }
                }
/**
				//txtOut.Text += string.Format("expectedText: [{3}], foundText: [{4}], levDist {0}, " 
				//	+ "levDistPct: [{1:n2}], allowed: [{2}]" + Environment.NewLine,
				//		levDist, levDistPct, allowed, expectedNode.text, foundNode.text);
 **/
			}
            scenario.remediation.feedback = feedback;
            ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
            ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
		}
	
		//TODO! - implement task feedback/remediation and use the taskDeductions to obtain points deductions information
		//			the new routine will be able to cap deductions based on rubric category limits and let both the student and
		//			the teacher know if there were "warnings" or other issues
		private void xxx()
		{
			ZScenario scenario = new ZScenario();
			scenario.name = "DefaultValueEntry";
			ZAnswer dfltAnswer = new ZAnswer();
//			dfltAnswer.expression = "=" + answerExprWithDefaults;
			dfltAnswer.type = answer.type;
			scenario.answers.Add(dfltAnswer);
			ZExpressionPreference allowDefaultValueEntry =ZRubric.activeProject.preferences.content.expression.functionArg_DefaultValue;
			scenario.deduction = allowDefaultValueEntry.deduction;
			scenario.remediation = allowDefaultValueEntry.remediation;
			// TODO - perform final feedback expansion such that it can reference properties
			//			within the taskDeduction, may need to use special tokens
			string feedback = allowDefaultValueEntry.remediation.feedback;
//			scenario.remediation.feedback = string.Format(feedback, defaultParms, functionName);

			ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
			ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
			Debug.Print(string.Format("ScenarioMatched:[{0}], Remediation: Category-[{1}]; Feedback[{2}]",
				scenario.name, scenario.remediation.category, scenario.remediation.feedback));
		}
		public bool ZExprDeltaHandler(ZExprDeltaEventArgs zExprDeltaEventArgs)
		{
			bool allowed = false;
			string tagValue = "";
			string feedback = "";
			ZExprNode badNode = zExprDeltaEventArgs.zExprDelta.lExprNode;
            //TODO: update Typos for expression literal values

            ZScenario scenario = null;
			ZPreference expressionPreference = null;
			string prefDeltaName = zExprDeltaEventArgs.deltaName;
			if ( prefDeltaName.Length > 0 )
				expressionPreference = ZRubric.activePreferences.content.preference(prefDeltaName) as ZPreference;

            if (zExprDeltaEventArgs.scenario != null)
            {
                scenario = zExprDeltaEventArgs.scenario;
				// override deduction info?
				if (expressionPreference != null)
				{
					if (allowed = expressionPreference.enabled)
					{
						scenario.deduction = expressionPreference.deduction;
						scenario.remediation = expressionPreference.remediation;
					}
				}
				scenario.remediation.feedback = string.Format(scenario.remediation.feedback, zExprDeltaEventArgs.zExprDelta.rExprNode.text,
                    zExprDeltaEventArgs.deltaName);

            }
            else
            {
                scenario = new ZScenario();
                scenario.name = zExprDeltaEventArgs.deltaName;
                ZAnswer dfltAnswer = new ZAnswer();
                scenario.answers.Add(dfltAnswer);
                if (expressionPreference != null )
                {
					if (allowed = expressionPreference.enabled)
					{
						scenario.deduction = expressionPreference.deduction;
						scenario.remediation = expressionPreference.remediation;
					}
                    // TODO - perform final feedback expansion such that it can reference properties
                    //			within the taskDeduction, may need to use special tokens
                    // include answer found in "Answers[]"
					feedback = expressionPreference.remediation.feedback.Replace("{_argName_}", "{0}").Replace("{_functionName_}", "{1}");
                }
                else
                {
                    allowed = false;
                    ZDeduction dfltDeduction = new ZDeduction(ZDeduction.Types.Full, this.mapping.category);
                    scenario.deduction = dfltDeduction;
                    // TODO - perform final feedback expansion such that it can reference properties
                    //			within the taskDeduction, may need to use special tokens
                    // include answer found in "Answers[]"
                    feedback = "Found incorrect argument '{0}' for '{1}'.";

                }
                ZExprNode expectedNode = zExprDeltaEventArgs.zExprDelta.lExprNode;
                ZExprNode foundNode = zExprDeltaEventArgs.zExprDelta.rExprNode;
                string[] feedbackArgs = zExprDeltaEventArgs.feedbackArgs;	// { foundNode.text, expectedNode.text };
                if (expectedNode.isTagged)
                {
                    tagValue = expectedNode.tag;
                }

                scenario.remediation.feedback = string.Format(feedback, feedbackArgs);
                scenario.remediation.tag = tagValue;
            }
            // add the delta if some deduction
			if (scenario.deduction.type == ZDeduction.Types.None)
			{
				zExprDeltaEventArgs.zExprDelta.Allowed = true;
				return zExprDeltaEventArgs.zExprDelta.Allowed;	// if we match this scenario, it's allowed
			}

            ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
            ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);

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
		public void GradeCopyFormula()
		{
			// if we're copying a formula, or anything for that matter, we're going to have a series of targets
			// as for the answer, that will be equal to the source content

			List<ZContent> submissionContentList = target.content;
			ZContent sourceContent = source.content;
			string	sourceFormula = sourceContent.formulaR1C1;
			ZExpr sourceExpr = new ZExpr(sourceFormula);

			// preferences? allow equivalent
			ZPreferences prefs = ZRubric.activePreferences;
			// TODO: double-check to see if answers match and figure out if something different should be done??
			foreach (ZContent submissionContent in submissionContentList)
			{
				// start with the easiest thing first, if the formula matches the expression, we're good
				if (submissionContent.formula == answer.expression)
					continue;
				if ( submissionContent.formulaR1C1 == sourceFormula )
					continue;
				// depending on whether or not there are defined scenarios
				// check other standard options based on preferences
				// remove leading =

				ZExpr submissionExpr = new ZExpr(submissionContent.formula);
				string taggedExpr = answer.taggedExpression;
				string altExpr = answer.altTaggedExpression;
				string beginExpr = answer.begExpression;

				ZExpr answerExpr = new ZExpr(answer.taggedExpression, this);
				ZExpr altAnswerExpr = new ZExpr(answer.altTaggedExpression, this);
				// need to do this more explicitly, than implicitly
				// if we have scenarios, we will do straight-up comparisons through
				// the first pass
				if (zScenarios.Count == 0)
				{
					ZExpr.ExprDeltaHandler = ZExprDeltaHandler;
					bool areEqual = ZExpr.Compare(answerExpr.rootNode, submissionExpr.rootNode, mapping.defaultScenarioPath);
					return;
				}
				// disable it for now, we may change our minds
				ZExpr.ExprDeltaHandler = null;

				// now check through the scenarios if they exist, look for exact match
				foreach (ZScenario scenario in zScenarios)
				{
					// need to account for scenario details
					foreach (ZAnswer zAnswer in scenario.answers)
					{
						string exp = zAnswer.expression;
						ZExpr scenarioExpr = new ZExpr(exp, this);
						if (ZExpr.Compare(scenarioExpr, submissionExpr, scenario))
						{
							ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
							ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
							return;
						}
					}
				}
				// if we made it here, rerun the comparison and trigger delta
				ZExpr.ExprDeltaHandler = ZExprDeltaHandler;
				bool isEqual = ZExpr.Compare(answerExpr.rootNode, submissionExpr.rootNode, mapping.defaultScenarioPath);
				if (!isEqual)
				{
					// check against the beginning value for this expression, if it's fine, fine
					ZExpr begExpr = new ZExpr(answer.begExpression);
					ZExpr.ExprDeltaHandler = ZExprDeltaHandler;
					bool equalBegin = ZExpr.Compare(answerExpr.rootNode, begExpr.rootNode);
					if (equalBegin)
					{
						return;
					}

					// make another sweep with the delta handler in place
					// now check through the scenarios if they exist, look for exact match
					foreach (ZScenario scenario in zScenarios)
					{
						// need to account for scenario details
						foreach (ZAnswer zAnswer in scenario.answers)
						{
							string exp = zAnswer.expression;
							ZExpr scenarioExpr = new ZExpr(exp, this);
							if (ZExpr.Compare(scenarioExpr, submissionExpr, scenario))
							{
								ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
								ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
								return;
							}
						}
					}
				}
			}
			return;
		}

		public void GradeCreateModifyFormula()
		{
			ZContent	submissionContent = target.content[0];	// just grab the first one for now
			// preferences? allow equivalent
			ZPreferences prefs = ZRubric.activePreferences;
			// TODO: double-check to see if answers match and figure out if something different should be done??
	
			// start with the easiest thing first, if the formula matches the expression, we're good
			if ( submissionContent.formula == answer.expression )
				return;
            // depending on whether or not there are defined scenarios
			// check other standard options based on preferences
			// remove leading =

			ZExpr submissionExpr = new ZExpr(submissionContent.formula);
			string taggedExpr = answer.taggedExpression;
			string altExpr = answer.altTaggedExpression;
			string beginExpr = answer.begExpression;

			ZExpr answerExpr = new ZExpr(answer.taggedExpression, this);
			ZExpr altAnswerExpr = new ZExpr(answer.altTaggedExpression, this);
            // need to do this more explicitly, than implicitly
            // if we have scenarios, we will do straight-up comparisons through
            // the first pass
			if (zScenarios.Count == 0)
			{
				ZExpr.ExprDeltaHandler = ZExprDeltaHandler;
				bool areEqual = ZExpr.Compare(answerExpr.rootNode, submissionExpr.rootNode, mapping.defaultScenarioPath);
				return;
			}
			// disable it for now, we may change our minds
			ZExpr.ExprDeltaHandler = null;

            // now check through the scenarios if they exist, look for exact match
            foreach (ZScenario scenario in zScenarios)
            {
                // need to account for scenario details
                foreach (ZAnswer zAnswer in scenario.answers)
                {
                    string exp = zAnswer.expression;
                    ZExpr scenarioExpr = new ZExpr(exp, this);
                    if (ZExpr.Compare(scenarioExpr, submissionExpr, scenario) )
                    {
						// no harm, no foul
						if (scenario.deduction.type != ZDeduction.Types.None)
						{
							ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
							ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
						}
                        return;
                    }
                }
            }
            // if we made it here, rerun the comparison and trigger delta
            //ZExpr.ExprDeltaHandler = ZExprDeltaHandler;
			bool isEqual = false;	// check it after ZExpr.Compare(answerExpr.rootNode, submissionExpr.rootNode, mapping.defaultScenarioPath);
			if (!isEqual)
			{
				// check against the beginning value for this expression, if it's fine, fine
				ZExpr begExpr = new ZExpr(answer.begExpression);
				//Don't do this ZExpr.ExprDeltaHandler = ZExprDeltaHandler;
				bool equalBegin = ZExpr.Compare(answerExpr.rootNode, begExpr.rootNode);
				if ( equalBegin )
				{
					return;
				}

				// make another sweep with the delta handler in place
				// now check through the scenarios if they exist, look for exact match
				foreach (ZScenario scenario in zScenarios)
				{
					// need to account for scenario details
					foreach (ZAnswer zAnswer in scenario.answers)
					{
						string exp = zAnswer.expression;
						ZExpr scenarioExpr = new ZExpr(exp, this);
						if (ZExpr.Compare(scenarioExpr, submissionExpr, scenario))
						{
							ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
							ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
							return;
						}
					}
				}
			}
			// if we get here, we'll engage the default scenario
			ZExpr.ExprDeltaHandler = ZExprDeltaHandler;
			isEqual = ZExpr.Compare(answerExpr.rootNode, submissionExpr.rootNode, mapping.defaultScenarioPath);

			return;
#if false
				//	// need to check each of the answers, there may be more than one required
				//	Debug.Print(string.Format("Checking Scenario:[{0}], Notes:[{1}]",
				//		scenario.name, scenario.notes));
				//	// TODO: when checking each answer that involves a function:
				//	//			1) check preferences
				//	//				a) equivalent expression
				//	//				b) allow entry of default value for function arguments
				//	//					check args 1 at a time
				//	foreach (ZAnswer scenarioAnswer in scenario.answers)
				//	{
				//		Debug.Print(string.Format("    Checking Type:[{0}], Expression:[{1}]",
				//			scenarioAnswer.type, scenarioAnswer.expression));
				//		if (scenarioAnswer.expression != submissionContent.formula)
				//		{
				//			scenarioMatched = false;
				//		}
				//	}
				//	if (scenarioMatched)
				//	{
				//		ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
				//		ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
				//		Debug.Print(string.Format("ScenarioMatched:[{0}], Remediation: Category-[{1}]; Feedback[{2}]",
				//			scenario.name, scenario.remediation.category, scenario.remediation.feedback));
				//		double deduction = scenario.deduct;
				//		break;
				//	}
				//}
			//}
#endif
		}
        public bool isCorrectApplyFormatStyle(ZFormat submissionFormat, JProperty setting)
        {
            bool isCorrect = true;
            string subValue = submissionFormat.SelectToken("Style." + setting.Name).ToString();
            string ansValue = setting.Value.ToString();
            isCorrect = (subValue == ansValue);
            return isCorrect;
        }
        public bool isCorrectApplyFormatNumberFormat(ZFormat submissionFormat, JProperty settings)
        {
            bool isCorrect = true;
            string propId = settings.Name;
            string settingName = "";
            JValue ansValue = null;
            JValue subValue = null;
            foreach (JToken setting in settings.Value.Children())
            {
                if (setting.Type == JTokenType.Property)
                {

                    settingName = (setting as JProperty).Name;
                    ansValue = (setting as JProperty).Value as JValue;
                }
                else if (setting.Type == JTokenType.Object)
                {
                    settingName = (setting.First as JProperty).Name;
                    ansValue = (setting.First as JProperty).Value as JValue;
                }
                subValue = submissionFormat.SelectToken(propId + "." + settingName) as JValue;
                // compare them
                if (ansValue.Value.ToString() != subValue.Value.ToString())
                {
                    isCorrect = false;
                }
            }
            return isCorrect;
        }
        public void GradeApplyFormat(ZFormat submissionFormat)
        {
            //TODO: build 
            bool submissionCorrect = true;
            foreach (JProperty jProp in answer.zProperties.Properties)
            {
                if (jProp.Name == "Type")
                    continue;   // skip it
                switch (jProp.Name)
                {
                    case "Style":   // grab the name of the style
                        submissionCorrect &= isCorrectApplyFormatStyle(submissionFormat, jProp.First as JProperty);
                        break;
                    case "NumberFormat":
                        submissionCorrect &= isCorrectApplyFormatNumberFormat(submissionFormat, jProp);
                        break;
                }
            }
            // if submission correct, done, otherwise, check scenarios
            if (submissionCorrect)
                return;
            bool scenarioMatched = true;    // assume it matches, prove it doesn't
            foreach (ZScenario scenario in zScenarios)
            {
                scenarioMatched = true;
                string scenarioName = scenario.name;
                foreach (ZAnswer scenarioAnswer in scenario.answers)
                {
                    //TODO: consider writing functions that use the property name as a parameter to call a function
                    //      based on the qualified verb
                    foreach (JProperty jProp in scenarioAnswer.zProperties.Properties)
                    {
                        if (jProp.Name == "Type")
                            continue;   // skip it
                        switch (jProp.Name)
                        {
                            case "Style":   // grab the name of the style
                                scenarioMatched &= isCorrectApplyFormatStyle(submissionFormat, jProp.First as JProperty);
                                break;
                            case "NumberFormat":
                                scenarioMatched &= isCorrectApplyFormatNumberFormat(submissionFormat, jProp);
                                break;
                        }
                    }
                }
                // if all of the answers for this scenario match, we're done
                if (scenarioMatched)
                {
                    // make sure remediation is added to graded submission
                    return;
                }
            }
            // if we get here, the remediation is from the original

        }
        public void GradeApplyFormat()
        {
//            ZFormat submissionFormat = null;
            // preferences? allow equivalent
            ZPreferences prefs = ZRubric.activeProject.preferences;
            // since the target could be a range, need to iterate over each of the cells in the range
            ZTarget targetCell = target;
            if (target.location.type == ZLocation.Types.Cell)
            {
                GradeApplyFormat( targetCell.format );
            }
            else
            {
                string originalAddress = target.location.address;
                ZRangeRef rangeRef = new ZRangeRef(originalAddress);
                //OK - if we have the same format for all cells in the range, but that format is
                //      incorrect, it's just one error. Also need to make sure to only check
                //      that part of the format being assessed.
                // Create function to get a copy of the first format by copying it to the "answer's"
                // format and then using that
                foreach(ZCellRef cellRef in rangeRef )
                {
                    // overwrite the address with each cell in the range
                    targetCell.location.address = cellRef.ToAbsolute().ToString();
                    GradeApplyFormat( targetCell.format );
                }
                target.location.address = originalAddress;  // restore address
            }

#if false
            // start with the easiest thing first, if the formula matches the expression, we're good
            if (submissionContent.value == answer.literalValue)
                return;
            // check other standard options based on preferences
            // remove leading =
            ZExpr.ExprDeltaHandler = ZLiteralDeltaHandler;

            ZExpr submissionExpr = new ZExpr(submissionContent.expression);
            ZExpr answerExpr = new ZExpr(answer.taggedExpression, this);
            // if they don't match, or the typo is significant
            if (answerExpr != submissionExpr)
            {
                string feedback = "";
                ZScenario scenario = new ZScenario();
                ZAnswer dfltAnswer = new ZAnswer();
                ZExprNode expectedNode = answerExpr.rootNode;
                string tagValue = "";
                if (expectedNode.isTagged)
                {
                    tagValue = expectedNode.tag;
                }
                scenario.answers.Add(dfltAnswer);
                if (submissionContent.text.Length == 0)
                {
                    scenario.name = ZScenario.Errors.Missing;
                    feedback = string.Format(ZScenario.Remediation.Missing, answerExpr.rootNode.text);
                }
                else
                {
                    scenario.name = ZScenario.Errors.Incorrect;
                    feedback = string.Format(ZScenario.Remediation.Incorrect, answer.literalValue, submissionContent.text);
                }
                ZDeduction dfltDeduction = new ZDeduction(ZDeduction.Types.Full, this.mapping.category);
                scenario.deduction = dfltDeduction;
                scenario.remediation.tag = tagValue;
                scenario.remediation.feedback = feedback;
                ZTaskDeduction taskDeduction = new ZTaskDeduction(this, scenario);
                ZRubric.activeSubmission.taskDeductions.Add(taskDeduction);
            }
            // make this part of the ZDeduction collection for evaluating this task
            if (!prefs.partialCredit.enabled)
            {

            }
        }
#endif
        }
    }
	public class ZTasks : ZObjects<ZTasks, ZTask>
	{
		public ZTasks(ZObject<ZTask> zObject) : base(zObject) { }
		public ZTasks(JArray jArray) : base(jArray) { }
		public ZTasks()  { }

	}
}
