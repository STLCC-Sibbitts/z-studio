#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
#endregion

namespace ZLib.ZRubric
{
	public class ZRubricSchema
	{
		private string m_rubricSchema;
		private JObject m_oSchema;
		public JObject oSchema
		{
			get { return m_oSchema; }
		}
		public ZRubricSchema(string schemaFileName)
		{
			m_rubricSchema = "";
			m_oSchema = null;
			try
			{
				using (StreamReader infile = new StreamReader(schemaFileName))
				{
					m_rubricSchema = infile.ReadToEnd();
				}
				if ((m_oSchema = JObject.Parse(m_rubricSchema)) != null)
				{
					JToken tokProp = m_oSchema.SelectToken("Steps");
					JObject oTok = tokProp as JObject;
					foreach (JProperty val in oTok.Properties())
					{
						string theVal = val.Name + ":" + val.Value.ToString();
					}
				}
			}
			catch (Exception ex)
			{
				string msg = ex.Message;
			}
		}

	}
}
