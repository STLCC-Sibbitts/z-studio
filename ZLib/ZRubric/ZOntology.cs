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
using System.Reflection;

namespace ZLib.ZRubric
{

	#region ObjectiveResource

	public class ZObjectiveResource : ZObject<ZObjectiveResource>
	{
		public new class Tags : ZToken.Tags
		{
			public const string NullValue		= "<NULL>";
		}
		public ZObjectiveResource(ZObject<ZObjectiveResource> obj)
			: base(obj)
		{
		}
		public ZObjectiveResource(JObject obj) : base(obj) { }
		public ZObjectiveResource() { }
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
	}
	public class ZObjectiveResources : ZObjects<ZObjectiveResources, ZObjectiveResource>
	{
		public ZObjectiveResources(JArray jArray) : base(jArray) { }
		public ZObjectiveResources(ZObject<ZObjectiveResource> zObject) : base(zObject) { }
		public ZObjectiveResources() { }
	}
	#endregion


	#region ObjectiveMapping

	public class ZObjectiveMapping : ZObject<ZObjectiveMapping>
	{
		public new class Tags : ZToken.Tags
		{
			public const string Type            = "Type";
		}
		public class Types
		{
			public const string Training		= "Training";
		}
		public ZObjectiveMapping(ZObject<ZObjectiveMapping> obj)
			: base(obj)
		{
		}
		public ZObjectiveMapping(JObject obj) : base(obj) { }
		public ZObjectiveMapping() { }
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
						return ZToken.NULL_VALUE;
					// this makes sure we have resolved the value
				}

