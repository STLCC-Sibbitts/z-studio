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
	public class ZFormat : ZObject<ZFormat>
	{

		//public ZFunction(ZFunction arg)
		//	: base((JObject)arg)
		//{
		//	JProperty expressionProp = new JProperty(Tags.Expression, arg.expression);
		//	JProperty textProp = new JProperty(Tags.Text, arg.text);
		//	JObject argObj = new JObject(expressionProp, textProp);
		//	JProperty argProp = new JProperty(arg.id, argObj);
		//	m_jToken = new JObject(argProp);
		//}
		public ZFormat(ZObject<ZFormat> obj)
			: base(obj)
		{
		}
		public ZFormat(JObject obj) : base(obj) { }
		public ZFormat() { }
		public string horizontalAlignment
		{
			get
			{
				string value = (string)get(MethodInfo.GetCurrentMethod());
				if (value == null)
				{
					JToken jTok =this.Properties.First<JToken>().Children().First<JToken>();
					value = (string)((ZToken)(jTok)).get(MethodInfo.GetCurrentMethod());
					if (value == null)
						return "";
					// this makes sure we have resolved the value
				}
				return value;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public bool wordWrap
		{
			get
			{
				bool value = (bool)get(MethodInfo.GetCurrentMethod());
				if (value == null)
				{
					JToken jTok =this.Properties.First<JToken>().Children().First<JToken>();
					value = (bool)((ZToken)(jTok)).get(MethodInfo.GetCurrentMethod());
					if (value == null)
						return false;
					// this makes sure we have resolved the value
				}
				return value;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	public class ZFormats : ZObjects<ZFormats, ZFormat>
	{
		public ZFormats(JArray jArray) : base(jArray) { }
		public ZFormats(ZObject<ZFormat> zObject) : base(zObject) { }
		public ZFormats() { }
	}

}
