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
	public class ZNumberFormat : ZObject<ZNumberFormat>
	{
		public ZNumberFormat(ZObject<ZNumberFormat> obj)
			: base(obj)
		{
		}
		public ZNumberFormat(JObject obj) : base(obj) { }
		public ZNumberFormat() { }

		public string name
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string value
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	public class ZNumberFormats : ZObjects<ZNumberFormats, ZNumberFormat>
	{
		public ZNumberFormats(JArray jArray) : base(jArray) { }
		public ZNumberFormats(ZObject<ZNumberFormat> zObject) : base(zObject) { }
		public ZNumberFormats() { }
	}

}
