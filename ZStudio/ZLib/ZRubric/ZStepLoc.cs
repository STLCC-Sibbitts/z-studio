#region using
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
#endregion

namespace ZLib.ZRubric
{
	// ZStepLoc - used to tie a task back to the text within the associated step
	public class ZStepLoc : ZObject<ZStepLoc>
	{
		public ZStepLoc(ZObject<ZStepLoc> obj) : base(obj) { }
		public ZStepLoc(JObject jObj) : base(jObj) { }
		public ZStepLoc()
			: base(new JObject())
		{
			startPos = -1;		// this will be used as a flag to indicate that the entire step is selected
			length = -1;
		}
		public ZStepLoc(int startPos, int length)
			: base(new JObject())
		{
			this.startPos = startPos;
			this.length = length;
		}
		public int startPos
		{
			get { return GetIntValue("startPos"); }
			set { SetValue("startPos", value); }
		}
		public int length
		{
			get { return GetIntValue("length"); }
			set { SetValue("length", value); }
		}
	}

}
