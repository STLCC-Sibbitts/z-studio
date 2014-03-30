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
	public class ZTag : ZObject<ZTag>
	{

		public ZTag(ZObject<ZTag> obj)
			: base(obj)
		{
		}
		public ZTag(JArray obj) : base(obj as JToken) { }
		public ZTag() { }
		public new string id
		{
			get
			{
				string value = base.id;
				if (value == "" || value == null)
				{
					JToken jTok =this.Properties.First<JToken>().Children().First<JToken>();
					value = ZToken.OID(jTok);
				}

				return value;
			}
		}
		public string this[int index]
		{
			get
			{
				string tag = string.Format("requestedTag[{0:D}]", index);
				string[] splitTag = null;
				JToken jTok = null;
				JArray tagArray = null;;
				if (m_jToken.Children().Count<JToken>() > 0)
				{
					jTok = m_jToken.Children().First<JToken>();
					if ( jTok.Type != JTokenType.Array && jTok.Children().Count<JToken>() > 0)
						jTok = jTok.Children().First<JToken>();
				}
				if (jTok != null && jTok.Type == JTokenType.Array)
				{
					tagArray = jTok as JArray;
					tag = (string)tagArray[0];	// right now everything is in the first one
					if (index >= 0)
					{
						splitTag = tag.Split('\t');
						if (index < splitTag.Length)
							tag = splitTag[index];
						else
							tag =string.Format("NFND[{0:D}]{1}", index, tag);
					}
				}
				return tag;
			}
		}
	}
	public class ZTags : ZObjects<ZTags, ZTag>
	{
		public ZTags(JArray jArray) : base(jArray) { }
		public ZTags(ZObject<ZTag> zObject) : base(zObject) { }
		public ZTags() { }
	}

}
