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
	"Target":"Content",	
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
			"Comments":"This usually indicates an incomplete understanding of the proper format of an IF statement.",
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

    public class ZRemediation : ZObject<ZRemediation>
    {
        //"Remediation": {
        //"Category" : "EE",
        //"Deduction" : "Minor",
        //"Feedback" : "Specifying the Range_lookup was not required, review {^Args[Function].Expression}"
        //},
        public new class Tags : ZToken.Tags
        {
            public const string Category    = "Category";
            public const string Deduction   = "Deduction";
            public const string Action      = "Action";
        }
		public ZRemediation(ZObject<ZRemediation>  obj) : base(obj) { }
        public ZRemediation(JObject jObj) : base(jObj) { }
        public ZRemediation() { }

        public string category
        {
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string partialCreditFeedback
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string feedback
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string missingFeedback
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string tag
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	public class ZScenario : ZObject<ZScenario>
	{
		public ZScenario(ZObject<ZScenario> obj) : base(obj) { }
		public ZScenario(JObject jObj) : base(jObj) { }
		public ZScenario(ZTask task) : base(new JObject())
		{

		}
		private const string DEFAULT_SCENARIO_NAME = "DefaultScenario";
		public ZScenario() : base(new JObject())
		{
			remediation = new ZRemediation();
            //deduct = 0;
			name = DEFAULT_SCENARIO_NAME;	// most contexts will have their own default
            ////credit = "Full";
            //type = "Content";
            //feedback = "Correct";

            //zAnswers.Add(new ZAnswer());
		}
		public class Errors
		{
			public const string Missing		= "Missing";
            public const string Incorrect   = "Incorrect";
            public const string Typo        = "Typo";
        }
		public class Remediation
		{
			public const string Missing		= "Expected value '{0}' not found.";
            public const string Incorrect   = "Expected value '{0}', found '{1}'.";
            public const string Typo        = "Expected value '{0}', found '{1}', partial credit given.";
        }
		public class Types
		{
			public const string Technical = "Technical";
		}
		public new class Tags : ZToken.Tags
		{
            public const string Notes           = "Notes";

			public const string  Deduct			= "Deduct";
			public const string  Custom			= "Custom";
			public const string  Type			= "Type";
			public const string  Feedback		= "Feedback";
			public const string  Comments		= "Comments";
			public const string  Expression		= "Expression";
			public const string  R1C1Expression	= "R1C1Expression";
		}
		public ZMapping parentMapping
		{
			get
			{
				JToken	jTok = base.GetParentProperty(ZMapping.itemTag);
				return (ZMapping)jTok;
			}
		}
		public ZAnswers answers
		{
			get
			{
				JToken jToken = SelectToken(ZAnswers.itemTag);
				ZAnswers val = null;
				if (jToken == null)
				{
					val = new ZAnswers();
					(base.m_jToken as JObject).Add(ZAnswers.itemTag, (JArray)val);
				}
				else
				{
					if (jToken.Type == JTokenType.Array)
						val = new ZAnswers((JArray)jToken);
					else
					{
						(base.m_jToken as JObject).Remove(ZAnswers.itemTag);
						(base.m_jToken as JObject).Add(ZAnswers.itemTag, (JArray)new ZAnswers());
					}
				}
				return val;
			}
			set { answers = value; }
		}
		public ZRemediation remediation
		{
			get
			{
				ZRemediation val = (ZRemediation)((JObject)GetObjectValue(ZRemediation.itemTag));
				if (val == null)
				{
					val = new ZRemediation();
					(m_jToken as JObject).Add(ZRemediation.itemTag, (JObject)val);
				}
				return val;
			}
			set
			{
				JObject jVal = base.m_jToken[ZRemediation.itemTag] as JObject;
				// if we don't have it, add it
				if (jVal == null)
					(m_jToken as JObject).Add(ZRemediation.itemTag, (JToken)value);
				else
					base.m_jToken[ZRemediation.itemTag] = (JToken)value;
			}
		}
		public double deduct
		{
			get 
			{ 
				ZMapping mapping = parentMapping;
				return ZRubric.activeProject.pts(mapping, deduction); 
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZDeduction deduction
		{
			get
			{
				JToken jToken = SelectToken(ZDeduction.itemTag);
				ZDeduction val = null;
				if (jToken == null)
				{
					val = new ZDeduction();
					(base.m_jToken as JObject).Add(ZDeduction.itemTag, (JObject)val);
				}
				else
					val = new ZDeduction((JObject)jToken);
				return val;
			}
			set 
			{
				JObject jVal = base.m_jToken[ZDeduction.itemTag] as JObject;
				// if we don't have it, add it
				if (jVal == null)
					(m_jToken as JObject).Add(ZDeduction.itemTag, (JToken)value);
				else
					base.m_jToken[ZDeduction.itemTag] = (JToken)value;
			}
		}
		//public string deduction
		//{
		//	get { return (string)get(MethodInfo.GetCurrentMethod()); }
		//	set { set(MethodInfo.GetCurrentMethod(), value); }
		//}
		public string notes
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string type
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		//public string credit
		//{
		//	get { return GetStringValue(Tags."Credit"); }
		//	set { SetValue("Credit", value); }
		//}
		// TODO: let feedback refer to properties in 
		public string feedback
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}

	}
	public class ZScenarios : ZObjects<ZScenarios, ZScenario>
	{
		public ZScenarios(JArray jArray) : base(jArray) { }
		public ZScenarios(ZObject<ZScenario> zObject) : base(zObject) { }
		public ZScenarios() { }
	}
}
