
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ZLib.ZRubric;

namespace ZLib
{
	// TODO - add predicate function isValue, isExpression - which means it starts with an = sign
	public class ZExpr
	{
		// Added an event which is raised whenever there is a difference

		public string expression;		// string version of expression
		public ZExprNode rootNode;
		static private string sheetPattern = @"(([A-Za-z]+!)?)";
		static private string cellBasePattern = 
			@"((R(\[?-?[1-9]+[0-9]*\]?)?C(\[?-?[1-9]+[0-9]*\]?)?)|(\$?[A-Za-z]{1,3}\$?[1-9]+[0-9]*))";
		static private string cellPattern = "(" + sheetPattern + cellBasePattern + ")";
		static private string rangePattern = "(" + cellPattern + ":" + cellBasePattern + ")";
		public bool isValue 
		{ 
			get 
			{ 
				return rootNode != null && rootNode.isLiteral && (rootNode.children.Count == 0); 
			}
		}
		public bool isExpression
		{
			get
			{
				return rootNode != null && ((rootNode.children.Count > 0) || rootNode.isExpression);
			}
		}
		public ZExpr(string expression, ZTask task)
		{
			this.expression = expression;
			string [] tags = null;
			if (task != null && task.parentStep.tags != null)
			{
				ZTags zTags = task.parentStep.tags;
				tags = new string[zTags.Count];
				int tagCount = 0;
				foreach (ZTag stepTag in zTags)
				{
					tags[tagCount++] = stepTag[-1];	// returns the entire tag 
				}
			}
			rootNode = Parse(expression,tags);
		}
		public ZExpr(string expression, string [] tags)
		{
			this.expression = expression;
			rootNode = Parse(expression, tags);
		}
		public ZExpr(string expression)
		{
			this.expression = expression;
			rootNode = Parse(expression);
		}
		static public string CellRangeText(string expression)
		{
			string cellRange = "";
			// try matching most restrictive form first
			foreach (Match match in Regex.Matches(expression, "(" + rangePattern + "|" + cellPattern + ")", RegexOptions.IgnoreCase))
			{
				cellRange = match.Value;
				break;
			}
			return cellRange;
		}
		//static string varNamePattern		= @"(^(?<"   + ZToken.RXFields.VarName			+ @">[^\^\/\*\(\[]+))";
		//static string elementNamePattern	= @"(\.(?<"  + ZToken.RXFields.ElementName		+ @">[\w\d_]+))";
		//static string elementName1Pattern	= @"(\.(?<"  + ZToken.RXFields.ElementName1	+ @">[\w\d_]+))";
		//static string elementName2Pattern	= @"(\.(?<"  + ZToken.RXFields.ElementName2	+ @">[\w\d_]+))";
		//static string parentVarNamePattern  = @"(^\^(?<" + ZToken.RXFields.ParentVarName	+ @">[^\^\*\(\[\.]+))";
		static string tagVarPattern		= @"(((?'Open'///\<)[^{}]*)+((?'Close-Open'})[^{}]*)+)*(?(Open)(?!))$";

