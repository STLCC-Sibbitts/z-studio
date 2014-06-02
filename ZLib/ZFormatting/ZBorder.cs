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
	public class ZBorder : ZObject<ZBorder>
	{
		public ZBorder(ZObject<ZBorder> obj)
			: base(obj)
		{
		}
		public ZBorder(JObject obj) : base(obj) { }
		public ZBorder() { }
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
						return "";
					// this makes sure we have resolved the value
				}

				return value;
			}
		}
		public new string name
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string lineStyle
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string weight
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string colorIndex
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string rGBColor		// TODO: change to ZColorRGB
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	public class ZBorders : ZObjects<ZBorders, ZBorder>
	{
		public ZBorders(JArray jArray) : base(jArray) { }
		public ZBorders(ZObject<ZBorder> zObject) : base(zObject) { }
		public ZBorders() { }
	}

}
