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
using ZLib.ZRubric;

namespace ZLib
{
	#region Function
	public class ZFunction : ZObject<ZFunction>
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
		public class Types
		{
			public const string Numeric         = "Numeric";
			public const string String			= "String";
			public const string Value			= "Value";
		}
		//public ZFunction(ZFunction arg)
		//	: base((JObject)arg)
		//{
		//	JProperty expressionProp = new JProperty(Tags.Expression, arg.expression);
		//	JProperty textProp = new JProperty(Tags.Text, arg.text);
		//	JObject argObj = new JObject(expressionProp, textProp);
		//	JProperty argProp = new JProperty(arg.id, argObj);
		//	m_jToken = new JObject(argProp);
		//}
		public ZFunction(ZObject<ZFunction> obj)
			: base(obj)
		{
		}
		public ZFunction(JObject obj) : base(obj) { }
		public ZFunction() { }
		public new string id
		{
			get
			{
				string value = base.id;
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
		public string type
		{
			get
			{
				string value = (string)get(MethodInfo.GetCurrentMethod());
				if (value == null)
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
		public ZFunctionArgs functionArgs
		{
			get
			{
				ZFunctionArgs val = null;
				JToken jToken = this[ZFunctionArgs.itemTag];
				if (jToken == null)
					jToken = SelectToken(ZFunctionArgs.itemTag);
				if (jToken == null)
				{
					if (!(base.m_jToken as JObject).TryGetValue(ZFunctionArgs.itemTag, out jToken))
					{
						val = new ZFunctionArgs();
						(base.m_jToken as JObject).Add(ZFunctionArgs.itemTag, (JToken)val);
					}
				}
				val = new ZFunctionArgs((JArray)jToken);
				return val;
			}
			set { functionArgs = value; }
		}
	}
	public class ZFunctions : ZObjects<ZFunctions, ZFunction>
	{
		public ZFunctions(JArray jArray) : base(jArray) { }
		public ZFunctions(ZObject<ZFunction> zObject) : base(zObject) { }
		public ZFunctions() { }
	}

#endregion

#region FunctionArg

	public class ZFunctionArg : ZObject<ZFunctionArg>
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
		public class Types
		{
			public const string Range           = "range";
			public const string Criteria		= "criteria";
			public const string Expression		= "Expression";
			public const string Numeric			= "Numeric";
			public const string String			= "String";
			public const string Boolean			= "Boolean";
		}
		//public ZFunctionArg(ZFunctionArg arg)
		//	: base((JObject)arg)
		//{
		//	JProperty expressionProp = new JProperty(Tags.Expression, arg.expression);
		//	JProperty textProp = new JProperty(Tags.Text, arg.text);
		//	JObject argObj = new JObject(expressionProp, textProp);
		//	JProperty argProp = new JProperty(arg.id, argObj);
		//	m_jToken = new JObject(argProp);
		//}
		public ZFunctionArg(ZObject<ZFunctionArg> obj)
			: base(obj)
		{
		}
		public ZFunctionArg(JObject obj) : base(obj) { }
		public ZFunctionArg() { }
		public new string id
		{
			get
			{
				string value = base.id;
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
		public string type
		{
			get
			{
				string value = (string)get(MethodInfo.GetCurrentMethod());
				if (value == null)
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
		public string text
		{
			get
			{
				string value = (string)get(MethodInfo.GetCurrentMethod());
				if (value == null)
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
		public bool required
		{
			get
			{
				return (bool)get(MethodInfo.GetCurrentMethod());
			}
		}
		public bool outputValue
		{
			get
			{
				bool	value = false;
				value = (bool)get(MethodInfo.GetCurrentMethod());
				return value;
			}
		}
		//public string[] preferenceOptions
		//{
		//	get
		//	{
		//		string[] value = (string[])get(MethodInfo.GetCurrentMethod());
		//		if (value == null)
		//			return new string[0];
		//		// this makes sure we have resolved the value
		//		return value;
		//	}
		//	set { set(MethodInfo.GetCurrentMethod(), value); }
		//}
		public string notes
		{
			get
			{
				return (string)get(MethodInfo.GetCurrentMethod());
			}
		}
		public string defaultValue
		{
			get
			{
				JToken jTok = SelectToken("DefaultValue");
				JValue jVal = jTok as JValue;
				string sVal = (jVal==null?"": jVal.ToString());
				if (jVal != null && jVal.Type == JTokenType.Boolean )
					sVal = sVal.ToUpper();
//				object value = (object)get(MethodInfo.GetCurrentMethod());
//				string returnValue = value.ToString();
				return sVal;
			}
		}
		public ZExprNode defaultExprNode
		{
			get
			{
				JToken jTok = SelectToken("DefaultValue");
				JValue jVal = GetJValue("DefaultValue");
				ZExprNode exprNode = new ZExprNode(defaultValue, "DefaultValue",true);
				if ( type == "range" )
					exprNode.nodeType = ZExprNode.ZNodeType.zRange;
				else if ( type == "cell" )
					exprNode.nodeType = ZExprNode.ZNodeType.zCell;
				else if (jTok != null && jTok.Type == JTokenType.Boolean)
					exprNode.nodeType = ZExprNode.ZNodeType.zBoolLiteral;
				else if (jTok != null && jTok.Type == JTokenType.Integer || jTok != null && jTok.Type == JTokenType.Float)
					exprNode.nodeType = ZExprNode.ZNodeType.zNumericLiteral;
				else
					exprNode.nodeType = ZExprNode.ZNodeType.zValue;
				return exprNode;
			}
		}
	}
	public class ZFunctionArgs : ZObjects<ZFunctionArgs, ZFunctionArg>
	{
		public ZFunctionArgs(JArray jArray) : base(jArray) { }
		public ZFunctionArgs(ZObject<ZFunctionArg> zObject) : base(zObject) { }
		public ZFunctionArgs() { }
	}
	#endregion

}
