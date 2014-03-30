using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// OK, I think I need to create an expression tree and then perform tests against the individual branches as is 
// needed
// We have three types of branches, unary, binary and nary
// I will examine from left to right and adjust the root as needed, by default, the first token will assume
// the role of root
namespace JsonExplorer
{
	class ZExprNode
	{
		//	node types
		public enum ZNodeType { zLiteral, zFunction, zCell, zOperator };
		public List<ZExprNode>	children;
		public ZExprNode( string text)
		{
			children = new List<ZExprNode>();
			this.text = text;
		}
		public string	text;
	}
	class ZExpression
	{

		// add options to constructor to set parameters for making comparisons
		//2) chart of equivalents
		//    +	+	- arithmetic operators are the same
		//    *	*
		//    =	=
		//    <	>	- relational use opposite
		//    <=	>=
		//    >	<
		//    >=	<=
		//a) OPERATOR Precedence
		//    9) reference. : (colon), (single space), (comma)
		//    8) – Negation (as in –1)
		//    7) % Percent
		//    6) ^ Exponentiation
		//    5) * and / Multiplication and division
		//    4) + and – Addition and subtraction
		//    3) & Connects two strings of text (concatenation)
		//    0) comparison =, < >, <=, >=, <>

		//* R1C1 style cell references
		//    # absolute cell reference 
		//        =R30C7 R#C# where # is absolute row and column on same sheet
		//        =Documentation!R3C2 on a different sheet
		//    # relative cell reference, have at least one set of brackets and do not use + for positive offset
		//        =R[-17]C[-4] value in brackets is relative row column
		//        =R[17]C - reference to another row in same column
		//        =RC[-4] - reference to another column in same row

		//    parse inside out looking for operands of operators that have an alternate order equivalent
		// TODO: change to a rubricResult? And/or add a rubricOptions
		// TODO: add version that parses the original expression and then determines if checkExpr is equivalent
		static public ZExprNode Parse(string expr)
		{
			ZExprNode zRootNode = new ZExprNode("");
			// we are at level 0, for parens
			// look for root node, this should either be a singular cell, operator or function
			string tokText = "";
			int exprIdx = 0;
			// look for an operator or delimiter
			int delimIdx = expr.IndexOfAny({',','(','+'});

			return zRootNode;
		}
		// this function will try to return the leading cell reference, if present, in the expression
		// it will recognize R#C# and default formats, if it doesn't find a cell reference, it returns
		// an empty string
		static public string CellReference(string expr)
		{
			string cellReference = "";
			// use regex
			string cellMatch = "(Mr\\.? |Mrs\\.? |Miss |Ms\\.? )";

			// if the first character is not an alpha, bubuy
			

			return cellReference;
		}
		static public bool isEquivalent(string origExpr, string checkExpr)
		{
			bool equivalent = true;		// optimistic assumption
			// assumptions
			//	1) origExpr has no extra spaces
			//	2)

			//	Start making comparisons
			//	1) if they are identical, ok
			if (origExpr == checkExpr)
				return equivalent;
			// 2) remove spaces, generally they are superfluous
			if (origExpr == checkExpr.Replace(" ", ""))
				return equivalent;
			// start parsing, tokenize the expression

			// now we get down to it, many expressions have commas and those bind the highest
			// so, we'll split the expression into subexpressions and evaluate, providing that commas are present
			if (origExpr.Contains(",") && checkExpr.Contains(","))
			{
				// make sure their respective split arrays contain the same number of elements, then
				// start comparing the subexpressions, this will be a recursive call
				string[] origExprArray = origExpr.Split(',');
				string[] checkExprArray = checkExpr.Split(',');
				if (origExprArray.Count() != checkExprArray.Count())
					return !equivalent;
				for (int subExprIdx = 0; subExprIdx < origExprArray.Count(); ++subExprIdx)
				{
					equivalent = isEquivalent(origExprArray[subExprIdx], checkExprArray[subExprIdx]);
					if (!equivalent)
						return equivalent;
				}
			}
			// TODO: add support for nested function expressions
			// if the expression is the front-end of a function, their will most likely be an opening paren
			// but not a closing paren, in these cases, we will split at the left paren and disregard what
			// is on the left
			if (origExpr.Contains('('))
			{
				if ( !checkExpr.Contains('('))
					return !equivalent;
				// see what we have to the left of the paren
				int origIdx = origExpr.IndexOf('('), checkIdx = checkExpr.IndexOf('(');
				// if there is nothing, we should have a matching close paren, look for last closing paren
				// looks like we have an algebraic expression
				if (origIdx == 0 && checkIdx == 0)
				{

				}
				// if we don't have an operator or another "delimiter", comma or open paren, it should be a
				// function
				int oTokIdx;
				string oTok = "", cTok = "";
				for (oTokIdx = origIdx - 1; oTokIdx >= 0; --oTokIdx)
				{
					switch (origExpr[oTokIdx])
					{
						case '+':
						case '*':
							case '
					}
				}
			}
			return equivalent;
		}

	}
}
