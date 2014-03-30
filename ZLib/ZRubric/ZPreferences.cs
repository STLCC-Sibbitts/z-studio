using System;
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
using System.Diagnostics;

namespace ZLib.ZRubric
{
	public class ZThresholdPreference : ZObject<ZThresholdPreference>
	{
		public int threshold
		{
			get 
			{
				int retValue = 0;	// default value to return if none found
				try
				{
					retValue = (int)get(MethodInfo.GetCurrentMethod());
				}
				catch (System.Exception ex)
				{

				}
				return retValue;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public int thresholdPct		// as a whole number
		{
			get { return 10; }
		}
        public bool enabled
        {
            get { return (bool)get(MethodInfo.GetCurrentMethod()); }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }
        public string notes
        {
            get
            {
                string retValue = "";
                try
                {
                    retValue = (string)get(MethodInfo.GetCurrentMethod());
                }
                catch (System.Exception ex)
                {

                }
                return retValue;
            }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }


		public ZDeduction deduction
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZDeduction zOpt = null;
				if (jObj == null)
					zOpt = new ZDeduction();
				else
					zOpt = new ZDeduction(jObj);

				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZRemediation remediation
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZRemediation zOpt = null;
				if ( jObj == null )
					zOpt = new ZRemediation();
				else
					zOpt = new ZRemediation(jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}

		public ZThresholdPreference(ZObject<ZThresholdPreference> zToken) : base(zToken) { }
		public ZThresholdPreference() { }
	}
    public class ZThresholdPreferences : ZObjects<ZThresholdPreferences,ZThresholdPreference>
    {
        public new ZThresholdPreference this[string preferenceName]
        {
            get
            {
                ZThresholdPreference zPreference = null;
                try
                {
                    JToken jTok = SelectToken(preferenceName);
                    if (jTok == null)
                        jTok = SelectToken("[" + preferenceName + "]");
                    zPreference = new ZThresholdPreference(jTok as JObject);
                }
                catch (System.Exception ex)
                {

                }
                return zPreference;
            }
        }
        public ZThresholdPreferences(ZThresholdPreferences zToken) : base(zToken) { }
        public ZThresholdPreferences(JToken jToken) : base(jToken) { }
        public ZThresholdPreferences() { }
    }
    public class ZMultiplierPreference : ZPreference
    {
        public ZMultiplierPreference(ZMultiplierPreference zToken) : base(zToken) { }
		public ZMultiplierPreference(JToken jToken) : base(jToken) { }
		public ZMultiplierPreference() { }
        public double baseValue
        {
            get
            {
                double retValue = 0;	// default value to return if none found
                try
                {
                    retValue = (int)get(MethodInfo.GetCurrentMethod());
                }
                catch (System.Exception ex)
                {

                }
                return retValue;
            }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }
        public double increment
        {
            get
            {
                double retValue = 0;	// default value to return if none found
                try
                {
                    retValue = (int)get(MethodInfo.GetCurrentMethod());
                }
                catch (System.Exception ex)
                {

                }
                return retValue;
            }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }

        public ZMultipliers multipliers
        {
            get
            {
                JProperty jTok = (m_jToken as JObject).Property("Multipliers");
                ZMultipliers zOpt = null;
                if (jTok == null)
                    zOpt = new ZMultipliers();
                else
                {
                    JArray jArray = jTok.Value as JArray;
                    zOpt = new ZMultipliers(jArray);
                }

                return zOpt;
            }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }

    }
	public class ZPreference : ZObject<ZPreference>
	{
		public object this[string propertyName]
		{
			get
			{
				JValue jVal = null;
				try
				{
					JObject jObj = m_jToken as JObject;
					if (jObj != null)
					{
						jVal = jObj.Value<JValue>(propertyName);
						if (jVal == null)
						{
							JToken jTok = SelectToken(propertyName);
							JToken jTok2 = (jTok as JObject).First;
							jVal = (jTok2 as JProperty).Value as JValue;
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Print(ex.Message);
				}
				return jVal;
			}
		}

		public bool enabled
		{
			get { return (bool)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string notes
		{
			get 
			{ 
				string retValue = "";
				try
				{
					retValue = (string)get(MethodInfo.GetCurrentMethod()); 
				}
				catch (System.Exception ex)
				{

				}
				return retValue; 
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public int threshold
		{
			get
			{
				int retValue = 0;	// default value to return if none found
				try
				{
					retValue = (int)get(MethodInfo.GetCurrentMethod());
				}
				catch (System.Exception ex)
				{

				}
				return retValue;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
        // factors, to the nearest
        //  00  - no rounding
        //  10  - .10, tenth of a point
        //  25  - .25, quarter point
        //  50  - .50, half point

        public int factor
        {
            get
            {
                int retValue = 0;	// default value to return if none found
                try
                {
                    retValue = (int)get(MethodInfo.GetCurrentMethod());
                }
                catch (System.Exception ex)
                {

                }
                return retValue;
            }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }

		public ZDeduction deduction
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZDeduction zOpt = null;
				if (jObj == null)
					zOpt = new ZDeduction();
				else
					zOpt = new ZDeduction(jObj);

				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZRemediation remediation
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZRemediation zOpt = null;
				if ( jObj == null )
					zOpt = new ZRemediation();
				else
					zOpt = new ZRemediation(jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}

		public ZPreference(ZObject<ZPreference> zToken) : base(zToken) { }
		public ZPreference(JToken jToken) : base(jToken) { }
		public ZPreference() { }
	}

	public class ZPreferences : ZObjects<ZPreferences,ZPreference>
	{
		public new class Tags : ZToken.Tags
		{
			public static string SkillLevels		= "SkillLevels";
			public static string Coverages			= "Coverages";
			public static string Difficulties		= "Difficulties";
			public static string Deductions			= "Deductions";
            public static string PartialCredit      = "PartialCredit";
            public static string Rounding           = "Rounding";
            public static string Content            = "Content";
			public static string Expressions		= "Expressions";

		}
		public new ZPreference this[string preferenceName]
		{
			get
			{
				ZPreference zPreference = null;
                JToken jTok = SelectToken(preferenceName);
				// if the preferenceName contains a period, locate it and split things out first
				string preferencePath = "";
				string[] pathElements = preferenceName.Split(".".ToCharArray(),2);
				preferencePath = "[" + pathElements[0] + "]";
				if ( pathElements.Count() > 1 )
					preferencePath += "." + pathElements[1];

                if (jTok == null)
                    jTok = SelectToken(preferencePath);
				zPreference = (ZPreference)jTok;
				return zPreference;
			}
		}
        public ZPreference rounding
        {
            get { return this["Rounding"];} // (ZPreference)((JToken)get(MethodInfo.GetCurrentMethod())); }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }
        public ZMultiplierPreference skillLevels
		{
            get
            {
                JToken jTok = this["SkillLevels"];
                return new ZMultiplierPreference(jTok);
            }
            set { set(MethodInfo.GetCurrentMethod(), value); }
		}
        public ZMultiplierPreference coverages
        {
            get
            {
                JToken jTok = this["Coverages"];
                return new ZMultiplierPreference(jTok);
            }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }
        public ZMultiplierPreference difficulties
        {
            get 
            {
                JToken jTok = this["Difficulties"];
                return new ZMultiplierPreference(jTok);
            }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }
        public ZMultiplierPreference deductions
        {
            get 
            {
                JToken jTok = this["Deductions"];
                return new ZMultiplierPreference(jTok); 
            }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }

        public ZPreference partialCredit
		{
			get 
			{
                JObject jObj = this["PartialCredit"];
				ZPreference zOpt = new ZPreference((JToken)jObj);
				return zOpt; 
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZContentPreferences content
		{
			get
			{
                JObject jObj = (JObject)this["Content"];
                JToken jTok = jObj.Value<JToken>(); // jObj.First;
				ZContentPreferences zOpt = new ZContentPreferences(jTok);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}

		public ZPreferences(ZObjects<ZPreferences, ZPreference> zObject) : base(zObject) { }
		public ZPreferences(JToken jToken) : base(jToken) { }
		public ZPreferences() { }
	}

	public class ZContentPreferences : ZObject<ZContentPreferences>
	{
		public string notes
		{
			get
			{
				string retValue = "";
				try
				{
					//JToken jObj = (JToken)get(MethodInfo.GetCurrentMethod());
					retValue = (string)get(MethodInfo.GetCurrentMethod());
				}
				catch (System.Exception ex)
				{

				}
				return retValue;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZThresholdPreference typos
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZThresholdPreference zOpt = new ZThresholdPreference(jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
        public ZThresholdPreference literal
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZThresholdPreference zOpt = new ZThresholdPreference(jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public object thresholdPreference(string preferencePath)
		{
			object zOpt = null;
			JToken jTok =  SelectToken(preferencePath);
			if (jTok != null)
			{
				//jTok = jTok.First.First;
			}
			if (jTok != null)
			{
				JToken jEnabled = jTok.SelectToken("Enabled");
				jEnabled = jTok.Value<JToken>("Enabled");
				// see if we have enabled as a property
				if (jEnabled == null || jEnabled.Parent.Parent != jTok)
					zOpt = new ZContentPreferences(jTok as JObject);
				else
					zOpt = new ZThresholdPreference(jTok as JObject);
			}
			return zOpt;

		}
		public ZPreferences action
		{
			get
			{
                JToken jTok = SelectToken("[Action]");
                // JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
                if (jTok != null)
                {
                    jTok = jTok.First.First;
                }
//                JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZPreferences zOpt = new ZPreferences(jTok);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZExpressionPreferences expression
		{
			get
			{
                JToken jTok = SelectToken("[Expression]");
                // JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
                if (jTok != null)
                {
                    jTok = jTok.First.First;
                }
                ZExpressionPreferences zOpt = new ZExpressionPreferences(jTok);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public new ZPreferences this[string contextName]
		{
			get
			{
				ZPreferences zPreferences = null;
				try
				{
					JToken jTok = SelectToken(contextName);
					zPreferences = new ZPreferences(jTok);
				}
				catch (System.Exception ex)
				{
					zPreferences = new ZPreferences();
				}
				return zPreferences;
			}
		}
		public ZContentPreferences(ZObject<ZContentPreferences> zToken) : base(zToken) { }
		public ZContentPreferences(JToken jToken) : base(jToken) { }
		public ZContentPreferences() { }

	}
	public class ZExpressionPreferences : ZPreferences
	{
		public new ZExpressionPreference this[string preferenceName]
		{
			get
			{
				ZExpressionPreference zPreference = null;
				try
				{
					JToken jTok = SelectToken(preferenceName);
					zPreference = new ZExpressionPreference(jTok);
				}
				catch (System.Exception ex)
				{

				}
				return zPreference;
			}
		}
		public ZExpressionPreference functionArg_DefaultValue
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZExpressionPreference zOpt = new ZExpressionPreference((JToken)jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZExpressionPreference functionArg_Range
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZExpressionPreference zOpt = new ZExpressionPreference((JToken)jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZExpressionPreference functionArg_NumericLiteral_OutputValue
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZExpressionPreference zOpt = new ZExpressionPreference((JToken)jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZExpressionPreference functionArg_StringLiteral_OutputValue
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZExpressionPreference zOpt = new ZExpressionPreference((JToken)jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZExpressionPreference functionArg_NumericLiteral
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZExpressionPreference zOpt = new ZExpressionPreference((JToken)jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZExpressionPreference functionArg_StringLiteral
		{
			get
			{
				JObject jObj = (JObject)get(MethodInfo.GetCurrentMethod());
				ZExpressionPreference zOpt = new ZExpressionPreference((JToken)jObj);
				return zOpt;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		// get expression preference by tag value
		public ZExpressionPreference preference(string preferenceTag)
		{
			JObject jObj = (JObject)GetObjectValue(preferenceTag);
			ZExpressionPreference zOpt = new ZExpressionPreference((JToken)jObj);
			return zOpt;
		}

		public ZExpressionPreferences(ZExpressionPreferences zToken) : base(zToken) { }
		public ZExpressionPreferences(JToken jToken) : base(jToken) { }
		public ZExpressionPreferences() { }
	}
	public class ZExpressionPreference : ZPreference
	{

		public ZExpressionPreference(ZExpressionPreference zToken) : base(zToken) { }
		public ZExpressionPreference(JToken jToken) : base(jToken) { }
		public ZExpressionPreference() { }
	}

}