				return value;
			}
		}
		public new string name
		{
			get
			{
				string value = (string)get(MethodInfo.GetCurrentMethod(),true);
				return value;
			}
		}
		public string type
		{
			get
			{
				string value = (string)get(MethodInfo.GetCurrentMethod(),true);
				return value;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
	}
	public class ZObjectiveMappings : ZObjects<ZObjectiveMappings, ZObjectiveMapping>
	{
		public ZObjectiveMappings(JArray jArray) : base(jArray) { }
		public ZObjectiveMappings(ZObject<ZObjectiveMapping> zObject) : base(zObject) { }
		public ZObjectiveMappings() { }
		//TODO: need to be able to grab all objective mappings, like EX281
		public new List<ZObjectiveMapping> this[string tag]
		{
			get
			{
				List<ZObjectiveMapping> val = new List<ZObjectiveMapping>();
				JArray array = m_jToken as JArray;
				var mappings = array.Children().Values(tag);

				foreach (var mapping in mappings)
				{
					JObject job = mapping.Parent.Parent as JObject;
					ZObjectiveMapping om = new ZObjectiveMapping(job);
					string theType = om.type;
					theType = om.name;
					val.Add(new ZObjectiveMapping(job));
				}
				return val;
			}
		}

	}
	#endregion

	#region Objective

	public class ZObjective : ZObject<ZObjective>
	{
		public new class Tags : ZToken.Tags
		{
			public const string Text            = "Text";
			public const string NullValue		= "<NULL>";
		}
		public ZObjective(ZObject<ZObjective> obj)
			: base(obj)
		{
		}
		public ZObjective(JObject obj) : base(obj) { }
		public ZObjective() { }
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
	}
	public class ZObjectives : ZObjects<ZObjectives, ZObjective>
	{
		public ZObjectives(JArray jArray) : base(jArray) { }
		public ZObjectives(ZObject<ZObjective> zObject) : base(zObject) { }
		public ZObjectives() { }
	}
	#endregion

	#region ResourceProvider

	public class ZResourceProvider : ZObject<ZResourceProvider>
	{
		public new class Tags : ZToken.Tags
		{
			public const string Text            = "Text";
			public const string NullValue		= "<NULL>";
		}
		public ZResourceProvider(ZObject<ZResourceProvider> obj)
			: base(obj)
		{
		}
		public ZResourceProvider(JObject obj) : base(obj) { }
		public ZResourceProvider() { }
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
		public ZObjectiveMappings objectiveMappings
		{
			get
			{
				ZObjectiveMappings val = null;
				JToken jToken = this[ZObjectiveMappings.itemTag];
				if (jToken == null)
					jToken = SelectToken(ZObjectiveMappings.itemTag);
				if (jToken == null)
				{
					if (!(base.m_jToken as JObject).TryGetValue(ZObjectiveMappings.itemTag, out jToken))
					{
						val = new ZObjectiveMappings();
						(base.m_jToken as JObject).Add(ZObjectiveMappings.itemTag, (JToken)val);
					}
				}
				val = new ZObjectiveMappings((JArray)jToken);
				return val;
			}
			set { objectiveMappings = value; }
		}
		public ZObjectiveResources Resources(string resourceType)
		{
			ZObjectiveResources val = null;
			JToken jTok = SelectToken("ObjectiveResources[" + resourceType + "]");
			//HACK: fix this later
			JToken jResources = jTok.First.First;
			if ( jTok != null && jTok.First.First is JArray)
			{
				val = new ZObjectiveResources(jTok.First.First as JArray);
			}
			return val;
		}
	}
	public class ZResourceProviders : ZObjects<ZResourceProviders, ZResourceProvider>
	{
		public ZResourceProviders(JArray jArray) : base(jArray) { }
		public ZResourceProviders(ZObject<ZResourceProvider> zObject) : base(zObject) { }
		public ZResourceProviders() { }
		public new ZResourceProvider this[string tag]
		{
			get
			{

				ZResourceProvider val = null;
				JArray array = m_jToken as JArray;
				var items = array.Children().Values(tag);

				foreach (var item in items)
				{
					JObject job = item.Parent.Parent as JObject;
					val = new ZResourceProvider(job);
					string theType = val.name;
					theType = val.id;
				}
				return val;
			}
		}

	}
	#endregion

	
	public class ZOntology : ZObject<ZOntology>
	{
		public new class Tags : ZToken.Tags
		{
			public const string NullValue		= "<NULL>";
		}
		public ZOntology(ZObject<ZOntology> obj)
			: base(obj)
		{
		}
		public ZOntology(JObject obj) : base(obj) { }
		public ZOntology() { }
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
		public ZObjectives objectives
		{
			get
			{
				ZObjectives val = null;
				JToken jToken = this[ZObjectives.itemTag];
				if (jToken == null)
					jToken = SelectToken(ZObjectives.itemTag);
				if (jToken == null)
				{
					if (!(base.m_jToken as JObject).TryGetValue(ZObjectives.itemTag, out jToken))
					{
						val = new ZObjectives();
						(base.m_jToken as JObject).Add(ZObjectives.itemTag, (JToken)val);
					}
				}
				val = new ZObjectives((JArray)jToken);
				return val;
			}
			set { objectives = value; }
		}
		public ZResourceProviders resourceProviders
		{
			get
			{
				ZResourceProviders val = null;
				JToken jToken = this[ZResourceProviders.itemTag];
				if (jToken == null)
					jToken = SelectToken(ZResourceProviders.itemTag);
				if (jToken == null)
				{
					if (!(base.m_jToken as JObject).TryGetValue(ZResourceProviders.itemTag, out jToken))
					{
						val = new ZResourceProviders();
						(base.m_jToken as JObject).Add(ZResourceProviders.itemTag, (JToken)val);
					}
				}
				val = new ZResourceProviders((JArray)jToken);
				return val;
			}
			set { resourceProviders = value; }
		}

	}
	public class ZOntologies : ZObjects<ZOntologies, ZOntology>
	{
		public ZOntologies(JObject rubric) : base(rubric[ZOntologies.itemTag] as JArray) { }
		public ZOntologies(ZObjects<ZOntologies, ZOntology> zObject) : base(zObject) { }
		public ZOntologies(JArray jArray) : base(jArray) { }
		public ZOntologies() { }
	}

}
