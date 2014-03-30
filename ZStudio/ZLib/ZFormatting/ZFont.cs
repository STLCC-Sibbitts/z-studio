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
	public class ZFont : ZObject<ZFont>
	{
		public ZFont(ZObject<ZFont> obj)
			: base(obj)
		{
		}
		public ZFont(JObject obj) : base(obj) { }
		public ZFont() { }

		public string name
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string size
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public bool italic
		{
			get { return (bool)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string fontStyle
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public bool strikeThrough
		{
			get { return (bool)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public bool subscript
		{
			get { return (bool)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public bool superscript
		{
			get { return (bool)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string underline
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string color		// TODO: change to ZColorRGB
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}

	public class ZFonts : ZObjects<ZFonts, ZFont>
	{
		public ZFonts(JArray jArray) : base(jArray) { }
		public ZFonts(ZObject<ZFont> zObject) : base(zObject) { }
		public ZFonts() { }
	}

}
