#region using
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using System.Diagnostics;
using ZLib;
using ZLib.ZRubric;

#endregion
#if false

{
	"Location":
	{	
		"Type":"Cell",
		"Worksheets":"Order Form", 
		"Cells":"$F$20",
		"Path":"Worksheets[Order Form].Cells[$F$20]" 
	}
}
#endif

namespace ZLib.ZRubric
{
	public class ZTargetRange : ZTarget
	{
		public ZTargetRange(ZTargetRange obj)
			: base()
		{
			text		= obj.text;
			type		= obj.type;
			property	= obj.property;
			location	= new ZLocation(obj.location);
		}
		public ZTargetRange(JObject jObj) : base(jObj) { }
		public ZTargetRange()
			: base(new JObject())
		{
		}
	}
	public class ZTarget : ZTargetSource
	{
		public ZTarget(ZTarget obj) : base()
		{
			text		= obj.text;
			type		= obj.type;
			property	= obj.property;
			location	= new ZLocation(obj.location);
		}
		public ZTarget(JObject jObj) : base(jObj) { }
		public ZTarget()
			: base(new JObject())
		{
		}
        public ZFormat format
        {
            get
            {
                ZFormat value = null;
                value = ZRubric.activeSubmission.GetFormat(this);
                return value;
            }
        }
		public new List<ZContent> content
		{
			get
			{
				object value = ZRubric.activeSubmission.GetContent(this);
				if (value is List<ZContent>)
					return value as List<ZContent>;
				return new List<ZContent>();	// return an empty list
			}
		}
	}
	public class ZSource : ZTargetSource
	{
		public ZSource(ZObject<ZTargetSource> obj) : base(obj) { }
		public ZSource(JObject jObj) : base(jObj) { }
		public ZSource()
			: base(new JObject())
		{
		}
		public new class Tags : ZTargetSource.Tags
		{
			public const string Item		= "Source";
		}
	}
	public class ZTargetSource : ZObject<ZTargetSource>
	{
		public ZTargetSource(ZObject<ZTargetSource> obj) : base(obj) { }
		public ZTargetSource(JObject jObj) : base(jObj) { }
		public ZTargetSource()
			: base(new JObject())
		{
			type = Types.Content;	// default target type
		}

		public new class Tags : ZObject<ZTargetSource>.Tags
		{
			public const string Text		= "Text";
			public const string Type		= "Type";
			public const string Property	= "Property";
		}
		public class Types
		{
			public const string Format		= "Format";
			public const string Content		= "Content";
		}
		public new class Properties
		{
			public const string Formula		= "Formula";
			public const string FormulaR1C1 = "FormulaR1C1";
			public const string Text		= "Text";
			public const string Font		= "Font";
		}
		public string text
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}

