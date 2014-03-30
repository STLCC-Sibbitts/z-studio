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

namespace ZLib.ZRubric
{
	#region ZProperty
	public class ZProperty : ZObject<ZProperty>
	{
		public ZProperty(ZObject<ZProperty> obj) : base(obj) { }
        public ZProperty(JObject jObj) : base(jObj) { }
        public ZProperty(JToken jObj) : base(jObj) { }
        public ZProperty() { }
	}
	public class ZProperties : ZObjects<ZProperties, ZProperty>
	{
		public ZProperties(JArray jArray) : base(jArray) { }
		public ZProperties(ZObject<ZProperty> zObject) : base(zObject) { }
		public ZProperties() : base(new JArray()) { }
	}
	#endregion
}
