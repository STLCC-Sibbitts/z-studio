using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;

namespace ZLib.ZRubric
{
    public class ZArg : ZObject<ZArg>
    {
        //{	"Range_lookup": {
        //    "Expression":null,
        //    "DefaultValue":"TRUE",
        //    "Text":"e. Verify that the formula syntax is =VLOOKUP(H4,Bonus!A4:B8,2)."
        //}}

        public new class Tags : ZToken.Tags
        {
            public const string Text            = "Text";
            public const string Expression      = "Expression";
            public const string DefaultValue    = "DefaultValue";
			public const string NullValue		= "<NULL>";
        }
		public ZArg(ZArg arg)
			: base((JObject)arg)
		{
			JProperty expressionProp = new JProperty(Tags.Expression, arg.expression);
			JProperty textProp = new JProperty(Tags.Text, arg.text);
			JObject argObj = new JObject(expressionProp, textProp);
			JProperty argProp = new JProperty(arg.id, argObj);
			m_jToken = new JObject( argProp);
		}
        public ZArg(ZObject<ZArg> obj) : base(obj) 
		{
		}
        public ZArg(JObject obj) : base(obj) { }
        public ZArg() { }
		public new string id
		{
			get
			{
				string	value = base.id;
				if (value == "" || value == null)
				{
					JToken jTok =this.Properties.First<JToken>().Children().First<JToken>();
					value = ZToken.OID(jTok);
					if (value == null)
						return Tags.NullValue;
					// this makes sure we have resolved the value
				}

				return value;
			}
		}
		public string text
		{
			get 
			{
				string value = (string)get(MethodInfo.GetCurrentMethod());
				if ( value == null)
				{
					JToken jTok =this.Properties.First<JToken>().Children().First<JToken>();
					value = (string)((ZToken)(jTok)).get(MethodInfo.GetCurrentMethod());
					if (value == null)
						return Tags.NullValue;
					// this makes sure we have resolved the value
				}
				return value; 
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string[] preferenceOptions
		{
			get
			{
				string[] value = (string[])get(MethodInfo.GetCurrentMethod());
				if (value == null)
					return new string[0];
				// this makes sure we have resolved the value
				return value;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		// TODO: I may need a way to deal with potentially null and non-string values here
        public string expression
        {
			get
			{
				string value = (string)get(MethodInfo.GetCurrentMethod());
				if (value == "" || value == null)
				{
					JToken jTok =this.Properties.First<JToken>().Children().First<JToken>();
					value = (string)((ZToken)(jTok)).get(MethodInfo.GetCurrentMethod());
					if (value == null)
						return Tags.NullValue;
					// this makes sure we have resolved the value
				}
				return value;  
			}
            set { SetValue(Tags.Expression, value); }
        }
		// TODO: I may need a way to deal with potentially null and non-string values here
		public string defaultValue
        {
			get 
			{ 
				return (string)get(MethodInfo.GetCurrentMethod()); 
			}
		}
    }
	public class ZArgs : ZObjects<ZArgs, ZArg>
	{
		public ZArgs(JArray jArray) : base(jArray) { }
		public ZArgs(ZObject<ZArg> zObject) : base(zObject) { }
		public ZArgs() { }
	}

	//public class ZArgs : ZObjects<ZArgs, ZArg>
	//{
	//	public ZArgs(JObject rubric) : base(rubric[ZArgs.itemTag] as JArray) { }
	//	public ZArgs(ZObject<ZArg> zObject) : base(zObject) { }
	//	public ZArgs(JArray jArray) : base(jArray) { }
	//	public ZArgs() : base(new JArray()) { }
	//}

}