		static public ZExprNode Parse(string[] taggedExpression, string[] zTags)
		{
			ZExprNode rootNode			= null;
			string	expression = "";
			foreach(string taggedString in taggedExpression)
				expression += taggedString;

			rootNode = Parse(expression,zTags);

			return rootNode;
		}
		internal class ExprTags
		{
			public const string taggedFunction			= "taggedFunction";
			public const string taggedGroup				= "taggedGroup";
			public const string taggedValue				= "taggedValue";
			public const string taggedNumericLiteral	= "taggedNumericLiteral";
			public const string taggedStringLiteral		= "taggedStringLiteral";
			public const string taggedOperator			= "taggedOperator";
			public const string taggedRange				= "taggedRange";
			public const string taggedCell				= "taggedCell";
			public const string taggedText				= "taggedText";
			public const string tagIdx0					= "tagIdx0";
			public const string tagIdx1					= "tagIdx1";
		}
		static string tagBegPattern			= @"(///<\{(?<" + ExprTags.tagIdx0 + @">\d)(,(?<" + ExprTags.tagIdx1 + @">\d))?\})";
		static string tagEndPattern			= @"(>///)";
		// TODO: look at Resolve logic
		static public ZExprNode Parse(string expression, string[] zTags = null)
		{
			string beginFunctionPattern			= @"([A-Z]{2,}[\.A-Z0-9]*\s*\()";
			string beginGroupPattern			= @"(\()";
			string numericLiteralPattern		= @"((?!///)(-?[0-9]{1,}\.?[0-9]*)(?!///))";
			string stringLiteralPattern			= @"((?!///)(""[^""]*"")(?!///))";
			string boolLiteralPattern			= @"((?!///)((TRUE)|(FALSE))(?!///))";
			string operatorPattern				= @"((^=)" 
												+ @"|" + @"((?!///)(\+|\*|((?!/)/(?!/))|\-|\<=|\>=|\<\>|((?!///)\<(?!\{))"
												+ @"|(\>(?!///))|=)(?!///)))";
			string textValuePattern				= @"(('.*)(?=>///)*)";
//			string textValuePattern				= @"(')";
#region taggedPatterns
			//string stuff = "";
			//string taggedFunction			= "";
			//string taggedGroup				= "";
			//string taggedNumericLiteral		= "";
			//string taggedStringLiteral		= "";
			//string taggedOperator			= "";
			//string taggedRange				= "";
			//string taggedCell				= "";
			//string taggedText				= "";
			//string taggedValue				= "";

			//string taggedPatternFormat			= "(?<{0}>{1}(?<{2}>{3}){4})";
			//string taggedBeginFunctionPattern	= string.Format(taggedPatternFormat
			//										,ExprTags.taggedFunction
			//										,tagBegPattern 
			//										,ExprTags.taggedValue
			//										,beginFunctionPattern
			//										,tagEndPattern );
			//string taggedBeginGroupPattern	= string.Format(taggedPatternFormat
			//										, ExprTags.taggedGroup
			//										, tagBegPattern
			//										, ExprTags.taggedValue
			//										, beginGroupPattern
			//										, tagEndPattern);

			//string taggedNumericLiteralPattern	= string.Format(taggedPatternFormat
			//										, ExprTags.taggedNumericLiteral
			//										, tagBegPattern
			//										, ExprTags.taggedValue
			//										, numericLiteralPattern
			//										, tagEndPattern);
			//string taggedBoolLiteralPattern	= string.Format(taggedPatternFormat
			//										, ExprTags.taggedNumericLiteral
			//										, tagBegPattern
			//										, ExprTags.taggedValue
			//										, boolLiteralPattern
			//										, tagEndPattern);
			//string taggedStringLiteralPattern	= string.Format(taggedPatternFormat
			//										, ExprTags.taggedStringLiteral
			//										, tagBegPattern
			//										, ExprTags.taggedValue
			//										, stringLiteralPattern
			//										, tagEndPattern);
			//string taggedOperatorPattern	= string.Format(taggedPatternFormat
			//										, ExprTags.taggedOperator
			//										, tagBegPattern
			//										, ExprTags.taggedValue
			//										, operatorPattern
			//										, tagEndPattern);
			//string taggedTextValuePattern	= string.Format(taggedPatternFormat
			//										, ExprTags.taggedText
			//										, tagBegPattern
			//										, ExprTags.taggedValue
			//										, textValuePattern
			//										, tagEndPattern);
			//string taggedRangePattern	= string.Format(taggedPatternFormat
			//										, ExprTags.taggedRange
			//										, tagBegPattern
			//										, ExprTags.taggedValue
			//										, rangePattern
			//										, tagEndPattern);
			//string taggedCellPattern	= string.Format(taggedPatternFormat
			//										, ExprTags.taggedCell
			//										, tagBegPattern
			//										, ExprTags.taggedValue
			//										, cellPattern
			//										, tagEndPattern);
#endregion

			// eliminated colon as an operator
			//string operatorPattern = @"(\+|\*|/|\-|\<=|\>=|\<\>|\<|\>|=|:)";
			string seperatorPattern		= @"(,)";
			string parenPattern			= @"(\(|\))";
			string allPatterns =		rangePattern 
								+ "|" + cellPattern 
								+ "|" + beginFunctionPattern 
								+ "|" + numericLiteralPattern 
								+ "|" + stringLiteralPattern 
								+ "|" + boolLiteralPattern 
								+ "|" + textValuePattern 
								+ "|" + operatorPattern 
								+ "|" + seperatorPattern 
								+ "|" + beginGroupPattern
								+ "|" + tagBegPattern
								+ "|" + tagEndPattern
								+ "|" + parenPattern 
								+ "|( )";
			ZExprNode			rootNode			= null;
			ZExprNode			activeOpNode		= null;
			ZExprNode			exprGroupNode		= null;
			Stack<ZExprNode>	exprStack			= new Stack<ZExprNode>();	// this is used for grouping expressions using parens
			Stack<ZExprNode>	rootNodeStack		= new Stack<ZExprNode>();
			Stack<ZExprNode>	activeOpNodeStack	= new Stack<ZExprNode>();	// for expressions, if the first thing is an "=" that will be considered the active opNode/root node

			bool inGroup = false;
			bool inTag = false;
			int		tagIdx0 = -1;
			int		tagIdx1 = -1;
			string	matchValue = "";
			string	tag = "";
			MatchCollection matches = Regex.Matches(expression, allPatterns, RegexOptions.IgnoreCase);
			foreach (Match match in matches)
			{
				if ( !inTag )
					tag = "";	// reset the tag 
				matchValue = match.Value;
				if (match.Groups.Count > 0)
				{
					try
					{
						if (int.TryParse(match.Groups[ExprTags.tagIdx0].Value, out tagIdx0))
						{
							tag = zTags[tagIdx0];
							if (int.TryParse(match.Groups[ExprTags.tagIdx1].Value, out tagIdx1))
							{
								tag = tag.Split('\t')[tagIdx1];
							}
							else
							{
								tag = tag.Replace(" \t", " ");	// remove embedded tabs and just grab the whole thing
								tag = tag.Replace("\t ", " ");	// remove embedded tabs and just grab the whole thing
								tag = tag.Replace("\t.", ".");	// remove embedded tabs and just grab the whole thing
								tag = tag.Replace("  ", " ");	// remove embedded tabs and just grab the whole thing
							}
						}
					}
					catch ( Exception ex )
					{
						tag = "";	// string.Format("idx0{0}, idx1{1}", match.Groups[ExprTags.tagIdx0].Value,match.Groups[ExprTags.tagIdx1].Value);
						string msg = ex.Message;
					}
					#region oldTaggedStuff
					// see what we got
					//taggedFunction			= match.Groups[ExprTags.taggedFunction].Value;
					//taggedGroup				= match.Groups[ExprTags.taggedGroup].Value;
					//taggedNumericLiteral	= match.Groups[ExprTags.taggedNumericLiteral].Value;
					//taggedStringLiteral		= match.Groups[ExprTags.taggedStringLiteral].Value;
					//taggedOperator			= match.Groups[ExprTags.taggedOperator].Value;
					//taggedRange				= match.Groups[ExprTags.taggedRange].Value;
					//taggedCell				= match.Groups[ExprTags.taggedCell].Value;
					//taggedText				= match.Groups[ExprTags.taggedText].Value;
					//taggedValue				= match.Groups[ExprTags.taggedValue].Value;
					//if (match.Groups[ExprTags.taggedValue].Success)
					//	matchValue = taggedValue;
					#endregion
				}
				// we're now in a tag
				if (!inTag && tag.Length > 0)
				{
					inTag = true;
					continue;
				}
				// and now we're not
				else if (inTag && matchValue == ">///")
				{
					inTag = false;
					continue;
				}
				// construct this node, then, adjust root node
				ZExprNode node = new ZExprNode(matchValue,tag);

				// if we're in a group and we encounter a comma, add the current root node to
				// the children of the current exprGroupNode and reset
				// this is very similar to ending a group, but is more of a subGroup since we should
				// have just completed processing an expression
				if (inGroup)
				{
					if (matchValue == ",")
					{
						exprGroupNode.AddChild(rootNode);
						rootNode = null;
						activeOpNode = null;
						continue;
					}
				}
				// if we aren't in a group and the rootNode is null, and we're starting with an =, this will be an expression
				// if not, we're some sort of literal
				else if (rootNode == null && rootNodeStack.Count() == 0)
				{
					if (matchValue == "=")
					{
						node.nodeType = ZExprNode.ZNodeType.zExpression;
						rootNode = node;
						activeOpNode = node;
						continue;
					}
					else if (matchValue.StartsWith("'")) 	// if the first thing we get is a tick, we've got a string, we're done
					{
						node.nodeType = ZExprNode.ZNodeType.zStringLiteral;
						rootNode = node;
						if (inTag && matchValue.EndsWith(">///"))
						{
							inTag = false;
							rootNode.text = matchValue.Substring(1).Replace(">///","");	// the text will be whatever the actual value is
						}
						else
						{
							//						rootNode.text = expression.Substring(1);	// the text will be whatever the actual value is
							rootNode.text = matchValue.Substring(1);	// the text will be whatever the actual value is
						}
						return rootNode;
					}
				}
				if (Regex.IsMatch(matchValue, beginFunctionPattern))
				{
					node.nodeType = ZExprNode.ZNodeType.zFunction;
					// we can strip off the opening paren
					node.text = node.text.Replace("(","").Trim();
				}
				// see if we're grouping first
				if (matchValue == "(" || node.isFunction )
				{
					inGroup = true;
					exprStack.Push(node);
					// make this the activeNode
					exprGroupNode = node;
					if (rootNode != null)	// if we were in a group, push the root node
						rootNodeStack.Push(rootNode);
					rootNode = null;	// start of new group, use this to capture this group's root
					if (activeOpNode != null)
						activeOpNodeStack.Push(activeOpNode);
					activeOpNode = null;
					continue;
				}
				if (matchValue == ")")
				{
					// if this rootNode doesn't have a known type, it must be a literal, see which one
					if (rootNode != null)
					{
						if (rootNode.nodeType == ZExprNode.ZNodeType.zUnknown)
						{
							node.nodeType = ZExprNode.ZNodeType.zStringLiteral;
							if (Regex.IsMatch(matchValue, boolLiteralPattern))
								node.nodeType = ZExprNode.ZNodeType.zBoolLiteral;
							else if (Regex.IsMatch(matchValue, numericLiteralPattern))
								node.nodeType = ZExprNode.ZNodeType.zNumericLiteral;
						}
						// if the exprGroupNode type is unknown, set it to zExpression and add the node as a child
						if (exprGroupNode.nodeType == ZExprNode.ZNodeType.zUnknown)
						{
							exprGroupNode.nodeType = ZExprNode.ZNodeType.zExpression;
							//exprGroupNode.AddChild(node);
						}
						// add the expression as a child
						exprGroupNode.AddChild(rootNode);
					}
					string expr = exprGroupNode.expression;
					// we need to tweak the rootNode, if we have nested root
					if (rootNodeStack.Count() > 0)
					{
						// if we had a rootNode, reinstate it and add the existing expression as a child
						rootNode = rootNodeStack.Pop();
						rootNode.AddChild(exprGroupNode);
					}
					else
					{
						// we'll make the expression group the root
						rootNode = exprGroupNode;
					}
					// we need to tweak the activeOpNode
					if (activeOpNodeStack.Count() > 0)
					{
						activeOpNode = activeOpNodeStack.Pop();
					}
					else
					{
						activeOpNode = null;
					}
					// remove the start of the group from the stack
					exprStack.Pop();
					// see if we're in a nested group
					if (exprStack.Count() > 0)
					{
						inGroup = true;
						exprGroupNode = exprStack.Peek();
					}
					else
					{
						exprGroupNode = null;
						inGroup = false;
					}
					continue;	// bounce to the next match
				}
				else if (matchValue == " ")	// TODO: make ignoring intervening spaces a preference option
					continue;
				if (Regex.IsMatch(matchValue, stringLiteralPattern))
					node.nodeType = ZExprNode.ZNodeType.zStringLiteral;
				else if (Regex.IsMatch(matchValue, boolLiteralPattern))
					node.nodeType = ZExprNode.ZNodeType.zBoolLiteral;
				else if (Regex.IsMatch(matchValue, numericLiteralPattern))
					node.nodeType = ZExprNode.ZNodeType.zNumericLiteral;
				if (Regex.IsMatch(matchValue, cellPattern))
					node.nodeType = ZExprNode.ZNodeType.zCell;
				if (Regex.IsMatch(matchValue, rangePattern))
					node.nodeType = ZExprNode.ZNodeType.zRange;
				if (rootNode == null)
				{
					rootNode = node;
				}
				else
				{
					// depending what we have, do something different
					// TODO: still need to figure out how push and pop the expression
					// need to push right op as a child to the active op node
					//if (Regex.IsMatch(match.Value, numericLiteralPattern ) )
					//	node.nodeType = ZExprNode.ZNodeType.zLiteral;
					//if (Regex.IsMatch(match.Value, cellPattern))
					//	node.nodeType = ZExprNode.ZNodeType.zCell;
					// if we have an operand, literal, cell or range
					if (node.isLiteral || node.isCell || node.isRange)
					{
						activeOpNode.AddChild(node);
					}
					else if (Regex.IsMatch(matchValue, operatorPattern))
					{
						node.nodeType = ZExprNode.ZNodeType.zOperator;
						if (activeOpNode == null)
						{
							node.AddChild(rootNode);
							rootNode = node;
						}
						// compare precedence and make the operator with the lowest precedence the root
						else if (Operators.Precedence(node.text) < Operators.Precedence(activeOpNode.text))
						{
							// if the active node is the root node, move it too
							if (rootNode == activeOpNode)
								rootNode = node;
							node.AddChild(activeOpNode);
						}
						else
						{
							// swap the right op node[1] with the new node and add 
							if (activeOpNode.children.Count > 1)
							{
								node.AddChild(activeOpNode.children[1]);
								activeOpNode.children[1] = node;
							}
							else
							{
								node.AddChild(activeOpNode.children[0]);
								activeOpNode.children[0] = node;
							}

						}
						activeOpNode = node;
					}
				}
			}
			// if the rootNode has no children and it is a literal value, it's type is a value
			if ( rootNode != null && rootNode.children.Count == 0 && rootNode.nodeType == ZExprNode.ZNodeType.zUnknown )
				rootNode.nodeType = ZExprNode.ZNodeType.zValue;
			//string expr2 = rootNode.expression;
			return rootNode;
		}

