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
	public class ZProject : ZObject<ZProject>
	{
		public new class Tags : ZToken.Tags
		{
			public static string Type				= "Type";
			public static string Description		= "Description";
			public static string TotalPts			= "TotalPts";
			public static string Worksheets			= "Worksheets";
			public static string DocumentFileName	= "File";
		}

		public ZProject(ZObject<ZProject> zToken) : base(zToken) { }
		public ZProject(JToken jToken) : base(jToken) { }
		public ZProject() { }
		public string ontology
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}

		public string description
		{
			get { return GetStringValue(Tags.Description); }
			set { SetValue(Tags.Description, value); }
		}
		public string type
		{
			get { return GetStringValue(Tags.Type); }
			set { SetValue(Tags.Type, value); }
		}
		public double totalPts
		{
			get { return GetDoubleValue(Tags.TotalPts); }
			set { SetValue(Tags.TotalPts, value); }
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
		public double pts(ZMapping mapping)
		{
			// TODO: enable RO 2 LO mapping weights
			double categoryPPE = allocations[mapping.category].ppe;
			double difficultyPct = ZRubric.activePreferences.difficulties.multipliers[mapping.difficulty].factor;
			double value = categoryPPE * difficultyPct;
            return rounded(value);
        }
        public double rounded(double value)
        {
            // factors, to the nearest
            //  00  - no rounding
            //  10  - .10, tenth of a point
            //  25  - .25, quarter point
            //  50  - .50, half point      
            // get rounding factor
			int roundingFactor = ZRubric.activePreferences.rounding.factor*10;
            int intValue = (int) (value * 1000.0);
            int remainder = roundingFactor > 0 ? intValue % (roundingFactor) : 0;
            int multiple = roundingFactor > 0 ? ((remainder / (roundingFactor)) % 1000) : 0;
          
            if (multiple == 0 && intValue < 1000)
                multiple = intValue / roundingFactor;
            intValue /= 1000;    // reset to base value
            double midPoint = roundingFactor / 2.0;
            roundingFactor /= 10;
            decimal up = (decimal)(value * roundingFactor );
            decimal roundedUp = decimal.Round(up);
			// at a minimum, if there was an error, a non-zero value needs to be rounded up to the rounding factor
			// this can be done by having a minimum multiple of one when the remainder is < midpoint
//            value = roundedUp / roundingFactor
            switch (roundingFactor)
            {
                case 0:
                    break;
                case 10:
                    value = (double)Decimal.Round((decimal)(value), 1);
                    break;
                case 25:
                    value = ((double)intValue) + ((multiple + ((remainder >= midPoint || multiple == 0) ? 1 : 0)) * (roundingFactor / 100.0));
                    break;
                case 50:
					value = ((double)intValue) + ((multiple + ((remainder >= midPoint || multiple == 0) ? 1 : 0)) * (roundingFactor / 100.0));
                    break;
                default:
                    break;
            }

            return value;
        }
		public double pts(ZMapping mapping, ZDeduction deduction)
		{
			if ( deduction.type == "None" )
				return 0;
            double deductionPct = ZRubric.activePreferences.deductions.multipliers[deduction.type].factor; 
//			double categoryPPE = allocations[deduction.category].ppe;
            double deductionPts = pts(mapping);
            double value = deductionPts * deductionPct;
			return rounded(value);
		}
		public string documentFileName
		{
			get { return GetStringValue(Tags.DocumentFileName); }
			set { SetValue(Tags.DocumentFileName, value); }
		}
		public ZAllocations allocations
		{
			get
			{
				JArray jArray = m_jToken.Value<JArray>(ZAllocations.itemTag);
				ZAllocations val =  new ZAllocations(jArray);

				if (val == null)
				{
					val = new ZAllocations();
					(m_jToken as JObject).Add(ZAllocations.itemTag, (JArray)val);
				}
				return val;
			}
		}
		public ZMultipliers multipliers
		{
			get
			{
                ZMultipliers val = null; // (ZMultipliers)m_jToken.Value<JToken>(ZMultipliers.itemTag);
				if (val == null)
				{
					val = new ZMultipliers();
					(m_jToken as JObject).Add(ZMultipliers.itemTag, (JArray)val);
				}
				return val;
			}
		}
		public ZPreferences preferences
		{
			get
			{
                ZPreferences val = null; // (ZPreferences)m_jToken.Value<JToken>(ZPreferences.itemTag);
				if (val == null)
				{
					val = new ZPreferences();
					(m_jToken as JObject).Add(ZPreferences.itemTag, (JArray)val);
				}
				return val;
			}
		}
	}
}