		public string type
		{
			get { return GetStringValue(Tags.Type); }
			set { SetValue(Tags.Type, value); }
		}
		public string property
		{
			get { return GetStringValue(Tags.Property); }
			set { SetValue(Tags.Property, value); }
		}
		public ZContent content
		{
			get
			{
				ZContent value = null;
				value = ZRubric.activeSubmission.GetContent(this);
				return value;
			}
		}
		public ZLocation location
		{
			get
			{
				JToken jToken = SelectToken(ZLocation.Tags.Item);
				ZLocation val = null;
				if (jToken == null)
				{
					val = new ZLocation();
					(base.m_jToken as JObject).Add(ZLocation.Tags.Item, (JObject)val);
				}
				else
					val = new ZLocation(jToken as JObject);
				return val;
			}
			set { SetValue(ZLocation.Tags.Item, value); }
		}
	}
	// ZLocation - used to tie a task to the resulting document

	public class ZLocation : ZObject<ZLocation>
	{
		public ZLocation(ZObject<ZLocation> obj) : base(obj) { }
		public ZLocation(JObject jObj) : base(jObj) { }
		public ZLocation(ZLocation location) : base()
		{
			type			= location.type;
			address			= location.address;
			taggedAddress	= location.taggedAddress;
			context			= location.context;
			//taggedContext	= location.taggedContext;
		}
		public ZLocation()
			: base(new JObject())
		{
			type = Types.Cell;	// default location type
		}

		public new class Tags : ZObject<ZLocation>.Tags
		{
			public const string Item		= "Location";
			public const string Items		= Item + "s";
			public const string Type		= "Type";
			public const string Address		= "Address";
			public const string Context		= "Context";
#region old stuff to be removed
			public const string Worksheets	= "Worksheets";
			public const string Cells		= "Cells";
			public const string Path		= "Path";
			public const string RelPath		= "RelPath";
#endregion
		}
		public class Types
		{
			public const string Cell		= "Cell";
			public const string Range		= "Range";
			public const string Worksheet	= "Worksheet";
			public const string Workbook    = "Workbook";
		}
		public string type
		{
			get { return GetStringValue(Tags.Type); }
			set { SetValue(Tags.Type, value); }
		}
		public string qualifiedAddress
		{
			get 
			{
				// TODO this will change based on what's selected
				return context + "." + address; 
			}
		}
		public string address
		{
			get { return GetStringValue(Tags.Address); }
			set { SetValue(Tags.Address, value); }
		}
		public string taggedAddress
		{
			get
			{
				string[] valueArray = (string[])GetStringArrayValue("Tags.Address");
				string value = "";
				
				value = GetStringValue(Tags.Address,false);
#if false
				//if (valueArray == null || valueArray.Length == 0)
				//{
				//	if (valueArray == null)
				//	{
				//		valueArray = new string[0];
				//	}
				//	// this makes sure we have resolved the value
				//}
				//// now foldup the array into a single string
				//foreach (string str in valueArray)
				//	value += str;
				//string theAddress = ResolveStringValue("{^Address}");
				//value = value.Replace("{^Address}",ResolveStringValue("{^Address}"));
#endif
				return value;
			}
			set 
			{
				//SetValue("Tags.Address", value);
			
				JObject jTok = SelectToken("Tags") as JObject;
				if (jTok == null)
				{
					jTok = new JObject();
					(this.m_jToken as JObject).Add("Tags", jTok);
					jTok.Add("Address",value);
				}
				else
					(jTok.SelectToken("Address") as JValue).Value = value;
			}
		}
		public string context
		{
			get { return GetStringValue(Tags.Context); }
			set { SetValue(Tags.Context, value); }
		}
		public string taggedContext
		{
			get
			{
				string value = 	GetStringValue(Tags.Context, false);
				//(string)get(MethodInfo.GetCurrentMethod());
				//if (value == null || value.Length == 0)
				//{
				//	if (value == null)
				//	{
				//		value = new string[0];
				//	}
				//	// this makes sure we have resolved the value
				//}
				return value;
			}
			//set 
			//{
			//	JArray array = SelectToken("TaggedContext") as JArray;
			//	if (array == null)
			//	{
			//		array = new JArray();
			//		(this.m_jToken as JObject).Add("TaggedContext", array);
			//	}
			//	foreach ( String str in value )
			//		array.Add(new JValue(str));
			//}
		}
		#region old stuff to be deleted
		public string worksheets
		{
			get { return GetStringValue(Tags.Worksheets); }
			set { SetValue(Tags.Worksheets, value); }
		}
		public string cells
		{
			get { return GetStringValue(Tags.Cells); }
			set { SetValue(Tags.Cells, value); }
		}
		public static string cellPath(string context, string address, string targetType = "")
		{
			string thePath = string.Format("Worksheets[{0}].Cells[{1}]", context, address);
			if ( targetType.Length > 0 )
				thePath += "." + targetType;
			return thePath;
		}
		public string path
		{
			get 
			{ 
				return cellPath(context, address);
			}
			// set { SetValue(Tags.Path, value); }
		}
		public string relPath
		{
			get { return GetStringValue(Tags.RelPath); }
			set { SetValue(Tags.RelPath, value); }
		}
		#endregion
	}

}
