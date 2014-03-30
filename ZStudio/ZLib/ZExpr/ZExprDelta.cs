using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLib.ZRubric;

namespace ZLib
{
	/// <summary>
	/// What's the delta. Not all deltas are created equal. When performing a comparison of two expressions,
	/// what appears to be a delta, may not be a delta and therefore the expressions would still be considered
	/// equal. This class provides a mechanism for the parent process to assist, as needed, in the comparison process.
	/// </summary>
	public class ZExprDelta 
	{
		/// <summary>
		/// Default constructor for a new instance of the <see cref="ZExprDelta"/> class.
		/// </summary>
		internal ZExprDelta()
		{
			Initialize();
		}
		/// <summary>
		/// Initializes an instance of the <see cref="ZExprDelta"/> class.
		/// </summary>
		public void Initialize()
		{
			this.rExpr = null;
			this.lExpr = null;
			this.rExprNode = null;
			this.lExprNode = null;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ZExprDelta"/> class.
		/// </summary>
		/// <param name="lExpr">The lefthand expression.</param>
		/// <param name="rExpr">The righthand expression.</param>
		/// <param name="lExprNode">The lefthand expression node.</param>
		/// <param name="rExprNode">The righthand expression node.</param>
		internal ZExprDelta(ZExpr lExpr, ZExpr rExpr, ZExprNode lExprNode, ZExprNode rExprNode)
		{
			Initialize(lExpr,rExpr,lExprNode,rExprNode);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ZExprDelta"/> class.
		/// </summary>
		/// <param name="lExpr">The lefthand expression.</param>
		/// <param name="rExpr">The righthand expression.</param>
		/// <param name="lExprNode">The lefthand expression node, optional, default value is null.</param>
		/// <param name="rExprNode">The righthand expression node, optional, default value is null.</param>
		public void Initialize(ZExpr lExpr, ZExpr rExpr, ZExprNode lExprNode = null, ZExprNode rExprNode = null)
		{
			this.lExpr = lExpr;
			this.rExpr = rExpr;
			this.lExprNode = lExprNode;
			this.rExprNode = rExprNode;
		}
		/// <summary>
		/// Gets or sets the rExpr.
		/// </summary>
		/// <value>The ZExpr.</value>
		public ZExpr rExpr { get; private set; }
		/// <summary>
		/// Gets or sets the lExpr.
		/// </summary>
		/// <value>The ZExpr.</value>
		public ZExpr lExpr { get; private set; }
		/// <summary>
		/// Gets or sets the rExprNode.
		/// </summary>
		/// <value>The righthand ZExprNode being compared.</value>
		public ZExprNode rExprNode { get; set; }
		/// <summary>
		/// Gets or sets the lExprNode.
		/// </summary>
		/// <value>The lefthand ZExprNode being compared.</value>
		public ZExprNode lExprNode { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ZExprDelta"/> is allowed.
		/// </summary>
		/// <value><c>true</c> if allowed; otherwise, <c>false</c>.</value>
		public bool Allowed { get; set; }
		public class Contexts
		{
			public const string Content	="Content";
			public const string Format = "Format";
		}
		public class Qualifiers
		{
			public const string Value ="Value";
			public const string Action = "Action";
			public const string Expression = "Expression";
		}
	}

	//     public EventHandler<ErrorEventArgs> Error { get; set; }
	// TODO: implement static preferences to control which events are raised, or, register handlers for the various
	//		deltas and/or a combination of the two
	//
	//	Set preferences when project is loaded
	//	Use "context" to determine what to do
	//	evaluate functions at node level, rather than root level
	//		enable tagging/linking of expression nodes to directions
	//		when comparing function nodes, deal with default values at that time
	//
	//	
	/// <summary>
	/// Provides data for the Error event.
	/// </summary>
	public class ZExprDeltaEventArgs : EventArgs
	{
        /// <summary>
        /// Gets the current ZScenario the event is being raised for.
        /// </summary>
        /// <value>The current ZScenario the event is being raised for.</value>
        public ZScenario scenario{ get;  set; }
        /// <summary>
		/// Gets the current ZExprDelta the event is being raised for.
		/// </summary>
		/// <value>The current ZExprDelta the event is being raised for.</value>
		public ZExprDelta zExprDelta { get; private set; }
		/// <summary>
		/// Gets the current delta context the event is being raised for.
		/// </summary>
		/// <value>The current delta context the event is being raised for, Content, Formatting, ... </value>
		public string deltaContext { get; private set; }
		/// <summary>
		/// Gets the current context qualifier of the delta the event is being raised for.
		/// </summary>
		/// <value>The current context qualifier of the delta the event is being raised for, Expressions, ... </value>
		public string contextQualifier { get; private set; }
		/// <summary>
		/// Gets the feedbackArgs used when formatting the feedback message.
		/// </summary>
		/// <value>string array of feedbackArgs used to customize message </value>
		public string[] feedbackArgs { get; set; }
		/// <summary>
		/// Gets the delta name used to lookup how to handle this potential error for the event is being raised, ie FunctionArg_DefaultValue, FunctionArg_Missing.
		/// </summary>
		/// <value>delta name used to lookup how to handle this potential error for the event is being raised, ie FunctionArg_DefaultValue, FunctionArg_Missing.</value>
		public string deltaName { get; private set; }
		public void SetContext(string deltaContext, string contextQualifier, string deltaName)
		{
			this.deltaContext = deltaContext;
			this.contextQualifier = contextQualifier;
			this.deltaName = deltaName;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ZExprDeltaEventArgs"/> class.
		/// </summary>
		/// <param name="zExprDelta">The expression delta.</param>
		public ZExprDeltaEventArgs(ZExprDelta zExprDelta, string deltaContext, string contextQualifier, string deltaName, string[] feedbackArgs = null)
		{
			this.zExprDelta = zExprDelta;
			this.deltaContext = deltaContext;
			this.contextQualifier = contextQualifier;
			this.deltaName = deltaName;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ZExprDeltaEventArgs"/> class.
		/// </summary>
		public ZExprDeltaEventArgs( )
		{
			this.zExprDelta = new ZExprDelta();
		}
	}

}
