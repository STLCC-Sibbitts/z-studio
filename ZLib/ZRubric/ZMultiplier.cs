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

namespace ZLib.ZRubric
{
	public class ZDeduction: ZObject<ZDeduction>
	{
		public ZDeduction(ZObject<ZDeduction> obj) : base(obj) { }
		public ZDeduction(JObject jObj) : base(jObj) { }
		public ZDeduction() : base(new JObject()) { }
		public ZDeduction(string type, string category) : base(new JObject()) 
		{
			this.type		= type;
			this.category	= category;
		}
		public class Types
		{
			public const string None		= "None";
			public const string Minor		= "Minor";
			public const string Moderate	= "Moderate";
			public const string Major		= "Major";
			public const string Full		= "Full";
		}
		public class Categories
		{
			public const string NCE		= "NCE";
			public const string EE		= "EE";
			public const string LO		= "LO";
		}
		public string type
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
        public string category
        {
            get { return (string)get(MethodInfo.GetCurrentMethod()); }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }
        public double pointsDeducted
        {
            get { return (double)get(MethodInfo.GetCurrentMethod()); }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }
    }

	/// <summary>
	/// Represents the degree to which a deduction is assessed against the point value
	/// of the task and is designated per scenario. 
	/// </summary>
	public class ZDeductions : ZObjects<ZDeductions, ZMultiplier>
	{
		public ZDeductions(ZObject<ZMultiplier> zObject) : base(zObject) { }
		public ZDeductions(JArray jArray) : base(jArray) { }
		public ZDeductions() { }
		public bool enabled { get { return ZMultiplier.enabled(this, itemTag); } }
		public new double this[string multiplier] { get { return ZMultipliersBase.factor(this, multiplier); } }

		public double none
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double minor
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double moderate
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double major
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double full
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	/// <summary>
	/// Represents the degree to which a deduction is assessed against the point value
	/// of the task and is designated per scenario. 
	/// </summary>
	public class ZCoverages : ZObjects<ZCoverages, ZMultiplier>
	{
		public ZCoverages(ZMultiplier zObject) : base(zObject) { }
		public ZCoverages(JArray jArray) : base(jArray) { }
		public ZCoverages() { }
		public bool enabled { get { return ZMultiplier.enabled(this, itemTag); } }
		public new double this[string multiplier] { get { return ZMultipliersBase.factor(this, multiplier); } }

		public double review
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
		}
		public double normal
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}
	public class ZMultipliersBase : ZObjects<ZMultipliersBase, ZMultiplier>
	{
		public ZMultipliersBase(ZMultiplier zObject) : base(zObject) { }
		public ZMultipliersBase(JArray jArray) : base(jArray) { }
		public ZMultipliersBase() { }
		public ZMultipliersBase(ZMultipliers multipliers, string multiplerName) 
			: base ((JArray) multipliers.GetObjectValue(multiplerName)) { }

		public bool enabled  { get { return ZMultiplier.enabled(this, name); } }
		public static double factor(ZToken multipler, string propName)
		{
			return ((ZMultiplier)(multipler.SelectToken("[" + propName + "]"))).factor;
		}
		public new double this[string multiplierPropName]
		{
			get { return factor(this, multiplierPropName); } // ((ZMultiplier)(SelectToken("[" + multiplierPropName + "]"))).factor; }
		}
	}

	/// <summary>
	/// Represents the relative difficulty of the task at hand and allows for the point value
	/// for the task to be adjusted accordingly.
	/// </summary>
	public class ZDifficulties : ZObjects<ZDifficulties, ZMultiplier>
	{
		public ZDifficulties(ZMultiplier zObject) : base(zObject) { }
		public ZDifficulties(JArray jArray) : base(jArray) { }
		public ZDifficulties()  { }
		public bool enabled { get { return ZMultiplier.enabled(this, itemTag); } }
		public new double this[string multiplier] 
        {
            get 
            { 
                return ZMultipliersBase.factor(this, multiplier); 
            } 
        }
		public double easy
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double normal
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double hard
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double challenging
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	/// <summary>
	/// Represents the relative skill level that the learner/user is expected to be functioning at
	/// when attempting the task. 
	/// </summary>
	public class ZSkillLevels : ZObjects<ZSkillLevels, ZMultiplier>
	{
		public ZSkillLevels(ZMultiplier zObject) : base(zObject) { }
		public ZSkillLevels(JArray jArray) : base(jArray) { }
		public ZSkillLevels() { }
		public bool enabled { get { return ZMultiplier.enabled(this, itemTag); } }
		public new double this[string multiplier] { get { return ZMultipliersBase.factor(this, multiplier); } }

