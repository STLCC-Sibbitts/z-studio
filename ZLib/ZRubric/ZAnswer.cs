
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
#if false
	"Answers":
	[
		{ "Expression":"=IF({^Expression(1)},{^Expression(2)})" }
	]

#endif
	public class ZAnswer : ZObject<ZAnswer>
	{
		public ZAnswer(ZAnswer obj) : base() 
		{
			type = obj.type;

			value = obj.value;
			notes = obj.notes;
			path = obj.path;
			relPath = obj.relPath;
			expression = obj.expression;
			begExpression = obj.begExpression;
			r1c1Expression = obj.r1c1Expression;
			altTaggedExpression = obj.altTaggedExpression;
			taggedExpression = obj.taggedExpression;

		}
		public ZAnswer(ZObject<ZAnswer> obj) : base(obj) { }
		public ZAnswer(JObject jObj) : base(jObj) { }
		public ZAnswer() : base(new JObject()) { }
		public new class Tags : ZObject<ZAnswer>.Tags
		{
			public const string Expression		= "Expression";
			public const string BegExpression	= "BegExpression";
			public const string R1C1Expression	= "R1C1Expression";
		}
		// TODO: Answer has a location
		public string notes
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string path
		{
			get
			{
				string value = GetStringValue(ZLocation.Tags.Path);
				// if the value is not present, look for it at the parent level
				if (value.Length == 0)
					value = GetParentStringValue(ZLocation.Tags.Item + "."  + ZLocation.Tags.Path);
				return value;
			}
			set { SetValue(ZLocation.Tags.Path, value); }
		}
		public string relPath
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string expression
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string begExpression
		{
			get 
			{ 
				return (string)get(MethodInfo.GetCurrentMethod()); 
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string type
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string r1c1Expression
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		// TODO: I may need a way to deal with potentially null and non-string values here
		//public string literalValue
		//{
		//	get { return (string)get(MethodInfo.GetCurrentMethod()); }
		//	set { set(MethodInfo.GetCurrentMethod(), value); }
		//}
		//public string value
		//{
		//	get { return (string)get(MethodInfo.GetCurrentMethod()); }
		//	set { set(MethodInfo.GetCurrentMethod(), value); }
		//}
		public string taggedValue
		{
			get { return (string)GetStringValue(propName(MethodInfo.GetCurrentMethod()),false ); }
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		public string[] preferenceOptions
		{
			get
			{
				string[] value = (string[])get(MethodInfo.GetCurrentMethod());
				if (value == null || value.Length == 0 )
				{
					if (value == null)
					{
						value = new string[0];
					}
					// this makes sure we have resolved the value
				}
				return value;
			}
			set { set(MethodInfo.GetCurrentMethod(), value); }
		}
		// use altTaggedExpression for now, used when expression is subsequently modified
		public string altTaggedExpression
		{
			get
			{
				string[] valueArray = GetStringArrayValue(propName(MethodInfo.GetCurrentMethod()));
				string value = "";
				if (valueArray == null || valueArray.Length == 0)
				{
					if (valueArray == null)
					{
						valueArray = new string[0];
					}
					// this makes sure we have resolved the value
				}
				// now foldup the array into a single string
				foreach (string str in valueArray)
					value += str;

				return value;
			}
			set
			{
				//				JArray jArray = this.SelectToken(propName(MethodInfo.GetCurrentMethod()));
				set(MethodInfo.GetCurrentMethod(), value);
			}
		}
		public string taggedExpression
		{
			get
			{
				string[] valueArray = GetStringArrayValue(propName(MethodInfo.GetCurrentMethod()));
				string value = "";
				if (valueArray == null || valueArray.Length == 0)
				{
					if (valueArray == null)
					{
						valueArray = new string[0];
					}
					// this makes sure we have resolved the value
				}
				// now foldup the array into a single string
				foreach(string str in valueArray)
					value += str;
				value = ZTaskLocs.StrippedText(value);
				return value;
			}
			set 
			{
//				JArray jArray = this.SelectToken(propName(MethodInfo.GetCurrentMethod()));
				set(MethodInfo.GetCurrentMethod(), value); 
			}
		}
	}
	public class ZAnswers : ZObjects<ZAnswers, ZAnswer>
	{
		public ZAnswers(ZObject<ZAnswer> zObject) : base(zObject) { }
		public ZAnswers(JArray jArray) : base(jArray) { }
		public ZAnswers() : base(new JArray()) { }
	}
}
