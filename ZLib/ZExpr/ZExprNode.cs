using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ZLib
{
	public class ZExprNode
	{
		internal class ExprTags
		{
			public const string taggedFunction			= "taggedFunction";
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

		public class ZExprPattern
		{
			public const string sheetPattern				= @"(([A-Za-z]+!)?)";
			public const string cellBasePattern				= 
								@"((R(\[?-?[1-9]+[0-9]*\]?)?C(\[?-?[1-9]+[0-9]*\]?)?)|(\$?[A-Za-z]{1,3}\$?[1-9]+[0-9]*))";
			public const string cellPattern					= "(" + sheetPattern + cellBasePattern + ")";
			public const string rangePattern				= "(" + cellPattern + ":" + cellBasePattern + ")";

			public const string seperatorPattern			= @"(,)";
			public const string parenPattern				= @"(\(|\))";

			public const string beginFunctionPattern		= @"([A-Z]{2,}[\.A-Z0-9]*\s*\()";
			public const string numericLiteralPattern		= @"((?!///)(-?[0-9]{1,}\.?[0-9]*)(?!///))";
			public const string stringLiteralPattern		= @"((?!///)(""[^""]*"")(?!///))";
			public const string boolLiteralPattern			= @"((?!///)((TRUE)|(FALSE))(?!///))";
			public const string operatorPattern				= @"((^=)" 
															+ @"|" + @"((?!///)(\+|\*|((?!/)/(?!/))|\-|\<=|\>=|\<\>|((?!///)\<(?!\{))"
															+ @"|(\>(?!///))|=)(?!///)))";
			public const string textValuePattern			= @"(')";
			public const string tagBegPattern				= @"(///<\{(?<" + ExprTags.tagIdx0 + @">\d)(,(?<" + ExprTags.tagIdx1 + @">\d))?\})";
			public const string tagEndPattern				= @"(>///)";
			public const string taggedPatternFormat			= "(?<{0}>{1}(?<{2}>{3}){4})";
			public static string taggedBeginFunctionPattern = string.Format(taggedPatternFormat
													, ExprTags.taggedFunction
													, tagBegPattern
													, ExprTags.taggedValue
													, beginFunctionPattern
													, tagEndPattern);
			public static string taggedNumericLiteralPattern	= string.Format(taggedPatternFormat
													, ExprTags.taggedNumericLiteral
													, tagBegPattern
													, ExprTags.taggedValue
													, numericLiteralPattern
													, tagEndPattern);
			public static string taggedBoolLiteralPattern	= string.Format(taggedPatternFormat
													, ExprTags.taggedNumericLiteral
													, tagBegPattern
													, ExprTags.taggedValue
													, boolLiteralPattern
													, tagEndPattern);
			public static string taggedStringLiteralPattern	= string.Format(taggedPatternFormat
													, ExprTags.taggedStringLiteral
													, tagBegPattern
													, ExprTags.taggedValue
													, stringLiteralPattern
													, tagEndPattern);
			public static string taggedOperatorPattern	= string.Format(taggedPatternFormat
													, ExprTags.taggedOperator
													, tagBegPattern
													, ExprTags.taggedValue
													, operatorPattern
													, tagEndPattern);
			public static string taggedTextValuePattern	= string.Format(taggedPatternFormat
													, ExprTags.taggedText
													, tagBegPattern
													, ExprTags.taggedValue
													, textValuePattern
													, tagEndPattern);
			public static string taggedRangePattern	= string.Format(taggedPatternFormat
													, ExprTags.taggedRange
													, tagBegPattern
													, ExprTags.taggedValue
													, rangePattern
													, tagEndPattern);
			public static string taggedCellPattern	= string.Format(taggedPatternFormat
													, ExprTags.taggedCell
													, tagBegPattern
													, ExprTags.taggedValue
													, cellPattern
													, tagEndPattern);
			public static string literalPatterns				= numericLiteralPatterns
														+ "|" + stringLiteralPatterns
														+ "|" + boolLiteralPatterns
														+ "|" + textValuePatterns
														;
			public static string numericLiteralPatterns		= taggedNumericLiteralPattern + "|" + numericLiteralPattern;
			public static string stringLiteralPatterns		= taggedStringLiteralPattern + "|" + stringLiteralPattern;
			public static string boolLiteralPatterns			= taggedBoolLiteralPattern + "|" + boolLiteralPattern;
			public static string textValuePatterns			= taggedTextValuePattern + "|" + textValuePattern;
			public static string allPatterns			= taggedRangePattern 
								+ "|" + taggedCellPattern 
								+ "|" + taggedBeginFunctionPattern 
								+ "|" + taggedNumericLiteralPattern 
								+ "|" + taggedStringLiteralPattern 
								+ "|" + taggedBoolLiteralPattern 
								+ "|" + taggedTextValuePattern 
								+ "|" + taggedOperatorPattern 
								+ "|" + rangePattern 
								+ "|" + cellPattern 
								+ "|" + beginFunctionPattern 
								+ "|" + numericLiteralPattern 
								+ "|" + stringLiteralPattern 
								+ "|" + boolLiteralPattern 
								+ "|" + textValuePattern 
								+ "|" + operatorPattern 
								+ "|" + seperatorPattern 
								+ "|" + parenPattern 
								+ "|( )";
		
		}
		//	node types
		public enum ZNodeType { zUnknown, zExpression, zValue, zStringLiteral, zBoolLiteral, zNumericLiteral, zFunction, zCell, zOperator, zRange };
		public List<ZExprNode> children;
		public void AddChild(ZExprNode child)
		{
			if (child != null)
			{
				children.Add(child);
				child.parent = this;	// link child back to this node as its parent
			}
		}
		public ZExprNode(string text, string tag = "", bool isDefaultFunctionArgValue = false)
		{
			children = new List<ZExprNode>();
			parent = null;
			this.text = text;
			this.tag = tag;
			this.isDefaultFunctionArgValue = isDefaultFunctionArgValue;
			// we can do some node type detection, at least for bool and numeric literals
			nodeType = ZNodeType.zUnknown;
			if (Regex.IsMatch(text, ZExprPattern.boolLiteralPatterns))
				nodeType = ZExprNode.ZNodeType.zBoolLiteral;
			else if (Regex.IsMatch(text, ZExprPattern.numericLiteralPatterns))
				nodeType = ZExprNode.ZNodeType.zNumericLiteral;
		}
		public ZExprNode parent;
		public bool isDefaultFunctionArgValue;
		public string text;
		public string tag;		// this may get expanded to a full class, for now, it contains the instruction text associated with this node
		public bool isTagged
		{
			get
			{
				return (tag.Length > 0);
			}
		}
		public ZNodeType nodeType;
		// only those nodes that are tagged, or have a parent that is tagged are being graded
		public bool isBeingGraded
		{
			get
			{
				bool beingGraded = isTagged;
				if ( !beingGraded && parent != null )
					beingGraded = parent.isBeingGraded;

				return beingGraded;
			}
		}
		public bool isFunctionArg
		{
			get
			{
				if ( parent != null && parent.isFunction )
					return true;
				return false;
			}
		}
		public bool isValue
		{
			get
			{
				return ((nodeType==ZNodeType.zValue)|| isLiteral);
			}
		}
		public bool isExpression
		{
			get
			{
				return (nodeType==ZNodeType.zExpression || (children.Count > 0));
			}
		}
		public bool isLiteral 
		{ 
			get 
			{
				return (nodeType==ZNodeType.zNumericLiteral || nodeType==ZNodeType.zStringLiteral|| nodeType==ZNodeType.zBoolLiteral); 
			}
		}
		public bool isCell
		{
			get
			{
				return (nodeType==ZNodeType.zCell);
			}
		}
		public bool isFunction
		{
			get
			{
				return (nodeType==ZNodeType.zFunction);
			}
		}
		public bool isRange
		{
			get
			{
				return (nodeType==ZNodeType.zRange);
			}
		}
		public bool isOperand
		{
			get
			{
				return (isLiteral || isCell || isRange);
			}
		}
		public string expression
		{
			get
			{
				string value = "";
				string	functionArgSeparator = "";
				if (isFunction || (nodeType==ZNodeType.zExpression && children.Count == 1) || isOperand)
					value = text;
				if (isFunction ) // || isExpression)
					value += "(";
				if (children.Count == 1)
					value += children[0].expression;
				else if (!isFunction && children.Count == 2) // boolean operator
					value = children[0].expression + text + children[1].expression;
				else if ( children.Count > 2 )
				{
					// not sure what we got
					foreach (ZExprNode node in children)
					{
						value += functionArgSeparator + node.expression;
						// if we're doing a function, reinsert the comma separator
						if (isFunction)
							functionArgSeparator = ",";
					}
				}
				if (isFunction || (nodeType==ZNodeType.zExpression && text != "=") )	// close out the function
					value += ")";
				return value;
			}
		}
	}
}