		public double novice
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double beginner
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double intermediate
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double advanced
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double expert
		{
			get { return this[propName(MethodInfo.GetCurrentMethod())]; }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	public class ZMultiplier : ZObject<ZMultiplier>
	{
		public ZMultiplier(ZObject<ZMultiplier> obj) : base(obj) { }
		public ZMultiplier(JToken obj) : base(obj) { }
		public ZMultiplier() { }
		public static bool enabled(ZToken zToken, string multiplierType)
		{
			bool	value = false;
			try
			{
				string enabledTag = "/" + ZProject.itemTag + ".Preferences." + multiplierType + ".Enabled";
				JToken jTok = zToken.SelectToken(enabledTag);
				if (jTok != null && jTok.Type == JTokenType.Boolean)
				{
					JValue jVal = jTok as JValue;
					value = jVal.Value<bool>();
				}
			}
			catch (Exception )
			{
				value = false;
			}
			return value;
		}
		public bool factorEnabled
		{
			get
			{
				bool value = false;
				string[] path = null;
				string tag = "";
                //TODO: patch up multiplier enabled
                if (rPath.Contains(ZMultipliers.itemTag) && false)
                {
                    path = rPath.Split('.');
                    if (path.Length > 2)
                    {
                        tag = rPath.Split('.')[1];
                        value = enabled(this, tag);
                    }
                }
                else
                {
                    // check parent
                    string enabled = ResolveStringValue("{^Enabled}");
                    if ( ! bool.TryParse(enabled, out value) )
                        value = false;
                }
				return value;
			}
		}
		public double factor
		{
			get
			{
				double value = 1;	// default to a value of 1, override only if factor enabled
				if (factorEnabled)
				{
					JProperty jProp = (m_jToken as JObject).Properties().First();
					if (jProp != null)
					{
						JValue jVal = jProp.Value as JValue;
						value = jVal.Value<double>();
					}
				}
				return value;
			}
		}
	}
	public class ZMultipliers : ZObjects<ZMultipliers,ZMultiplier>
	{
		public ZMultipliers(ZObjects<ZMultipliers,ZMultiplier> zObject) : base(zObject) { }
        public ZMultipliers(JObject obj) : base(obj) { }
        public ZMultipliers(JArray obj) : base(obj) { }
        public ZMultipliers() { }
		public double this[string multiplerName, string propName]
		{
			get { return (new ZMultipliersBase(this, multiplerName))[propName]; }
		}
		public ZDifficulties difficulties
		{
			get 
            {
                JArray jArray = null;
                if (m_jToken.Type == JTokenType.Array)
                {
                    JToken jTok = m_jToken.SelectToken("[" + ZDifficulties.itemTag + "]");
                    JObject jObj = jTok as JObject;
                    jArray = jObj[ZDifficulties.itemTag] as JArray;
                }
                else
                    jArray = (JArray)GetObjectValue(ZDifficulties.itemTag);
                return new ZDifficulties(jArray);
            }
		}
		public ZDeductions deductions
		{
			get 
            {
                JArray jArray = null;
                if (m_jToken.Type == JTokenType.Array)
                {
                    JToken jTok = m_jToken.SelectToken("[" + ZDeductions.itemTag + "]");
                    JObject jObj = jTok as JObject;
                    jArray = jObj[ZDeductions.itemTag] as JArray;
                }
                else
                    jArray = (JArray)GetObjectValue(ZSkillLevels.itemTag);
                return new ZDeductions(jArray);
            }
		}
		public ZCoverages coverages
		{
			get
            {
                JArray jArray = null;
                if (m_jToken.Type == JTokenType.Array)
                {
                    JToken jTok = m_jToken.SelectToken("[" + ZCoverages.itemTag + "]");
                    JObject jObj = jTok as JObject;
                    jArray = jObj[ZCoverages.itemTag] as JArray;
                }
                else
                    jArray = (JArray)GetObjectValue(ZCoverages.itemTag);
                return new ZCoverages(jArray);
            }
		}
		public ZSkillLevels skillLevels
		{
			get 
            {
                JArray jArray = null;
                if (m_jToken.Type == JTokenType.Array)
                {
                    JToken jTok = m_jToken.SelectToken("[" + ZSkillLevels.itemTag + "]");
                    JObject jObj = jTok as JObject;
                    jArray = jObj[ZCoverages.itemTag] as JArray;
                }
                else
                    jArray = (JArray)GetObjectValue(ZSkillLevels.itemTag);
                return new ZSkillLevels(jArray);
            }
		}
	}

#if false // original type specific multipliers
	public class ZDeduction : ZObject<ZDeduction>
	{
		public new class Tags
		{
			public const string None		= "None";
			public const string Minor		= "Minor";
			public const string Moderate	= "Moderate";
			public const string Major		= "Major";
			public const string Full		= "Full";
		}
		public ZDeduction(ZObject<ZDeduction> obj) : base(obj) { }
		public ZDeduction(JToken obj) : base(obj) { }
		public ZDeduction() { }
		public double none
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double minor
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double moderate
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double major
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double full
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	public class ZDifficulty : ZObject<ZDifficulty>
	{
		public new class Tags
		{
			public const string Easy		= "Easy";
			public const string Normal		= "Normal";
			public const string Hard		= "Hard";
			public const string Challenging	= "Challenging";
		}
		public ZDifficulty(ZDifficulty obj) : base(obj) { }
		public ZDifficulty(JToken obj) : base(obj) { }
		public ZDifficulty() { }
	}
	public class ZCoverage : ZObject<ZCoverage>
	{
		public new class Tags : ZToken.Tags
		{
			public const string Review		= "Review";
			public const string Normal		= "Normal";
		}
		public ZCoverage(ZObject<ZCoverage> obj) : base(obj) { }
		public ZCoverage(JToken obj) : base(obj) { }
		public ZCoverage() { }
		public double review {
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double normal {
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}
	public class ZSkillLevel : ZObject<ZSkillLevel>
	{
		public new class Tags
		{
			public const string Easy		= "Easy";
			public const string Normal		= "Normal";
			public const string Hard		= "Hard";
			public const string Challenging	= "Challenging";
		}
		public ZSkillLevel(ZSkillLevel obj) : base(obj) { }
		public ZSkillLevel(JToken obj) : base(obj) { }
		public ZSkillLevel() { }
		public double novice
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double beginner
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double intermediate
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double advanced
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double expert
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	public class ZMultiplierTypes : ZObjects<ZMultiplierTypes, ZMultiplier>
	{
		public ZMultiplierTypes(ZObject<ZMultiplier> obj) : base(obj) { }
		public ZMultiplierTypes(JToken obj) : base(obj) { }
		public ZMultiplierTypes() { }
	}
#endif

}
