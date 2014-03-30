#region using
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
#endregion

namespace ZLib.ZRubric
{
	// 	{ "Name":"Neatness", "Weight":0.05, "Units":0, "Pts":2.5, "PtsPerUnit": 0.0 	},
	public class ZWeightCategory : ZObject<ZWeightCategory>
	{
		public new class Tags : ZObject<ZWeightCategory>.Tags
		{
			public static string Pts		= "Pts";
			public static string Item		= "Weight";
			public static string Items		= Item + "s";
			public static string Units		= "Units";
		}
		public class Categories
		{
			public static string Primary	= "Primary";
			public static string Neatness	= "Neatness";
			public static string Accuracy	= "Accuracy";
		}
		public ZWeightCategory(ZObject<ZWeightCategory> obj) : base(obj) { }
		public ZWeightCategory(JObject jObj) : base(jObj) { }
		public ZWeightCategory( ) : base(new JObject())
		{
			Initialize();
		}

		public ZWeightCategory(string name, double weight, int units, double pts) : base(new JObject())
		{
			Initialize();
			this.name = name;
			this.weight = weight;
			this.units = units;
			this.pts = pts;
		}
		public override void Initialize()
		{
			// set default values
			name = "";
			weight = 0.0;
			units = 0;
			pts = 0.0;
		}
		public double pts
		{
			get { return GetDoubleValue(Tags.Pts); }
			set { SetValue(Tags.Pts, value); }
		}
		public double weight
		{
			get { return GetDoubleValue(Tags.Item); }
			set { SetValue(Tags.Item, value); }
		}
		public double units
		{
			get { return GetDoubleValue(Tags.Units); }
			set { SetValue(Tags.Units, value); }
		}

		public double ptsPerUnit
		{
			get { return pts / units; }
		}

	}
	public class ZWeightCategories : ZObjects<ZWeightCategories, ZWeightCategory>
	{
		public ZWeightCategories(JObject rubric) : base(rubric[ZWeightCategory.Tags.Items] as JArray) { }
		public ZWeightCategories(JArray jArray) : base(jArray) { }
		public ZWeightCategories() : base(new JArray()) { }
	}
}
