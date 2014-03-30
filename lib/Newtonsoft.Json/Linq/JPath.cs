#if false	// my modified version

#else	// old version
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Newtonsoft.Json.Linq
{


	public delegate object JsonPathScriptEvaluator(string script, object value, string context);
	public delegate void JsonPathResultAccumulator(object value, string[] indicies);

	public interface IJsonPathValueSystem
	{
		bool HasMember(object value, string member);
		object GetMemberValue(object value, string member);
		IEnumerable GetMembers(object value);
		bool IsObject(object value);
		bool IsArray(object value);
		bool IsPrimitive(object value);
	}

	[Serializable]
	public sealed class JsonPathNode
	{
		private readonly object value;
		private readonly string path;

		public JsonPathNode(object value, string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			if (path.Length == 0)
				throw new ArgumentException("path");

			this.value = value;
			this.path = path;
		}

		public object Value
		{
			get { return value; }
		}

		public string Path
		{
			get { return path; }
		}

		public override string ToString()
		{
			return Path + " = " + Value;
		}

		public static object[] ValuesFrom(ICollection nodes)
		{
			object[] values = new object[nodes != null ? nodes.Count : 0];

			if (values.Length > 0)
			{
				Debug.Assert(nodes != null);

				int i = 0;
				foreach (JsonPathNode node in nodes)
					values[i++] = node.Value;
			}

			return values;
		}

		public static string[] PathsFrom(ICollection nodes)
		{
			string[] paths = new string[nodes != null ? nodes.Count : 0];

			if (paths.Length > 0)
			{
				Debug.Assert(nodes != null);

				int i = 0;
				foreach (JsonPathNode node in nodes)
					paths[i++] = node.Path;
			}

			return paths;
		}
	}

	public sealed class JsonPathContext
	{
		public static readonly JsonPathContext Default = new JsonPathContext();

		private JsonPathScriptEvaluator eval;
		private IJsonPathValueSystem system;

		public JsonPathScriptEvaluator ScriptEvaluator
		{
			get { return eval; }
			set { eval = value; }
		}

		public IJsonPathValueSystem ValueSystem
		{
			get { return system; }
			set { system = value; }
		}

		public void SelectTo(object obj, string expr, JsonPathResultAccumulator output)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");

			if (output == null)
				throw new ArgumentNullException("output");

			Interpreter i = new Interpreter(output, ValueSystem, ScriptEvaluator);

			expr = Normalize(expr);

			if (expr.Length >= 1 && expr[0] == '$') // ^\$:?
				expr = expr.Substring(expr.Length >= 2 && expr[1] == ';' ? 2 : 1);

			i.Trace(expr, obj, "$");
		}

		public JsonPathNode[] SelectNodes(object obj, string expr)
		{
			ArrayList list = new ArrayList();
			SelectNodesTo(obj, expr, list);
			return (JsonPathNode[])list.ToArray(typeof(JsonPathNode));
		}

		public IList SelectNodesTo(object obj, string expr, IList output)
		{
			ListAccumulator accumulator = new ListAccumulator(output != null ? output : new ArrayList());
			SelectTo(obj, expr, new JsonPathResultAccumulator(accumulator.Put));
			return output;
		}

		private static Regex RegExp(string pattern)
		{
			return new Regex(pattern, RegexOptions.ECMAScript);
		}

		private static string Normalize(string expr)
		{
			NormalizationSwap swap = new NormalizationSwap();
			expr = RegExp(@"[\['](\??\(.*?\))[\]']").Replace(expr, new MatchEvaluator(swap.Capture));
			expr = RegExp(@"'?\.'?|\['?").Replace(expr, ";");
			expr = RegExp(@";;;|;;").Replace(expr, ";..;");
			expr = RegExp(@";$|'?\]|'$").Replace(expr, string.Empty);
			expr = RegExp(@"#([0-9]+)").Replace(expr, new MatchEvaluator(swap.Yield));
			return expr;
		}

		private sealed class NormalizationSwap
		{
			private readonly ArrayList subx = new ArrayList(4);

			public string Capture(Match match)
			{
				Debug.Assert(match != null);

				int index = subx.Add(match.Groups[1].Value);
				return "[#" + index.ToString(CultureInfo.InvariantCulture) + "]";
			}

			public string Yield(Match match)
			{
				Debug.Assert(match != null);

				int index = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
				return (string)subx[index];
			}
		}

		public static string AsBracketNotation(string[] indicies)
		{
			if (indicies == null)
				throw new ArgumentNullException("indicies");

			StringBuilder sb = new StringBuilder();

			foreach (string index in indicies)
			{
				if (sb.Length == 0)
				{
					sb.Append('$');
				}
				else
				{
					sb.Append('[');
					if (RegExp(@"^[0-9*]+$").IsMatch(index))
						sb.Append(index);
					else
						sb.Append('\'').Append(index).Append('\'');
					sb.Append(']');
				}
			}

			return sb.ToString();
		}

		private static int ParseInt(string s)
		{
			return ParseInt(s, 0);
		}

		private static int ParseInt(string str, int defaultValue)
		{
			if (str == null || str.Length == 0)
				return defaultValue;

			try
			{
				return int.Parse(str, NumberStyles.None, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				return defaultValue;
			}
		}

		private sealed class Interpreter
		{
			private readonly JsonPathResultAccumulator output;
			private readonly JsonPathScriptEvaluator eval;
			private readonly IJsonPathValueSystem system;

			private static readonly IJsonPathValueSystem defaultValueSystem = new BasicValueSystem();

			private static readonly char[] colon = new char[] { ':' };
			private static readonly char[] semicolon = new char[] { ';' };

			private delegate void WalkCallback(object member, string loc, string expr, object value, string path);

			public Interpreter(JsonPathResultAccumulator output, IJsonPathValueSystem valueSystem, JsonPathScriptEvaluator eval)
			{
				Debug.Assert(output != null);

				this.output = output;
				this.eval = eval != null ? eval : new JsonPathScriptEvaluator(NullEval);
				this.system = valueSystem != null ? valueSystem : defaultValueSystem;
			}

			public void Trace(string expr, object value, string path)
			{
				if (expr == null || expr.Length == 0)
				{
					Store(path, value);
					return;
				}

				int i = expr.IndexOf(';');
				string atom = i >= 0 ? expr.Substring(0, i) : expr;
				string tail = i >= 0 ? expr.Substring(i + 1) : string.Empty;

				if (value != null && system.HasMember(value, atom))
				{
					Trace(tail, Index(value, atom), path + ";" + atom);
				}
				else if (atom == "*")
				{
					Walk(atom, tail, value, path, new WalkCallback(WalkWild));
				}
				else if (atom == "..")
				{
					Trace(tail, value, path);
					Walk(atom, tail, value, path, new WalkCallback(WalkTree));
				}
				else if (atom.Length > 2 && atom[0] == '(' && atom[atom.Length - 1] == ')') // [(exp)]
				{
					Trace(eval(atom, value, path.Substring(path.LastIndexOf(';') + 1)) + ";" + tail, value, path);
				}
				else if (atom.Length > 3 && atom[0] == '?' && atom[1] == '(' && atom[atom.Length - 1] == ')') // [?(exp)]
				{
					Walk(atom, tail, value, path, new WalkCallback(WalkFiltered));
				}
				else if (RegExp(@"^(-?[0-9]*):(-?[0-9]*):?([0-9]*)$").IsMatch(atom)) // [start:end:step] Phyton slice syntax
				{
					Slice(atom, tail, value, path);
				}
				else if (atom.IndexOf(',') >= 0) // [name1,name2,...]
				{
					foreach (string part in RegExp(@"'?,'?").Split(atom))
						Trace(part + ";" + tail, value, path);
				}
			}

			private void Store(string path, object value)
			{
				if (path != null)
					output(value, path.Split(semicolon));
			}

			private void Walk(string loc, string expr, object value, string path, WalkCallback callback)
			{
				if (system.IsPrimitive(value))
					return;

				if (system.IsArray(value))
				{
					IList list = (IList)value;
					for (int i = 0; i < list.Count; i++)
						callback(i, loc, expr, value, path);
				}
				else if (system.IsObject(value))
				{
					foreach (string key in system.GetMembers(value))
						callback(key, loc, expr, value, path);
				}
			}

			private void WalkWild(object member, string loc, string expr, object value, string path)
			{
				Trace(member + ";" + expr, value, path);
			}

			private void WalkTree(object member, string loc, string expr, object value, string path)
			{
				object result = Index(value, member.ToString());
				if (result != null && !system.IsPrimitive(result))
					Trace("..;" + expr, result, path + ";" + member);
			}

			private void WalkFiltered(object member, string loc, string expr, object value, string path)
			{
				object result = eval(RegExp(@"^\?\((.*?)\)$").Replace(loc, "$1"),
					Index(value, member.ToString()), member.ToString());

				if (Convert.ToBoolean(result, CultureInfo.InvariantCulture))
					Trace(member + ";" + expr, value, path);
			}

			private void Slice(string loc, string expr, object value, string path)
			{
				IList list = value as IList;

				if (list == null)
					return;

				int length = list.Count;
				string[] parts = loc.Split(colon);
				int start = ParseInt(parts[0]);
				int end = ParseInt(parts[1], list.Count);
				int step = parts.Length > 2 ? ParseInt(parts[2], 1) : 1;
				start = (start < 0) ? Math.Max(0, start + length) : Math.Min(length, start);
				end = (end < 0) ? Math.Max(0, end + length) : Math.Min(length, end);
				for (int i = start; i < end; i += step)
					Trace(i + ";" + expr, value, path);
			}

			private object Index(object obj, string member)
			{
				return system.GetMemberValue(obj, member);
			}

			private static object NullEval(string expr, object value, string context)
			{
				//
				// @ symbol in expr must be interpreted specially to resolve
				// to value. In JavaScript, the implementation would look 
				// like:
				//
				// return obj && value && eval(expr.replace(/@/g, "value"));
				//

				return null;
			}
		}

		private sealed class BasicValueSystem : IJsonPathValueSystem
		{
			public bool HasMember(object value, string member)
			{
				if (IsPrimitive(value))
					return false;

				IDictionary dict = value as IDictionary;
				if (dict != null)
					return dict.Contains(member);

				IList list = value as IList;
				if (list != null)
				{
					int index = ParseInt(member, -1);
					return index >= 0 && index < list.Count;
				}

				return false;
			}

			public object GetMemberValue(object value, string member)
			{
				if (IsPrimitive(value))
					throw new ArgumentException("value");

				IDictionary dict = value as IDictionary;
				if (dict != null)
					return dict[member];

				IList list = (IList)value;
				int index = ParseInt(member, -1);
				if (index >= 0 && index < list.Count)
					return list[index];

				return null;
			}

			public IEnumerable GetMembers(object value)
			{
				return ((IDictionary)value).Keys;
			}

			public bool IsObject(object value)
			{
				return value is IDictionary;
			}

			public bool IsArray(object value)
			{
				return value is IList;
			}

			public bool IsPrimitive(object value)
			{
				if (value == null)
					throw new ArgumentNullException("value");

				return Type.GetTypeCode(value.GetType()) != TypeCode.Object;
			}
		}

		private sealed class ListAccumulator
		{
			private readonly IList list;

			public ListAccumulator(IList list)
			{
				Debug.Assert(list != null);

				this.list = list;
			}

			public void Put(object value, string[] indicies)
			{
				list.Add(new JsonPathNode(value, JsonPathContext.AsBracketNotation(indicies)));
			}
		}
	}
	internal class JPath
	{
		private readonly string _expression;
		public List<object> Parts { get; private set; }

		private int _currentIndex;

		public JPath(string expression)
		{
			ValidationUtils.ArgumentNotNull(expression, "expression");
			_expression = expression;
			Parts = new List<object>();

			ParseMain();
		}

		private void ParseMain()
		{
			int currentPartStartIndex = _currentIndex;
			bool followingIndexer = false;

			while (_currentIndex < _expression.Length)
			{
				char currentChar = _expression[_currentIndex];

				switch (currentChar)
				{
					case '[':
					case '(':
						if (_currentIndex > currentPartStartIndex)
						{
							string member = _expression.Substring(currentPartStartIndex, _currentIndex - currentPartStartIndex);
							Parts.Add(member);
						}

						ParseIndexer(currentChar);
						currentPartStartIndex = _currentIndex + 1;
						followingIndexer = true;
						break;
					case ']':
					case ')':
						throw new Exception("Unexpected character while parsing path: " + currentChar);
					case '.':
						if (_currentIndex > currentPartStartIndex)
						{
							string member = _expression.Substring(currentPartStartIndex, _currentIndex - currentPartStartIndex);
							Parts.Add(member);
						}
						currentPartStartIndex = _currentIndex + 1;
						followingIndexer = false;
						break;
					default:
						if (followingIndexer)
							throw new Exception("Unexpected character following indexer: " + currentChar);
						break;
				}

				_currentIndex++;
			}

			if (_currentIndex > currentPartStartIndex)
			{
				string member = _expression.Substring(currentPartStartIndex, _currentIndex - currentPartStartIndex);
				Parts.Add(member);
			}
		}
		// Modifed ParseIndexer to allow for parsing of keys in addition to indexes for arrays
		private void ParseIndexer(char indexerOpenChar)
		{
			_currentIndex++;

			char indexerCloseChar = (indexerOpenChar == '[') ? ']' : ')';
			int indexerStart = _currentIndex;
			int indexerLength = 0;
			bool indexerClosed = false;
			bool keyIndexer = false;	// // are we parsing a key?
			// // Need to make this work for key indexer
			while (_currentIndex < _expression.Length)
			{
				char currentCharacter = _expression[_currentIndex];
				// // test this first, this is an absolute
				if (currentCharacter == indexerCloseChar)
				{
					indexerClosed = true;
					break;
				}
				// // make sure we have an integer, or that we are processing a key
				else if (char.IsDigit(currentCharacter) || keyIndexer)
				{
					indexerLength++;
				}
				// // if this is the first character or at least not a control character, we will assume doing a key
				else if (indexerLength == 0 || (!char.IsControl(currentCharacter)))
				{
					keyIndexer = true;
					indexerLength++;
				}
				// // bad mojo
				else
				{
					throw new Exception("Unexpected character while parsing path indexer: " + currentCharacter);
				}

				_currentIndex++;
			}

			if (!indexerClosed)
				throw new Exception("Path ended with open indexer. Expected " + indexerCloseChar);

			if (indexerLength == 0)
				throw new Exception("Empty path indexer.");

			string indexer = _expression.Substring(indexerStart, indexerLength);
			// // adding square brackets around indexer to allow it to be identified by the JPath::Evaluate
			if ( keyIndexer )
				Parts.Add("[" + indexer + "]");	// add this as a key indexer
			else
				Parts.Add(Convert.ToInt32(indexer, CultureInfo.InvariantCulture));
		}

		internal JToken Evaluate(JToken root, bool errorWhenNoMatch)
		{
			int index;
			JToken current = root;
			foreach (object part in Parts)
			{
				string propertyName = part as string;
				if (propertyName != null)
				{
					// // see if we have a key as an index instead of a property
					if (propertyName.Substring(0, 1) == "[")
					{
						// trim off the brackets and look it up
						string key = propertyName.Replace("[", "");
						key = key.Replace("]", "");
						JToken t = current as JToken;
						if ( t == null )
						{
							if (errorWhenNoMatch)
								throw new Exception("Key '{0}' not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, key, current.GetType().Name));
							return null;
						}
						// if using a named element, we're looking for the property
						//current = null;	// reset this because we don't have a match
						foreach (JToken tok in t)
						{
							JObject obj = tok as JObject;
							JProperty prop = obj.First as JProperty;

							if (prop != null && prop.Name == key)
							{
								if (prop.Value.Type == JTokenType.Object)
									current = prop.Value as JToken;
								else
									current = tok;
								break;
							}
						}

						//current = t.Children()[key] as JToken;
						if (current == null)
						{
							if (errorWhenNoMatch)
								throw new Exception("Key '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, key));
							else
								return null;
						}
//						current = current.First();
//						if (current.Type == JTokenType.Property)
//							current = (current as JProperty).Value;
						if (current == null && errorWhenNoMatch)
							throw new Exception("Key '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, key));
						continue;	// keep going
					}
					JObject o = current as JObject;
					if (o != null)
					{
						current = o[propertyName];
						if (current == null)
						{
							foreach (JToken jTok in o.Children().First<JToken>().Children())
							{
								if ( jTok.HasValues )
									current = jTok[propertyName];
								if ( current != null )
									break;
							}
						}
						if (current == null && errorWhenNoMatch)
							throw new Exception("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, propertyName));
					}
					else
					{
						if (errorWhenNoMatch)
							throw new Exception("Property '{0}' not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, propertyName, current.GetType().Name));
						return null;
					}
				}
				else      // we have an numeric index into a collection
				{
					JArray a = current as JArray;
					index = -1;
					if (a != null && int.TryParse(part.ToString(),out index) )
					{
						if (a.Count <= index)
						{
							if (errorWhenNoMatch)
								throw new IndexOutOfRangeException("Index {0} outside the bounds of JArray.".FormatWith(CultureInfo.InvariantCulture, index));

							return null;
						}
						current = a[index];
						current = current.First();
						if ( current.Type == JTokenType.Property )
							current = (current as JProperty).Value;
					}
					else
					{
						if (errorWhenNoMatch)
							throw new Exception("Index {0} not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, index, current.GetType().Name));
						return null;
					}
				}
			}
			return current;
		}
	}
}
#endif