using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;

namespace ZLib.ZRubric
{
	public class ZCategory : ZObject<ZCategory>
	{
		public new class Tags : ZToken.Tags
		{
			public const string nce		= "NCE";
			public const string ee		= "EE";
			public const string lo		= "LO";
		}
	}
	public class ZAllocation : ZObject<ZAllocation>
	{
		public new class Tags : ZToken.Tags
		{
			public const string Pct		= "Pct";
			public const string Max		= "Max";
			public const string PPE		= "PPE";
            public const string Total   = "Total";  // total amount deducted
            public const string Actual  = "Actual"; // actual total, which may exceed total
        }
		public ZAllocation(ZObject<ZAllocation> obj) : base(obj) { }
		public ZAllocation(JObject obj) : base(obj) { }
		public ZAllocation() { }
		public double allocation(string propName)
		{
			return GetDoubleValue(propName);
		}
		public double pct
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double max
		{
			get { return (double)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public double ppe
		{
			get { return GetDoubleValue(Tags.PPE); }
			set { SetValue(Tags.PPE, value); }
		}
        public double total
        {
            get { return (double)get(MethodInfo.GetCurrentMethod()); }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }
        public double actual
        {
            get { return (double)get(MethodInfo.GetCurrentMethod()); }
            set { set(MethodInfo.GetCurrentMethod(), value); }
        }
    }
	public class ZAllocations : ZObjects<ZAllocations, ZAllocation>
	{
		public ZAllocations(JObject rubric) : base(rubric[ZAllocations.itemTag] as JArray) { }
		public ZAllocations(ZObject<ZAllocation> zObject) : base(zObject) { }
		public ZAllocations(JArray jArray) : base(jArray) { }
		public ZAllocations() : base(new JArray()) { }
		public ZAllocation NCE
		{
			get
			{
				ZAllocation val = this[propName(MethodInfo.GetCurrentMethod(),false)]; // ZCategory.Tags.nce];
				return val;
			}
		}
		public ZAllocation EE
		{
			get
			{
				ZAllocation val = this[propName(MethodInfo.GetCurrentMethod(), false)]; // ZCategory.Tags.ee];
				return val;
			}
		}
		public ZAllocation LO
		{
			get
			{
				ZAllocation val = this[propName(MethodInfo.GetCurrentMethod(), false)]; // ZCategory.Tags.lo];
				return val;
			}
		}
	}
}
