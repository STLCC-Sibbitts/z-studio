using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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
	public class ZSubmission : ZObject<ZSubmission>
	{
		public new class Tags : ZToken.Tags
		{
			public static string Type				= "Type";
			public static string Description		= "Description";
			public static string Worksheets			= "Worksheets";
		}

		public ZSubmission(ZObject<ZSubmission> zToken) : base(zToken) { }
		public ZSubmission(JToken jToken) : base(jToken) { }
		public ZSubmission() { }
		public ZContent GetContent(ZTarget target)
		{
			ZContent	content = null;
			JToken	jTok = null;
			JObject oRoot = m_jToken as JObject;
			//JToken jTokKeys = oRoot.SelectToken("Keys");

			string	path = string.Format("{0}.{1}", target.location.path, target.type);
			jTok = oRoot.SelectToken(path);	// .First.Value<JObject>();
			if ( jTok != null )
				content = new ZContent((JObject)jTok);
			return content;
		}
        public ZFormat GetFormat(ZTarget target)
        {
            ZFormat format = null;
            JToken jTok = null;
            JObject oRoot = m_jToken as JObject;
            //JToken jTokKeys = oRoot.SelectToken("Keys");

            string path = string.Format("{0}.{1}", target.location.path, target.type);
            jTok = oRoot.SelectToken(path);	// .First.Value<JObject>();
            if (jTok != null)
                format = new ZFormat((JObject)jTok);
            return format;
        }
        public string type
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public ZAllocations allocations
		{
			get
			{
				JToken jToken = base.m_jToken[ZAllocations.itemTag];
				ZAllocations val = null;
				if (jToken == null)
				{
					val = new ZAllocations();
					(base.m_jToken as JObject).Add(ZAllocations.itemTag, (JArray)val);
				}
				else
					val = new ZAllocations((JArray)jToken);
				return val;
			}
			set 
			{ 
				ZAllocations tmpAll = allocations;
				base.m_jToken[ZAllocations.itemTag] = (JArray)value;
//				allocations = value; 
			}
		}

		public ZTaskDeductions taskDeductions
		{
			get
			{
				JToken jToken = SelectToken(ZTaskDeductions.itemTag);
				ZTaskDeductions val = null;
				if (jToken == null)
				{
					val = new ZTaskDeductions();
					(base.m_jToken as JObject).Add(ZTaskDeductions.itemTag, (JArray)val);
				}
				else
					val = new ZTaskDeductions((JArray)jToken);
				return val;
			}
			set { taskDeductions = value; }
		}
		public JArray worksheets
		{
			get
			{
				JArray val = (JArray)(m_jToken[Tags.Worksheets]);
				if (val == null)
				{
					val = new JArray();
					(m_jToken as JObject).Add(Tags.Worksheets, (JArray)val);
				}
				return val;
			}
			set
			{
				JArray jVal = base.m_jToken[Tags.Worksheets] as JArray;
				// if we don't have it, add it
				base.m_jToken[Tags.Worksheets] = (JArray)value;
			}
		}
	}
}