		public override bool Equals(object obj)
		{
			ZExpr rExpr = null;
			try
			{
				rExpr = obj as ZExpr;
				return Compare(rootNode, rExpr.rootNode);
			}
			catch
			{
				return false;
			}
		}
		// Declare which operator to overload (+), the types 
		// that can be added (two Complex objects), and the 
		// return type (Complex):
        public static bool operator ==(ZExpr lExpr, ZExpr rExpr)
		{
			bool result = false;
			// if an expression delta handler has been installed, initialize the delta event args with
			// the expressions being compared
			if (ExprDeltaHandler != null)
			{
				zExprDeltaEventArgs.zExprDelta.Initialize(lExpr, rExpr);
			}
			result = Compare(lExpr.rootNode, rExpr.rootNode);
			return result;
		}
		public static bool operator !=(ZExpr lExpr, ZExpr rExpr)
		{
			return !Compare(lExpr.rootNode, rExpr.rootNode);
		}
		public override int GetHashCode()
		{
			return 0;
		}
		public delegate bool ZExprDeltaHandler(ZExprDeltaEventArgs zExprDeltaEventArgs);
		public static ZExprDeltaHandler ExprDeltaHandler = null;
		private static ZExprDeltaEventArgs zExprDeltaEventArgs = new ZExprDeltaEventArgs();
        public static bool Compare(ZExpr lExpr, ZExpr rExpr, ZScenario scenario)
        {
            zExprDeltaEventArgs.scenario = null;
            if (ExprDeltaHandler != null)
            {
                zExprDeltaEventArgs.scenario = scenario;
            }
            return lExpr == rExpr;
        }
		public static bool Compare(ZExprNode lNode, ZExprNode rNode)
		{
			bool equivalent = true;		// assume they are equal
			bool allowed = false;		// assume if we find a difference, that it is not allowed
			string deltaName = "";
			// if an expression delta handler has been installed, set the nodes being compared
			if (ExprDeltaHandler != null)
			{
				zExprDeltaEventArgs.zExprDelta.lExprNode = lNode;
				zExprDeltaEventArgs.zExprDelta.rExprNode = rNode;
			}
			if (lNode.nodeType != rNode.nodeType)
			{
				equivalent = false;	// by default, not equivalent
				// see if there is a handler installed which will determine whether or not we 
				// are allowing for partial matches
				if (ExprDeltaHandler != null)
				{
					// TODO: refine Mismatch handling
					zExprDeltaEventArgs.SetContext(ZExprDelta.Contexts.Content, ZExprDelta.Qualifiers.Expression,
							ZExprDelta.Qualifiers.Expression);
					allowed = ExprDeltaHandler(zExprDeltaEventArgs);
					equivalent = allowed;
				}
				return false;	// for now, these cannot be the same, may change later
			}

			// progress from the easiest checks to make to the more involved
			// if the text values don't match
			switch (lNode.nodeType)
			{
				case ZExprNode.ZNodeType.zStringLiteral:
				case ZExprNode.ZNodeType.zNumericLiteral:
				case ZExprNode.ZNodeType.zBoolLiteral:
				case ZExprNode.ZNodeType.zValue:
					if (lNode.isLiteral && rNode.isLiteral && (lNode.text != rNode.text))
					{
						equivalent = false;	// by default, not equivalent
						// see if there is a handler installed which will determine whether or not we 
						// are allowing for partial matches
						if (ExprDeltaHandler != null)
						{
							zExprDeltaEventArgs.SetContext(ZExprDelta.Contexts.Content, ZExprDelta.Qualifiers.Value, "Value");
							allowed = ExprDeltaHandler(zExprDeltaEventArgs);
							equivalent = allowed;
						}
						return equivalent;
					}
					break;
				case ZExprNode.ZNodeType.zFunction:
					// make sure we got the same function
					if (lNode.text != rNode.text)
					{
						equivalent = false;		// mismatch
						// eventually these errors will be consolidated
						if (ExprDeltaHandler != null)
						{
							zExprDeltaEventArgs.SetContext(ZExprDelta.Contexts.Content, ZExprDelta.Qualifiers.Expression, "Function");
							allowed = ExprDeltaHandler(zExprDeltaEventArgs);
							equivalent = allowed;
						}
					}
					// grab function args list and use it to iterate through the possible arguments
					ZFunction zFunction = ZRubric.ZRubric.activeFunctions[lNode.text];
					// iterate through each of the children and make sure they are equivalent
					// iterate through the entire list
					// TODO: create dictionary to link instructions to various elements, eventually use guids, for now, encoded text
					// use the position of the child to grab the appropriate function arg
					ZFunctionArg	zFunArg = null;
					ZExprNode		defaultFunArgNode = null, lFunArgNode = null, rFunArgNode = null;
					int				functionArgCount = zFunction.functionArgs.Count;
					for (int funArgIdx = 0; funArgIdx < functionArgCount; ++funArgIdx)
					{
						// we can do some additional checking here now
						zFunArg = zFunction.functionArgs[funArgIdx];
						defaultFunArgNode = zFunArg.defaultExprNode;
						lFunArgNode = defaultFunArgNode;	// assign defaulted values then override if they exist
						rFunArgNode = defaultFunArgNode;
						if (funArgIdx < lNode.children.Count )
							lFunArgNode = lNode.children[funArgIdx];
						if ( funArgIdx < rNode.children.Count )
							rFunArgNode = rNode.children[funArgIdx];
						// if both are default values, we're done, unless we allow ,,
						// TODO: handle missing intermediate function arguments
						if ( lFunArgNode.isDefaultFunctionArgValue && rFunArgNode.isDefaultFunctionArgValue )
							break;
						// invoke special delta handling for use or omission of default value
                        if (ExprDeltaHandler != null && (lFunArgNode.isDefaultFunctionArgValue || rFunArgNode.isDefaultFunctionArgValue))
                        {
                            deltaName = "FunctionArgs.";
                            // if we defaulted the left arg
                            if (lFunArgNode.isDefaultFunctionArgValue)
								deltaName += "DefaultValue.Found";		// submission included default value when not requested
                            else
                                deltaName += "Missing";	// submission omitted default value when it was requested
                            zExprDeltaEventArgs.SetContext(ZExprDelta.Contexts.Content, ZExprDelta.Qualifiers.Expression, deltaName);
                            zExprDeltaEventArgs.feedbackArgs = new string[] { zFunArg.id, zFunction.id };
                            allowed = ExprDeltaHandler(zExprDeltaEventArgs);
                            equivalent &= allowed;	// if not allowed, we're not equivalent
                        }
                        else
                        {
                            // compare them, set to false if was false or is false, regardless
                            equivalent &= Compare(lFunArgNode, rFunArgNode);
                        }
					}
					break;
				case ZExprNode.ZNodeType.zCell:
					// make sure we have equivalent cells
					
					if (lNode.text != rNode.text)
					{
						equivalent = false;		// mismatch
						// eventually these errors will be consolidated
						if (ExprDeltaHandler != null)
						{
							zExprDeltaEventArgs.SetContext(ZExprDelta.Contexts.Content, ZExprDelta.Qualifiers.Expression, "CellRef_Mismatch");
							allowed = ExprDeltaHandler(zExprDeltaEventArgs);
							equivalent = allowed;
						}
					}
					break;
				case ZExprNode.ZNodeType.zRange:
					// make sure we have equivalent ranges
					if (lNode.text != rNode.text)
					{
						equivalent = false;		// mismatch
						// eventually these errors will be consolidated
						if (ExprDeltaHandler != null)
						{
							zExprDeltaEventArgs.SetContext(ZExprDelta.Contexts.Content, ZExprDelta.Qualifiers.Expression, "Function.FunctionArgs.Range.Bounds");
							allowed = ExprDeltaHandler(zExprDeltaEventArgs);
							equivalent = allowed;
						}
					}
					break;
				case ZExprNode.ZNodeType.zOperator:
				case ZExprNode.ZNodeType.zExpression:
				case ZExprNode.ZNodeType.zUnknown:
				default:
					if ( lNode.nodeType == ZExprNode.ZNodeType.zOperator )
					{
						// make sure we have equivalent operators
						if (lNode.text != rNode.text)
						{
							equivalent = false;		// mismatch
							// eventually these errors will be consolidated
							if (ExprDeltaHandler != null)
							{
								zExprDeltaEventArgs.SetContext(ZExprDelta.Contexts.Content, ZExprDelta.Qualifiers.Expression, "Operator_Mismatch");
								allowed = ExprDeltaHandler(zExprDeltaEventArgs);
								equivalent = allowed;
							}
							// return equivalent;
						}
					}
					if (lNode.children.Count() != rNode.children.Count())
					{
						equivalent = false;	// by default, not equivalent
						// see if there is a handler installed which will determine whether or not we 
						// are allowing for partial matches
						if (ExprDeltaHandler != null)
						{
							zExprDeltaEventArgs.SetContext(ZExprDelta.Contexts.Content, ZExprDelta.Qualifiers.Expression, "Operand_Mismatch");
							allowed = ExprDeltaHandler(zExprDeltaEventArgs);
							equivalent = allowed;
						}
						return equivalent;
					}
					// now compare the actual children based on the count
					// if only one child, they better be the same
					if (lNode.children.Count() == 1)
					{
						if ( Compare(lNode.children[0], rNode.children[0]) == false )
							equivalent = false;
					}
					// if two children and the current node is either + or *, verify in either direction
					else if (lNode.children.Count() == 2 && (lNode.text == "+" || lNode.text == "*"))
					{
						if (Compare(lNode.children[0], rNode.children[0]) && Compare(lNode.children[1], rNode.children[1]) == false)
						{
							// try the other way
							if (Compare(lNode.children[0], rNode.children[1]) && Compare(lNode.children[0], rNode.children[1]) == false)
								equivalent = false;
						}
					}
					else
					{
						// iterate through each of the children and make sure they are equivalent
						// if there is a mismatch, we're done
						for (int child = 0; child < lNode.children.Count() && equivalent; ++child)
							if (Compare(lNode.children[child], rNode.children[child]) == false)
								equivalent = false;
					}
					break;
			}
			// TODO: do something to make sure the nodes being compared are the same type
			// TODO - if this is a function that allows for default values, this may not be a problem
			// they should both have the same number of children
			return equivalent;
		}
		// TODO: I have to think about this to figure out the best way to do this
		//			It may make more sense to examine the inbound expression and look for the relational
		//			operators and automatically generate those cases. 
		//			For now Z will at least prompt for the alternatives

