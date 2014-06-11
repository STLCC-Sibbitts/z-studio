
using System;
using System.Diagnostics;
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
using ZLib;


namespace ZLib.ZRubric
{
	#region ZCellRangeStuff

	public class ZCellRefEnumerator : IEnumerator
	{
		protected int m_index;
		protected ZRangeRef m_zRange;
		public ZCellRefEnumerator(ZRangeRef zRange)
		{
			m_zRange = zRange;
			Reset();
		}
		public void Reset()
		{
			m_index = -1;
		}
		public object Current
		{
			get
			{
				// TODO: fix this so it doesn't mess it up and require so much work
				ZCellRef value = m_zRange[m_index];
				return value;
			}
		}

		public bool MoveNext()
		{
			return (++m_index < m_zRange.Count);
		}
	}
	public class RXRangeFields
	{
		public const string SheetName		= "sheetName";
		public const string AbsoluteColumn	= "absCol";
		public const string ColumnName		= "colName";
		public const string AbsoluteRow		= "absRow";
		public const string RowNumber		= "rowNumber";
	}
	public class RXCellPatterns
	{
		public const string sheetName = @"((^(?<" + RXRangeFields.SheetName + @">[\w\d_]+)\!)?)";
		public const string absoluteColumn = @"((?<" + RXRangeFields.AbsoluteColumn + @">\$)?)";
		public const string columnName = @"((?<" + RXRangeFields.ColumnName + @">[A-Za-z]+))";
		public const string absoluteRow = @"((?<" + RXRangeFields.AbsoluteRow + @">\$)?)";
		public const string rowNumber = @"((?<" + RXRangeFields.RowNumber + @">\d+))";
	}
	public class ZRangeRef : IEnumerable
	{
		public static bool TryParse(string cellRangeRef, out ZRangeRef rangeRef)
		{
			rangeRef = new ZRangeRef(cellRangeRef);
			if (rangeRef.m_begCellRef == null)
			{
				rangeRef = null;
				return false;
			}
			return true;
		}
		public ZRangeRef(string cellRangeRef)
		{
			m_begCellRef = null;
			m_endCellRef = null;
			Match begCellMatch = ZCellRef.TryMatch(cellRangeRef);
			Match endCellMatch = null;
			if (begCellMatch.Success)
			{
				string crr = "";
				if (begCellMatch.Length < cellRangeRef.Length)
				{
					crr = cellRangeRef.Substring(begCellMatch.Length);
					if (crr.StartsWith(":"))
					{
						endCellMatch = ZCellRef.TryMatch(crr);
						m_begCellRef = new ZCellRef(begCellMatch);
						m_endCellRef = new ZCellRef(endCellMatch);
					}
				}
			}
		}
		public override string ToString()
		{
			return m_begCellRef.ToString() + ":" + m_endCellRef.ToString();
		}
		protected ZCellRef m_begCellRef;
		protected ZCellRef m_endCellRef;
		public ZCellRef begCellRef
		{
			get { return m_begCellRef; }
			set { m_begCellRef = value; }
		}
		public ZCellRef endCellRef
		{
			get { return m_endCellRef; }
			set { m_endCellRef = value; }
		}

		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)new ZCellRefEnumerator(this);
		}

		public ZCellRef this[int index]
		{
			get
			{
				ZCellRef cellRef = new ZCellRef(m_begCellRef);
				int rowOffset = index / ColCount;
				int colOffset = index % ColCount;
				cellRef.rowNumber += rowOffset;
				cellRef.columnIndex += colOffset;
				return cellRef;
			}
		}
		public int RowCount
		{
			get
			{
				int value = m_endCellRef.rowNumber - m_begCellRef.rowNumber + 1;
				return value;
			}
		}
		public int ColCount
		{
			get
			{
				int value = m_endCellRef.columnIndex - m_begCellRef.columnIndex + 1;
				return value;
			}
		}
		public int Count
		{
			get
			{
				int value = RowCount * ColCount;
				return value;
			}
		}
	}
	public class ZCellRef
	{
		protected string m_sheetName;
		protected bool m_absoluteColumn;
		protected int m_columnIndex;
		protected bool m_absoluteRow;
		protected int m_rowNumber;
		public static Match TryMatch(string cellRange)
		{
			Match m = Regex.Match(cellRange, RXCellPatterns.sheetName
						+ RXCellPatterns.absoluteColumn 
						+ RXCellPatterns.columnName
						+ RXCellPatterns.absoluteRow
						+ RXCellPatterns.rowNumber);
			return m;
		}
		public static bool TryParse(string cellRange, out ZCellRef cellRef)
		{
			cellRef = null;
			string results = cellRange;
			// test resolving nested 
			Match m = TryMatch(cellRange);
			if (m.Success)
			{
				cellRef = new ZCellRef(m);
				//cellRef.m_sheetName		= m.Groups[RXRangeFields.SheetName].Value;
				//cellRef.m_absoluteColumn= (m.Groups[RXRangeFields.AbsoluteColumn].Value == "$");
				//string columnName		= m.Groups[RXRangeFields.ColumnName].Value;
				//cellRef.m_absoluteRow	= (m.Groups[RXRangeFields.AbsoluteRow].Value == "$");
				//cellRef.m_rowNumber		= int.Parse(m.Groups[RXRangeFields.RowNumber].Value);
				//cellRef.m_columnIndex = 0;
				//foreach (char ch in columnName.ToUpper().ToCharArray() )
				//{
				//	cellRef.m_columnIndex = (cellRef.m_columnIndex * 26) + (ch - 'A' + 1);
				//}
			}
			return m.Success;
		}
		public void Initialize(Match m)
		{
			if (m.Success)
			{
				m_sheetName		= m.Groups[RXRangeFields.SheetName].Value;
				m_absoluteColumn= (m.Groups[RXRangeFields.AbsoluteColumn].Value == "$");
				columnName		= m.Groups[RXRangeFields.ColumnName].Value;
				m_absoluteRow	= (m.Groups[RXRangeFields.AbsoluteRow].Value == "$");
				m_rowNumber		= int.Parse(m.Groups[RXRangeFields.RowNumber].Value);
			}
		}
		public ZCellRef(ZCellRef cellRef)
		{
			m_sheetName		= cellRef.m_sheetName;
			m_absoluteColumn= cellRef.m_absoluteColumn;
			m_columnIndex	= cellRef.m_columnIndex;
			m_absoluteRow	= cellRef.m_absoluteRow;
			m_rowNumber		= cellRef.m_rowNumber;
		}

		public ZCellRef(Match m)
		{
			Initialize(m);
		}
		public ZCellRef(string cellRef)
		{
			Match m = TryMatch(cellRef);
			Initialize(m);
		}
		public ZCellRef()
		{
		}
		public string columnName
		{
			get
			{
				string value = "";
				int col = m_columnIndex;
				while (col > 0)
				{
					string ch = char.ConvertFromUtf32(char.ConvertToUtf32("A", 0) + ((col-1) % 26));
					value = ch  + value;
					col /= 26;
				}
				return value;
			}
			set
			{
				m_columnIndex = 0;
				foreach (char ch in value.ToUpper().ToCharArray())
				{
					m_columnIndex = (m_columnIndex * 26) + (ch - 'A' + 1);
				}
			}
		}
		public bool isRelativeReference
		{
			get { return (m_absoluteRow == false && m_absoluteColumn == false); }
		}
		public bool isAbsoluteReference
		{
			get { return (m_absoluteRow == true && m_absoluteColumn == true); }
		}
		public bool isMixedReference
		{
			get { return !(isRelativeReference || isAbsoluteReference); }
		}

		public ZCellRef ToAbsolute()
		{
			ZCellRef cellRef = new ZCellRef(this);
			cellRef.m_absoluteColumn = true;
			cellRef.m_absoluteRow = true;
			return cellRef;
		}
		public ZCellRef ToRelative()
		{
			ZCellRef cellRef = new ZCellRef(this);
			cellRef.m_absoluteColumn = false;
			cellRef.m_absoluteRow = false;
			return cellRef;
		}
		public override string ToString()
		{
			string value = "";
			value = string.Format("{0}{1}{2}{3}{4}",
				(m_sheetName.Length>0?(m_sheetName+"!"):""),
				(absoluteColumnReference?"$":""),
				columnName,
				(absoluteRowReference?"$":""), m_rowNumber);

			return value;
		}
		public bool absoluteColumnReference
		{
			get { return m_absoluteColumn; }
			set { m_absoluteColumn = value; }
		}
		public bool absoluteRowReference
		{
			get { return m_absoluteRow; }
			set { m_absoluteRow = value; }
		}
		public int columnIndex
		{
			get { return m_columnIndex; }
			set { m_columnIndex = value; }
		}
		public int rowNumber
		{
			get { return m_rowNumber; }
			set { m_rowNumber = value; }
		}
		public string sheetName
		{
			get { return m_sheetName; }
			set { m_sheetName = value; }
		}
	}

	#endregion

	#region ZObjects
	public class ZObjects<A, T> : ZObject<T>, IEnumerable where T : ZObject<T>, new() //, INotifyPropertyChanged 
	{
		public ZObjects() : base(new JArray()) { }
		public ZObjects(ZObject<T> zObject) : base(zObject) { }
		//		public ZObjects(ZObjects<A,T> zObject) : base(zObject) { }
		public ZObjects(JToken jToken) : base(jToken) { }
		public ZObjects(JArray jArray) : base(jArray) { }
		public override void Initialize()
		{
			base.Initialize();
			if (m_jToken != null && m_jToken.Type != JTokenType.Array)
				throw (new ApplicationException("oops, we didn't get an array"));
		}
		public int Count
		{
			get { return m_jToken == null ? 0 :  m_jToken.Children().Count(); }
		}
		public IEnumerator GetEnumerator()
		{
			ZEnumerator<T> zEnumerator = new ZEnumerator<T>();
			zEnumerator.Initialize(this);
			return zEnumerator;
		}
		public static implicit operator ZObjects<A, T>(JArray jArray)
		{
			ZObjects<A, T> zObj = null;
			try
			{
				zObj = new ZObjects<A, T>();
				zObj.m_jToken = jArray as JToken;
			}
			catch (Exception ex)
			{
				string msg = ex.ToString();
			}
			return zObj;
		}

		public virtual void Add(T zObject)
		{
			base.jArray.Add((JToken)zObject);
			//((JToken)zObject).Parent = base.jArray;
		}
		public T this[int index]
		{
			get
			{
				T jObj = (T)((JArray)m_jToken)[index];
				return jObj;
			}
		}
		//		public static string itemsTag { get { string value = typeof(A).Name; return value.Substring(1); } }
		//		public new static string itemTag { get { string value = typeof(T).Name; return value.Substring(1); } }
		public new static string itemTag = typeof(A).Name.ToString().Substring(1);

		public new T this[string tag]
		{
			get
			{
				JArray items = m_jToken as JArray;
				JToken jTok = items.SelectToken(itemTag + "[" + tag + "]");
				if (jTok == null)
					jTok = items.Parent.SelectToken(itemTag + "[" + tag + "]");
				if (jTok == null)
				{
					bool has = items.Contains(tag);
					jTok = items.SelectToken("[" + tag + "]");
				}
				if (jTok == null)
				{
					bool has = items.Contains(itemTag);
					JToken pTok = items.Parent.Parent.Values(itemTag).First<JToken>();
					if (pTok.Type == JTokenType.Array)
						jTok = pTok.SelectToken("[" + tag + "]");
//					if ( jTok == null )
//						jTok = items.Parent.Parent.SelectToken(itemTag + "[" + tag + "]");
				}
				if (jTok == null)
					jTok = items[tag];
				JToken val2 = jTok;
				//				T val = (T)((JObject)val2);
				T val = (T)(val2);
				if (val == null)
				{
					val = new T();
					(m_jToken as JObject).Add(tag, (JObject)val);
				}
				return val;
			}
		}
	}

	public class ZEnumerator<T> : IEnumerator where T : ZObject<T>, new()
	{
		protected int m_index;
		protected ZObject<T> m_zObject;
		public ZEnumerator()
		{
			Initialize(null);
		}
		public void Initialize(ZObject<T> zObject)
		{
			m_zObject = zObject;
			Reset();
		}
		public ZEnumerator(ZObject<T> zObject)
		{
			Initialize(zObject);
		}
		public void Reset()
		{
			m_index = -1;
		}
		public bool MoveNext()
		{
			return (m_zObject != null &&  m_zObject.jArray != null && (++m_index < m_zObject.jArray.Count()));
		}
		public virtual object Current
		{
			get
			{
				JToken jTok = null;
				JToken jFirst = null;
				JArray jArray = null;
				T current = null;
				if (m_zObject == null)
					return current;
				try
				{
					jArray = m_zObject.jArray;
					if (jArray != null)
					{
						jTok = jArray[m_index];
						jFirst = jTok.First;
						if (jFirst != null && jFirst.Type == JTokenType.Property)
						{
							jFirst = (jFirst as JProperty).Value;
							if (jFirst.Type == JTokenType.Object)
								jTok = jFirst;
						}
					}
					current = (T)jTok;
				}
				catch (Exception ex)
				{
					Debug.Print(ex.Message);
				}
				return current;
			}
		}
	}
	#endregion

	#region ZObject
	/// <summary>
	/// Base class for all things "Z".
	/// </summary>
	public class ZToken
	{
		public class Tags
		{
			public const string Name		= "Name";
			public const string ZUid		= "ZUid";
		}
		public const string NULL_VALUE = "<NULL>";
		//public virtual string itemTag { get { return ""; } }
		//		public virtual string itemsTag { get { return itemTag + "s"; } }
		protected JToken m_jToken;
		public static implicit operator JToken(ZToken zToken)
		{
			return zToken.m_jToken;
		}
		public static implicit operator ZToken(JObject job)
		{
			ZToken zObj = null;
			try
			{
				zObj = new ZToken();
				zObj.m_jToken = job as JToken;
			}
			catch (Exception ex)
			{
				string msg = ex.ToString();
			}
			return zObj;
		}
		public static explicit operator JObject(ZToken zToken)
		{
			JObject jObject = null;
			try
			{
				jObject = zToken.m_jToken as JObject;
			}
			catch (Exception ex)
			{
				string msg = ex.ToString();
			}
			return jObject;
		}
		public JToken this[string propertyName]
		{
			get
			{
				JToken jVal = null;
				try
				{
					JObject jObj = m_jToken as JObject;
					if (jObj != null)
					{
						jVal = jObj.Value<JToken>(propertyName);
					}
				}
				catch (Exception ex)
				{
					Debug.Print(ex.Message);
				}
				return jVal;
			}
		}

		public ZToken()
		{
			// for now, default to a new object, may have to reconsider for arrays?
			Initialize(new JObject());
		}
		public ZToken(ZToken other)
		{
			// TODO:	figure out if I need to do more here with regard to guids for copy constructors
			//			for now, do nothing
			m_jToken = null;
			if (other != null)
			{
				m_jToken = other.m_jToken;
			}
		}
		public ZToken(JToken jToken)
		{
			Initialize(jToken);
		}
		public virtual void Initialize()
		{
			// This is where to add the GUID
			if (m_jToken != null)
			{
				// don't assign guid to arrays, only to objects
				if (m_jToken.Type == JTokenType.Object)
					zUid = Guid.NewGuid();
			}
		}
		public virtual void Initialize(JToken jToken)
		{
			m_jToken = jToken;
			Initialize();
		}
		public Guid zUid
		{
			get
			{
				Guid val = Guid.Empty;
				try
				{
					JValue jVal = this[Tags.ZUid] as JValue;
					if (jVal != null)
					{
						// don't try to assign a new one, let it remain empty
						Guid.TryParse(jVal.Value<string>(), out val);
					}
				}
				catch (Exception ex)
				{
					Debug.Print(ex.Message);
				}
				return val;
			}
			set
			{
				string guid = value.ToString();
				// now save it to the json
				JValue jVal = this[Tags.ZUid] as JValue;
				// if we don't have a text attribute, add it
				if (jVal == null)
				{
					jVal = new JValue(value);
					(m_jToken as JObject).Add(Tags.ZUid, jVal);
				}
				else
					jVal.Value = value;
			}
		}
		public JToken Property(int ix)
		{
			JToken tok = null;
			JObject jObj = m_jToken as JObject;
			if (jObj != null)
			{
				tok = Properties[ix];
			}

			return tok;
		}
		public int index
		{
			get
			{
				int	value = -1;		// not an element of an array
				if (m_jToken.Parent is JArray)
				{
					value = m_jToken.BeforeSelf().Count<JToken>();
				}
				else if (m_jToken.Parent.Parent is JArray)
				{
					value = m_jToken.Parent.BeforeSelf().Count<JToken>();
				}
				else if (m_jToken.Parent.Parent.Parent is JArray)
				{
					value = m_jToken.Parent.Parent.BeforeSelf().Count<JToken>();
				}
				return value;
			}
		}
		public List<JToken> Properties
		{
			get
			{
				List<JToken> properties = null;
				JObject jObj = m_jToken as JObject;
				if (jObj != null)
				{
					properties = jObj.Children().ToList<JToken>(); ;
				}
				return properties;
			}
		}
		// TODO: may need to return variant at some time
		public string GetStringValue(string propName, bool stripTags = true)
		{
			string val = "";
			JValue jVal = this[propName] as JValue;
			JToken jTok = this[propName] as JToken;
			if (jVal != null)
			{
				val = jVal.Value<string>();
				// this is put in here to strip out any task loc information
				// I may change my mind on this in the event that it affects the grading feedback
				if (stripTags)
				{
//					val = ZTaskLocs.StrippedText(val);
					val = ResolveStringValue(val, true);
				}
			}
			return val;
		}
		public string[] GetStringArrayValue(string propName)
		{
			string[] val = null;
			JToken jVal =  null; //this[propName];
			jVal = (JToken)GetObjectValue(propName);
			if (jVal != null )
			{
				if (jVal.Type == JTokenType.Array)
				{
					JArray jArray = jVal as JArray;
					val = new string[jArray.Count];
					for (int x = 0; x < jArray.Count; ++x)
						val[x] = jArray[x].ToString();
				}
				// force return of array, even if it isn't
				else if (jVal.Type == JTokenType.String)
				{
					val = new string[1];
					val[0] = jVal.ToString();
				}
			}
			else
				val = new string[0];
			return val;
		}
		public object GetObjectValue(string propName)
		{
			object val = null;
			JToken jTok =  this.SelectToken(propName);
			if (jTok != null)
			{
				if (jTok.Type == JTokenType.Object || jTok.Type == JTokenType.Array)
					val = jTok;
				//jVal.Value<object>();
			}
			return val;
		}
		public JToken SelectToken(string tokenPath)
		{
			JToken value = null;
			// see if we're grabbing at the root level or relative to current position
			if (tokenPath.StartsWith("/"))
			{
				JToken jTok = m_jToken.Root;
				value = jTok.SelectToken(tokenPath.Substring(1));
			}
			else
				value = m_jToken.SelectToken(tokenPath);

			return value;
		}
		public class RXFields
		{
			public const string Parm			= "parm";
			public const string Index			= "index";
			public const string VarName			= "varName";
			public const string ElementName		= "elementName";
			public const string ElementName1	= "elementName1";
			public const string ElementName2	= "elementName2";
			public const string ParentVarName	= "parentVarName";
			public const string ContentVarName	= "contentVarName";
			public const string PathRef			= "pathRef";
			public const string FormatArg		= "formatArg";
		}
		static string exprVarPattern		= @"^[^{}]*(((?'Open'{)[^{}]*)+((?'Close-Open'})[^{}]*)+)*(?(Open)(?!))$";
		static string parmPattern			= @"(\((?<"  + RXFields.Parm			+ @">\d+)\)?)";
		static string indexPattern			= @"(\[(?<"  + RXFields.Index			+ @">[\w\d_]+)\]?)";
		static string varNamePattern		= @"(^(?<"   + RXFields.VarName			+ @">[^\^\/\*\(\[]+))";
		static string elementNamePattern	= @"(\.(?<"  + RXFields.ElementName		+ @">[\w\d_]+))";
		static string elementName1Pattern	= @"(\.(?<"  + RXFields.ElementName1	+ @">[\w\d_]+))";
		static string elementName2Pattern	= @"(\.(?<"  + RXFields.ElementName2	+ @">[\w\d_]+))";
		static string parentVarNamePattern  = @"(^\^(?<" + RXFields.ParentVarName	+ @">[^\^\*\(\[\.]+))";
		static string contentVarNamePattern = @"(^\*(?<" + RXFields.ContentVarName	+ @">[^\^\*\(\[]+))";
		static string pathRefPattern		= @"(^\/(?<" + RXFields.PathRef			+ @">[^{\]]+))";
		static string formatArgPattern		= @"(\{(?<"  + RXFields.FormatArg		+ @">[\d]+)\}$)";

		public string ResolveStringValue(string stringValue, bool stripTags = false)
		{
			string value		= "";
			if (stringValue == null)
				return NULL_VALUE;
			if (stripTags)
				stringValue = ZTaskLocs.StrippedText(stringValue);
			if (stringValue.Contains("{^Args"))
				value = "";
			JValue jValue		= null;
			JToken jToken		= null;
			// test resolving nested 
			Match m = Regex.Match(stringValue, pathRefPattern);
			string pathRef = "";
			// are we matching a format arg for string.Format?
			if (stringValue == "{0}" || stringValue == "{1}" || (stringValue.StartsWith("{_") && stringValue.EndsWith("_}")))
				return stringValue;
			m = Regex.Match(stringValue, formatArgPattern);
			if (m.Success)
				return stringValue;

			m = Regex.Match(stringValue, exprVarPattern);
			// group 5 contains the embedded variables
			if (m.Success && (m.Groups[5].Captures.Count > 0))
			{
				Group grp = m.Groups[5];
				int capCtr = 0;
				int stringValuePos = 0;		// current position in the string value
				int prevStringValuePos = stringValuePos;
				foreach (Capture cap in grp.Captures)
				{
					// if the current capture pos is after the current string value position, copy the text into the value
					if ((cap.Index-1) > stringValuePos)
					{
						value += stringValue.Substring(stringValuePos, cap.Index - stringValuePos - 1);
					}
					prevStringValuePos = stringValuePos;
					stringValuePos = cap.Index + cap.Length + 1;	// include the bounding braces
					// now process our captured variable
					Match vm = Regex.Match(cap.Value, "(" 
						+		varNamePattern
						+ "|" + pathRefPattern
						+ "|" + contentVarNamePattern
						+ "|" + parentVarNamePattern
						+ ")((" 
							+ "("  + indexPattern + elementNamePattern + parmPattern + ")"
							+ "|(" + indexPattern + elementNamePattern + ")"
							+ "|(" + elementNamePattern + parmPattern + ")"
							+ "|(" + elementNamePattern + elementName1Pattern + elementName2Pattern + ")"
							+ "|(" + elementNamePattern + elementName1Pattern + ")"
							+ "|(" + elementNamePattern + ")"
							+ "|(" + parmPattern + indexPattern  + ")"
							+ "|(" + indexPattern + parmPattern + ")"
							+ "|(" + indexPattern  + ")"
							+ "|(" + parmPattern +  ")"
						+ ")*)");
					if (vm.Success)
					{
						string varValue = "";
						string vName		= vm.Groups[RXFields.VarName].Value;
						string contentName	= vm.Groups[RXFields.ContentVarName].Value;
						string parentName	= vm.Groups[RXFields.ParentVarName].Value;
						string parmVal		= vm.Groups[RXFields.Parm].Value;
						string iVal			= vm.Groups[RXFields.Index].Value;
						string element		= vm.Groups[RXFields.ElementName].Value;
						string element1		= vm.Groups[RXFields.ElementName1].Value;
						string element2		= vm.Groups[RXFields.ElementName2].Value;
						//if (stringValuePos < stringValue.Length)
						//	value += stringValue.Substring(stringValuePos);
						// see if we have a path reference, if so, we need to resolve it first
						if (vm.Groups[RXFields.PathRef].Success)
						{
							// whatever is in value at the moment has our path
							pathRef = value.Substring(1);
							//							pathRef = vm.Groups[RXFields.PathRef].Value + "]"; // value.Substring(2);
							jToken = SelectToken(pathRef); // this.m_jToken.Root.SelectToken(pathRef);
							if (jToken == null)
								value = "*NFND*";
							else if (jToken.Type == JTokenType.Property)
							{
								//pathRef = value.Remove(0,pathRef.Length+3);
								//jToken = jToken.Parent;
								//jToken = jToken.SelectToken(pathRef);
								value = (jToken as JProperty).Value.ToString();
							}
							else
								value = (jToken as JValue).ToString();
							if (value.Contains("Context") || value.Contains("Employees"))
							{
								if ( stripTags )
									value = ZTaskLocs.StrippedText(value);
							}
								
							return value;
#if false
							int indexOfRest = prevStringValuePos + vm.Groups[RXFields.PathRef].Index + vm.Groups[RXFields.PathRef].Length + 1;
							string restOfIt = "";
							string	pathSuffix = "";
							// we have a path reference, we need to grab ....
							if (indexOfRest < stringValue.Length)
							{
								restOfIt = stringValue.Substring(indexOfRest);
								if (restOfIt.StartsWith(".") || restOfIt.StartsWith("["))
								{
									pathRef += restOfIt.Substring(0, 1);
									restOfIt = restOfIt.Substring( 1);
								}
								restOfIt = restOfIt.Substring(0,restOfIt.Length-1);	// TODO: fix issue if path is not the only thing here, assuming there is a trailing }
								pathSuffix = ResolveStringValue(restOfIt,stripTags);
								// append the pathRef root
								pathRef = value.Substring(2);
								jToken = this.m_jToken.Root.SelectToken(pathRef);
								if (jToken == null)
									value = "*NFND*";
								else if (jToken.Type == JTokenType.Property)
								{
									value = (jToken as JProperty).Value.ToString();
								}
								else
									value = (jToken as JValue).ToString();
								return value;
							}
#endif
						}

						else if (vName.Length > 0)
						{
							int		intValue = 0;
							if (int.TryParse(vName, out intValue) || (vName.StartsWith("_") && vName.EndsWith("_")))
							{
								varValue = "{" + vName + "}";
								value += varValue;
								capCtr++;
								continue;
							}
							else if (vName.StartsWith("~"))
							{
								jToken = GetParentProperty(vName);
							}
							else if (vName == "ParentName" || vName == "ParentID")
							{
								JToken parentToken = m_jToken.Parent;
								while (parentToken != null && parentToken.Type != JTokenType.Property)
									parentToken = parentToken.Parent;

								jToken = parentToken;
							}
							else
								jToken = GetSiblingProperty(vName);
							//varValue = GetSiblingValue(vName).Value<string>();
						}
						else if (parentName.Length > 0)
						{
							// we may be looking for an element in this parent
							if (iVal.Length > 0)
							{
								parentName += "[" + iVal + "]";
								iVal = "";
							}
							if (parentName == "Step" && this is ZTask)
							{
								ZStep step = (this as ZTask).parentStep;
								ZStepLoc stepLoc = step.stepLocs[element];
								value = step.text.Substring(stepLoc.startPos, stepLoc.length);
								return value;
							}
							if (element.Length > 0)
							{
								parentName += "." + element;
								if (element1.Length > 0)
									parentName += "." + element1;
								if (element2.Length > 0)
									parentName += "." + element2;
								element = "";
								element1 = "";
								element2 = "";
								jToken =  GetParentProperty("~" + parentName);

							}
							else if (parentName == "ParentName" || parentName == "ParentID")
							{
								JToken parentToken = m_jToken.Parent;
								// need to go one deeper
								if (parentToken.Parent != null)
									parentToken = parentToken.Parent;

								while (parentToken != null && parentToken.Type != JTokenType.Property)
									parentToken = parentToken.Parent;

								jToken = parentToken;
							}
							else
							{
								jToken =  GetParentProperty(parentName);
								if (jToken == null)
									jToken = m_jToken[parentName]; // GetSiblingValue(parentName);	// try the sibling, for now
								//varValue = GetParentStringValue(parentName);
							}
						}
						// see if jValue is an object or an array, if not, just grab the value as a string, trap for null
						if (jToken != null && jToken.Type == JTokenType.Array)
						{
							// make sure we have a value for the index, could be number or name
							JArray jArray = (JArray)jToken;
							JToken arrayValue = null;
							if (iVal.Length > 0)
							{
								int arrayIndex = 0;
								if (int.TryParse(iVal, out arrayIndex))
								{
									if (arrayIndex < jArray.Count)
										arrayValue = jArray[arrayIndex];
								}
								else
								{
									// if using a named element, we're looking for the property
									foreach (JToken tok in jArray)
									{
										JObject obj = tok as JObject;
										JProperty prop = obj.First as JProperty;

										if (prop != null && prop.Name == iVal)
										{
											if (prop.Value.Type == JTokenType.Object)
												arrayValue = prop.Value as JObject;
											else
												arrayValue = tok;
											break;
										}
									}
								}
								// see if we have something now, we should have an object, or a value
								if ((arrayValue != null) && 
										(arrayValue.Type == JTokenType.Object || arrayValue.Type == JTokenType.Property))
									jToken = (JToken)arrayValue;
								else
									jToken = null;	// TODO: may want to do something here, but not right now
							}
							else
							{
								jToken = null;	// TODO: may want to do something here, but not right now
							}
						}
						// assumption is that the array is first
						if (jToken == null)
						{
							varValue = null;
						}
						else if (jToken.Type == JTokenType.Object)
						{
							JObject jObject = (JObject)jToken;
							JToken jTok = null;
							varValue = null;
							// if we're looking at a particular element in this object?
							if (element != null && element.Length > 0)
							{
								if (jObject.TryGetValue(element, out jTok))
								{
									if (jTok.Type == JTokenType.Property)
									{
										jValue = (JValue)jTok;
									}
									else
									{
										jValue = (JValue)jTok;
									}
								}
							}
							else
								jValue = jToken as JValue;
							// need to resolve this here in case we need to go up the chain from here
							if (jValue == null ||  jValue.Value.Equals(null))
								varValue = null;
							else
								varValue = jValue.Value<string>();
						}
						else if (jToken.Type == JTokenType.Property)
						{
							if (parentName == "ParentID" || parentName == "ParentName" || vName == "ParentID" || vName == "ParentName")
							{
								varValue = ZToken.OName(jToken);
							}
							else
							{
								JProperty prop = jToken as JProperty;
								JValue jVal = prop.Value as JValue;
								if (jVal != null)
									varValue = jVal.Value<string>();
								else
									varValue = prop.Value<string>();
							}
						}
						else
						{
							jValue = jToken as JValue;
							if (jValue == null ||  jValue.Value.Equals(null))
								varValue = null;
							else
								varValue = jValue.Value<string>();
						}
						// first thing to do is to see if we have a cascading reference
						if (varValue != null && varValue.Length > 0)
						{
							if (jToken.Type != JTokenType.Object && jToken.Type != JTokenType.Array)
								jToken = jToken.Parent;
							// resolve value relative to this token
							if (jToken.Type == JTokenType.Object || jToken.Type == JTokenType.Array)
							{
								if (varValue == "{ParentName}" || varValue == "{ParentID}")
								{
									while (jToken.Type != JTokenType.Object && jToken.Type != JTokenType.Array)
										jToken = jToken.Parent;

									varValue = ((ZToken)jToken).GetParentStringValue("ParentName");
								}
								else if (varValue == "{^ParentName}" || varValue == "{^ParentID}")
								{
									jToken = jToken.Parent;
									while (jToken.Type != JTokenType.Object && jToken.Type != JTokenType.Array)
										jToken = jToken.Parent;

									varValue = ((ZToken)jToken).GetParentStringValue("ParentName");
								}
								else
									varValue = ((ZToken)jToken).ResolveStringValue(varValue, stripTags);
							}
							else
							{
								if (varValue == "{ParentName}" || varValue == "{ParentID}")
								{
									jToken = jToken.Parent;
									while (jToken.Type != JTokenType.Object && jToken.Type != JTokenType.Array)
										jToken = jToken.Parent;
									varValue = ((ZToken)jToken).GetParentStringValue("ParentName");
								}
								else if (varValue == "{^ParentName}" || varValue == "{^ParentID}")
								{
									jToken = jToken.Parent;
									while (jToken.Type != JTokenType.Object && jToken.Type != JTokenType.Array)
										jToken = jToken.Parent;

									varValue = ((ZToken)jToken).GetParentStringValue("ParentName");
								}
								else if (varValue.Contains("ParentID") || varValue.Contains("ParentName"))
								{
									if (jToken == null)
										jToken = m_jToken;
									else
										jToken = jToken.Parent;
									while (jToken.Type != JTokenType.Object && jToken.Type != JTokenType.Array)
										jToken = jToken.Parent;
									varValue = ((ZToken)jToken).ResolveStringValue(varValue, stripTags);
								}
								else if (varValue == "{^Text}" )
								{
									jToken = jToken.Parent;
									while (jToken.Type != JTokenType.Object && jToken.Type != JTokenType.Array)
										jToken = jToken.Parent;

									varValue = ((ZToken)jToken).GetParentStringValue("Text");
								}
								else
									varValue = ResolveStringValue(varValue, stripTags);
							}
						}
						// now we'll check to see if we are looking at an expression and extract parm values
						if (parmVal.Length > 0 && varValue != null && varValue.Length > 0)
						{
							int parmIndex = 0;
							int.TryParse(parmVal, out parmIndex);
							ZExprNode zNode = ZExpr.Parse(varValue.Substring(1));
							if (parmIndex == 0)
								varValue = zNode.text;
							else if (parmIndex <= zNode.children.Count)
								varValue = zNode.children[parmIndex-1].expression;
							else
								varValue = NULL_VALUE;
						}
						value += varValue;
					}
					capCtr++;
				}
				// if there is anything else in the string, append it to the value
				if (stringValuePos < stringValue.Length)
					value += stringValue.Substring(stringValuePos);
			}
			else
			{
				value = stringValue;
			}
			if (value.Contains("Context") || value.Contains("Employees"))
			{
				if (stripTags)
					value = ZTaskLocs.StrippedText(value);
			}

			return value;
		}
		public JValue GetSiblingValue(string propName)
		{
			JValue jVal = null;
			// start traversing back up the tree
			JToken jTok = GetSiblingProperty(propName);
			if (jTok != null)
			{
				jVal = jTok as JValue;
			}

			return jVal;
		}
		public JToken GetSiblingProperty(string propName)
		{
			JToken prop = null;
			// start traversing back up the tree
			JToken jTok = m_jToken;
			if (jTok != null && jTok.Type == JTokenType.Object)
			{
				JObject jObj = jTok as JObject;
				if (jObj.TryGetValue(propName, out prop))
					prop = null;
			}

			return prop;
		}
		public JToken GetParentProperty(string propName)
		{
			JToken jProp = null;
			bool	isParentProp = false;
			bool usePath = propName.StartsWith("~") || propName.Contains(".");
			if (propName.StartsWith("~"))
			{
				isParentProp = true;
				propName = propName.Substring(1);
			}
			// start traversing back up the tree
			JToken jTok = m_jToken.Parent;
			if ( isParentProp && jTok.Parent != null )
				jTok = jTok.Parent;
			JToken pTok = null;
			while (jTok != null && jProp == null)
			{
				if (usePath)
				{
					pTok = jTok.SelectToken(propName);
					if (pTok == null)
					{
						jTok = jTok.Parent;
						continue;
					}
					else
					{
						jTok = pTok.Parent; // .First();
						if (jTok.Parent.Type == JTokenType.Property)
							jTok = jTok.Parent;
					}
				}
				// if this is an array, go to the parent
				if (jTok.Type == JTokenType.Array)
				{
					jTok = jTok.Parent;
				}
				// if this is an object, go to the parent
				else if (jTok.Type == JTokenType.Object)
				{
					JObject jObj = jTok as JObject;
					JToken tmpTok = null;
					if (jObj.TryGetValue(propName, out tmpTok))
					{
						jProp = tmpTok;
					}
					else
						jTok = jTok.Parent;
				}
				else if (jTok.Type == JTokenType.Property)
				{
					JProperty tmpProp = jTok as JProperty;
					if ((!usePath) &&  (tmpProp.Name != propName))
					{
						jProp = null;
						jTok = jTok.Parent;
					}
					else
						jProp = jTok;
				}
				else if (jTok.Parent.Type == JTokenType.Property)
				{
					jProp = jTok as JProperty;
				}
				else
				{
					Debug.Print("oops, don't know how we got a: " +  jTok.Type.ToString());
				}
			}
			return jProp;
		}
		public JToken GetParentValue(string propName)
		{
			JToken jVal = null;
			JToken jProp = GetParentProperty(propName);
			if (jProp != null)
			{
				bool hasValue = jProp.HasValues;
				if (jProp.Type == JTokenType.Property)
					jVal = jProp as JValue;
			}
			return jVal;
		}
		public string GetParentStringValue(string propName)
		{
			string val = "";
			JToken jTok = m_jToken.Parent;
			// See if we want just the name of the parent object
			if (propName == "ParentName" && jTok != null && (jTok.Type == JTokenType.Property || jTok.Type == JTokenType.Object))
			{
				JProperty jProp = jTok as JProperty;
				val = jProp.Name;
				return val;
			}

			JValue jVal = GetParentProperty(propName) as JValue;
			if (jVal != null)
			{
				val = jVal.Value<string>();
			}
			return val;
		}
		public void SetValue(string propName, string value)
		{
			JValue jVal = this[propName] as JValue;
			// if we don't have a text attribute, add it
			if (jVal == null)
			{
				if (m_jToken == null)
					m_jToken = new JObject();
				(m_jToken as JObject).Add(propName, value);
			}
			else
				jVal.Value = value;
		}
		public static string propName(MethodBase myBase)
		{
			return propName(myBase, true);
		}
		public static string propName(MethodBase myBase, bool changeCase)
		{
			string propName = "";
			if (changeCase)
				propName = (myBase as MethodInfo).Name.Substring(4, 1).ToUpper() + (myBase as MethodInfo).Name.Substring(5);
			else
				propName = (myBase as MethodInfo).Name.Substring(4);
			return propName;
		}
		public void set(MethodBase myBase, string value)
		{
			SetValue(propName(myBase), value);
		}
		public void set(MethodBase myBase, object value)
		{
			SetValue(propName(myBase), value);
		}
		public void set(MethodBase myBase, JObject value)
		{
			SetValue(propName(myBase), value);
		}
		public void set(MethodBase myBase, int value)
		{
			SetValue(propName(myBase), value);
		}
		public void set(MethodBase myBase, bool value)
		{
			SetValue(propName(myBase), value);
		}
		public void set(MethodBase myBase, double value)
		{
			SetValue(propName(myBase), value);
		}
		public void SetValue(string propName, object value)
		{
			JValue jVal = this[propName] as JValue;
			// if we don't have a text attribute, add it
			if (jVal == null)
				(m_jToken as JObject).Add(propName, (JToken)value);
			else
				jVal.Value = value;
		}
		public void SetValue(string propName, JObject value)
		{
			JToken jVal = this.SelectToken(propName);
			//(m_jToken as JObject).Remove(propName);
			//(m_jToken as JObject).Remove(propName);
			// if we don't have a text attribute, add it
			if (jVal == null)
				(m_jToken as JObject).Add(propName, (JToken)value);
			else
				(m_jToken as JObject)[propName].Replace((JToken)value);
		}
		protected int GetIntValue(string propName)
		{
			int val = 0;
			JValue jVal = this[propName] as JValue;
			if (jVal != null)
			{
				val = jVal.Value<int>();
			}
			return val;
		}
		protected void SetValue(string propName, int value)
		{
			JValue jVal = this[propName] as JValue;
			// if we don't have a text attribute, add it
			if (jVal == null)
				(m_jToken as JObject).Add(propName, value);
			else
				jVal.Value = value;
		}
		protected bool GetBoolValue(string propName)
		{
			bool val = false;
			JValue jVal = this[propName] as JValue;
			if (jVal != null)
			{
				val = jVal.Value<bool>();
			}
			return val;
		}
		protected void SetValue(string propName, bool value)
		{
			JValue jVal = this[propName] as JValue;
			// if we don't have a text attribute, add it
			if (jVal == null)
				(m_jToken as JObject).Add(propName, value);
			else
				jVal.Value = value;
		}
		protected JValue GetJValue(string propName)
		{
			JValue jVal = null;
			try
			{
				JObject jObj = null;
				if (m_jToken.Type == JTokenType.Object)
				{
					jVal = this[propName] as JValue;
				}
				else if (m_jToken.Type == JTokenType.Array)
				{
					jObj = (m_jToken as JArray)[propName] as JObject;
					jVal = jObj.Values().First() as JValue;
				}
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message);
			}
			return jVal;
		}
		protected double GetDoubleValue(string propName)
		{
			double val = 0.0;
			try
			{
				JValue jVal = GetJValue(propName);
				if (jVal != null)
				{
					val = jVal.Value<double>();
				}
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message);
			}
			return val;
		}
		protected void SetValue(string propName, double value)
		{
			JValue jVal = this[propName] as JValue;

			// if we don't have a text attribute, add it
			if (jVal == null)
				(m_jToken as JObject).Add(propName, value);
			else
				jVal.Value = value;
		}
		public string PropName(MethodBase myBase)
		{
			MethodInfo myInfo = myBase as MethodInfo;
			string propName = ZToken.propName(myInfo);
			return propName;
		}
		public object get(MethodBase myBase, bool checkChildren = false)
		{
			MethodInfo myInfo = myBase as MethodInfo;
			string propName = ZToken.propName(myInfo);

			if (myInfo.ReturnType == typeof(string))
			{
				string value = GetStringValue(propName);
				if (checkChildren)
				{
					if (value == null || value == "")
					{
						JToken jTok =this.Properties.First<JToken>().Children().First<JToken>();
						value = (string)((ZToken)(jTok)).get(myBase);
						if (value == null)
							value = NULL_VALUE;
					}

				}
				return value;
			}
			else if (myInfo.ReturnType == typeof(int))
			{
				return GetIntValue(propName);
			}
			else if (myInfo.ReturnType == typeof(double))
			{
				return GetDoubleValue(propName);
			}
			else if (myInfo.ReturnType == typeof(bool))
			{
				return GetBoolValue(propName);
			}
			else if (myInfo.ReturnType == typeof(string[]))
			{
				return GetStringArrayValue(propName);
			}
			else
				return GetObjectValue(propName);
		}
		public JTokenType Type
		{
			get
			{
				if (m_jToken == null)
					return JTokenType.Null;
				return m_jToken.Type;
			}
		}
		public JArray jArray
		{
			get
			{
				if (m_jToken == null)
					m_jToken = new JArray();
				JArray jArray = m_jToken as JArray;
				return jArray;
			}
			set { m_jToken = (JToken)value; }
		}
		public ZProperties zProperties
		{
			get
			{
				ZProperties value = new ZProperties();
				value.m_jToken = m_jToken;
                return value;
			}
		}
		public JArray ZChildren(string name)
		{
			JArray jArray = null;
			try
			{
				// make sure this is an object for this call
				JObject jObj = m_jToken as JObject;
				if (jObj != null)
					jArray = jObj[name] as JArray;
				else
					jArray = new JArray();
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message);
			}

			return jArray;
		}
		public static string OName(JToken tok)
		{
			string oName = "";
			JObject obj = tok as JObject;
			JProperty prop = tok as JProperty;
			JArray jArray = tok as JArray;
			// if we have an object, grab the Name property, otherwise treat this as a property
			// and grab its name
			if (jArray != null)
			{
				if ((prop = jArray.Parent as JProperty) != null)
					oName = prop.Name;
			}
			if (oName.Length == 0 && obj != null)
			{
                JToken jTok = tok[Tags.Name];
                if (jTok != null)
                    oName = jTok.Value<string>();
                else
                {
                    if ((prop = tok.Parent as JProperty) != null)
                        oName = prop.Name;
                    else
                        prop = obj.First as JProperty;
                }
			}
			if (oName.Length == 0 && prop != null)
			{
				oName = prop.Name;
			}
			if (oName.Length == 0 && tok != null)
			{
				if ((prop = tok.Parent as JProperty) != null)
					oName = prop.Name;
			}

			return oName;
		}
		public static string OID(JToken tok)
		{
			string oID = "";
			JObject obj = tok as JObject;
			JProperty prop = tok as JProperty;
			JArray jArray = tok as JArray;
			// if we have an object, grab the Name property, otherwise treat this as a property
			// and grab its name
			if (jArray != null)
			{
				if ((prop = jArray.Parent as JProperty) != null)
					oID = prop.Name;
			}
			if (oID.Length == 0 && tok != null)
			{
				if ((prop = tok.Parent as JProperty) != null)
					oID = prop.Name;
			}
			if (oID.Length == 0 && prop != null)
			{
				oID = prop.Name;
			}

			return oID;
		}
		public string id
		{
			get
			{
				return OID(m_jToken);
			}
		}
		public string name
		{
			get
			{
				string value = "";
                JTokenType jType = m_jToken.Type;
//                value = (m_jToken as JObject).Property(Tags.Name);
				value = GetStringValue(Tags.Name);
				if (value.Length == 0)
					value = OName(m_jToken);
				return value;
			}
			set
			{
				JValue jVal = this[Tags.Name] as JValue;
				// if we don't have a text attribute, add it
				if (jVal == null)
				{
					if (m_jToken == null)
						m_jToken = new JObject();
					(m_jToken as JObject).Add(Tags.Name, value);
				}
				else
					jVal.Value = value;
			}
		}
		// TODO: resolve Value with embedded braces { }s
		public string value
		{
			get
			{
				string val = "";
				JProperty prop = m_jToken as JProperty;
				if (prop != null)
				{
					// if we have an object, grab its string rep
					if (prop.Value.Type == JTokenType.Object || prop.Value.Type == JTokenType.Array)
						val = prop.Value.ToString();
					else
					{
						JValue jVal = prop.Value as JValue;
						val = jVal.Value.ToString();
					}
				}
				else if (m_jToken is JObject)
				{
					val = GetStringValue("Value");
				}
				return val;
			}
			set
			{
				JProperty prop = m_jToken as JProperty;
				if (prop != null)
				{
					// if we have an object, grab its string rep
					if (prop.Value.Type == JTokenType.String)
						prop.Value = value;
					else if (prop.Value.Type == JTokenType.Integer)
						prop.Value = int.Parse(value);
				}
			}

		}
		public string jPath
		{
			get { return ZRubric.GetJPath(m_jToken); }
		}
		public string rPath
		{
			get { return ZRubric.GetRPath(m_jToken); }
		}
		public override string ToString()
		{
			return m_jToken.ToString();
		}
	}

	public class ZObject<T> : ZToken where T : ZObject<T>, new()
	{
		public ZObject() { }
		public ZObject(ZObject<T> other) : base(other) { }
		public ZObject(JToken jToken) : base(jToken) { }
		public ZObject(JObject jObject) : base(jObject) { }
		public static string itemTag = typeof(T).Name.ToString().Substring(1);
		//		public new static const string itemTag { get { string value = typeof(T).Name; return value.Substring(1); } }
		public static implicit operator JToken(ZObject<T> zObject)
		{
			return zObject.m_jToken;
		}
		public static implicit operator ZObject<T>(JObject job)
		{
			T zObj = null;
			try
			{
				zObj = new T();
				zObj.m_jToken = job as JToken;
			}
			catch (Exception ex)
			{
				string msg = ex.ToString();
			}
			return zObj;
		}

		//public static implicit operator ZObject<T>(JToken job)
		//{
		//	T zObj = null;
		//	try
		//	{
		//		zObj = new T();
		//		zObj.m_jToken = job;
		//	}
		//	catch (Exception ex)
		//	{
		//		string msg = ex.ToString();
		//	}
		//	return zObj;
		//}
		public static implicit operator JObject(ZObject<T> zObj)
		{
			JObject jObject = null;
			try
			{
				jObject = zObj.m_jToken as JObject;
			}
			catch (Exception ex)
			{
				string msg = ex.ToString();
			}
			return jObject;
		}
	}
	#endregion

}