		public static int Distance(ZExprNode lNode, ZExprNode rNode)
		{
			int distance = 0;		// so far there is no distance
			// progress from the easiest checks to make to the more involved
			// if the text values don't match
			if (lNode.text != rNode.text)
				return distance;
			// they should both have the same number of children
			if (lNode.children.Count() != rNode.children.Count())
				return distance;
			// now compare the actual children based on the count
			// if only one child, they better be the same
			if (lNode.children.Count() == 1)
				distance = Distance(lNode.children[0], rNode.children[0]);
			// if two children and the current node is either + or *, verify in either direction
			else if (lNode.children.Count() == 2 && (lNode.text == "+" || lNode.text == "*"))
			{
				distance = Distance(lNode.children[0], rNode.children[0]) + Distance(lNode.children[1], rNode.children[1]);
				// try the other way
				if (distance != 0)
					distance = Distance(lNode.children[0], rNode.children[1]) + Distance(lNode.children[0], rNode.children[1]);
			}
			else
			{
				// iterate through each of the children and make sure they are equivalent
				// if there is a mismatch, we're done
				for (int child = 0; child < lNode.children.Count(); ++child)
					distance += Distance(lNode.children[child], rNode.children[child]);
			}
			return distance;
		}

	}
	
	public class Operators
	{
		// only handle binary operators for now
		static public int Precedence(string op)
		{
			int precedence = 0;
			//a) OPERATOR Precedence
			//    7) reference. : (colon), (single space), (comma)
			//    6) – Negation (as in –1)
			//    5) % Percent
			//    4) ^ Exponentiation
			//    3) * and / Multiplication and division
			if (op == "*" || op == "/")
				precedence = 3;
			//    2) + and – Addition and subtraction
			else if (op == "+" || op == "-")
				precedence = 2;
			//    1) & Connects two strings of text (concatenation)
			else if (op == "&" )
				precedence = 1;
			//    0) comparison =, < >, <=, >=, <>
			else if (op == "=" || op == "<" || op == ">" || op == "<=" || op == ">=" || op == "<>" )
				precedence = 0;
			
			return precedence;
		}
		public string text;
		public int precedence;
		public Operators(string text, int precedence)
		{
			this.text = text;
			this.precedence = precedence;
		}
	}

}
