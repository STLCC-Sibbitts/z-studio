#region using
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
#endregion

namespace ZLib.ZRubric
{
	// ZStepLoc - used to tie a task back to the text within the associated step
	public class ZStepLoc
	{
		public enum LocType
		{
			task,
			tag
		}
		public ZStepLoc(string key, int startPos, int length, LocType locType)
		{
			this.startPos = startPos;
			this.length = length;
			this.key = key;
			this.locType = locType;
		}
		public int startPos { get; set;}
		public int length { get; set; }
		public string key { get; set; }
		public Color color { get; set; }
		public LocType locType { get; set; }
		public string taskPath;
	}
	public class ZStepLocs 
	{
		public ZStepLoc Find(int charPos)
		{
			foreach (ZStepLoc stepLoc in stepLocs.Values)
			{
				if ( charPos >= stepLoc.startPos && charPos < (stepLoc.startPos + stepLoc.length) )
					return stepLoc;
			}
			return null;
		}
		public ZStepLoc this[string key]
		{
			get
			{
				ZStepLoc stepLoc = null;
				if ( stepLocs.Keys.Contains(key) )
					stepLoc = stepLocs[key];
				return stepLoc;
			}
		}
		public string stepText { get; set; }
		private Dictionary<string,ZStepLoc>	stepLocs;
		public Dictionary<string,ZStepLoc>.ValueCollection Values
		{
			get
			{
				return stepLocs.Values;
			}
		}
		public Dictionary<string, ZStepLoc>.KeyCollection Keys
		{
			get
			{
				return stepLocs.Keys;
			}
		}
		public ZStepLocs(string stepText)
		{
			stepLocs = new Dictionary<string,ZStepLoc>();
			this.stepText = ParsedText(stepText);
			// if we didn't get anything, just put the whole thing in, probably an import
			if ( this.stepText.Length == 0)
				this.stepText = stepText;
		}
		static string stepTaskPattern		= @"(?'preText'[^\[]*)(\[\{(?'key'[^\}]*)\})(?'text'[^\]]*)\](?'postText'[^\[]*)";
		static string stepTagPattern		= @"(?'preText'[^<]*)(<\{(?'key'[^\}]*)\})(?'text'[^>]*)>(?'postText'[^<]*)";
		private string ParsedText(string text)
		{
			Color[] tagColors = { Color.Red, Color.Green, Color.DarkBlue, Color.Orange, Color.DarkViolet, Color.DarkOliveGreen, Color.DarkGoldenrod };
			ZStepLoc	stepLoc;
			string		results		= "";
			int			tagColorIdx = 0;
			//			int taskNum = 0;
			int			taskStart	= 0;
			int			taskLen		= 0;
			foreach (Match taskMatch in Regex.Matches(text, stepTaskPattern))
			{
				string taskKey		= taskMatch.Groups["key"].Value;
				string taskText		= taskMatch.Groups["text"].Value;
				string preTaskText	= taskMatch.Groups["preText"].Value;
				string postTaskText	= taskMatch.Groups["postText"].Value;
				Group taskGroup		= taskMatch.Groups["text"];
				results				+= preTaskText;
				taskStart			= results.Length;
				//				string text = String.Format("taskNum[{0}]: key='{1}', pos={2}", taskNum, taskKey, taskStart);
				//				txtOut.Text += Environment.NewLine + text;
				//				int tagNum		= 0;
				int tagStart	= 0;
				int tagLen		= 0;
				foreach (Match tagMatch in Regex.Matches(taskText, stepTagPattern))
				{
					string tagKey		= tagMatch.Groups["key"].Value;
					string tagValue		= tagMatch.Groups["text"].Value;
					string preTagText	= tagMatch.Groups["preText"].Value;
					string postTagText	= tagMatch.Groups["postText"].Value;
					results				+= preTagText;
					tagStart			= results.Length;
					tagLen				= tagValue.Length;
					results				+= tagValue + postTagText;
					//					text = String.Format("    tagNum[{0}]: key='{1}', value='{2}', pos={3}, len={4}", tagNum++, tagKey, tagValue, taskStart, tagLen);
					//					txtOut.Text += Environment.NewLine + text;
					stepLoc = new ZStepLoc(taskKey + "." + tagKey, tagStart, tagLen, ZStepLoc.LocType.tag);
					stepLoc.color = tagColors[tagColorIdx++];
					if (tagColorIdx >= tagColors.Length)
						tagColorIdx = 0;
					stepLocs.Add(stepLoc.key, stepLoc);
				}
				// if we didn't have any embedded tags, we have no result
				if ( results.Length == preTaskText.Length )
				{
					results += taskText;
				}
				taskLen		= results.Length - taskStart;
				taskText	= results.Substring(taskStart);
				stepLoc		= new ZStepLoc(taskKey, taskStart, taskLen, ZStepLoc.LocType.task);
				stepLocs.Add(taskKey, stepLoc);
				//				text = String.Format("taskNum[{0}]: key='{1}', pos={2}, len={3}{4}{5}", taskNum++, taskKey, taskStart, taskLen, Environment.NewLine, taskValue);
				//				txtOut.Text += Environment.NewLine + text;
				results		+= postTaskText;
			}
			return results;
		}

		internal class ExprTags
		{
			public const string taggedText				= "taggedText";
			public const string tagKey					= "tagKey";
		}
#if USE_THESE
		static string tagVarPattern		= @"(((?'Open'///\<{(?<" + ExprTags.tagKey + @">[\.A-Za-z0-9_ ]*)\})+((?'Close-Open'})[^{}]*)+)*(?(Open)(?!))$";

		static string tagBegPattern			= @"(///<\{(?<" + ExprTags.tagKey + @">[\.A-Za-z0-9_ ]*)\})(?<" + ExprTags.taggedText + @">[\.A-Za-z_0-9 ]*)";
		static string tagEndPattern			= @"(>///)";
		// 				"Select ///<{target_cell}E26>/// and type ///<{text_value}Highest Salary>///."
		// (?<!(Saturday|Sunday) )
		// [^<>]*(((?'Open'<)[^<>]*)+((?'Close-Open'>)[^<>]*)+)*(?(Open)(?!))
		static string tagPattern =
			@"((?'Open'///<)\{(?<" + ExprTags.tagKey + @">[\.A-Za-z0-9_ ]*)\})(?<" + ExprTags.taggedText + @">[\.A-Za-z_0-9 ]*)+((?'Close-Open'>///)(!(///<|>///))*)+)*(?(Open)(?!))$";
#endif
		//private void ResolveTags(string[] taggedStepTextLines)
		//{
		//	Color[] tagColors = { Color.Red, Color.Green, Color.DarkBlue, Color.Orange, Color.DarkViolet, Color.DarkOliveGreen, Color.DarkGoldenrod };
		//	string matchValue = "";
		//	string tagKey = "", taggedText = "";
		//	ZStepLoc stepLoc = null;
		//	stepText = "";
		//	int	prevMatchIndex = 0;
		//	int	tagColorIdx = 0;
		//	foreach (string taggedStepTextLine in taggedStepTextLines)
		//	{
		//		if ( stepText.Length > 0 )
		//			stepText += '\n';	// add the newline character to force line breaks
		//		MatchCollection matches = Regex.Matches(taggedStepTextLine, tagBegPattern+tagEndPattern, RegexOptions.IgnoreCase);
		//		foreach (Match match in matches)
		//		{
		//			matchValue = match.Value;
		//			int matchStart = match.Index;
		//			int matchLength = match.Length;
		//			if (matchStart > prevMatchIndex)
		//			{
		//				stepText += taggedStepTextLine.Substring(prevMatchIndex,matchStart - prevMatchIndex);
		//				prevMatchIndex = matchStart + matchLength;
		//			}
		//			if (match.Groups.Count > 0)
		//			{
		//				try
		//				{
		//					Group tagKeyGroup = match.Groups[ExprTags.tagKey];
		//					Group taggedTextGroup = match.Groups[ExprTags.taggedText];
		//					tagKey = tagKeyGroup.Value;
		//					taggedText = taggedTextGroup.Value;
		//					stepLoc = new ZStepLoc(tagKey, stepText.Length, taggedText.Length);
		//					stepLoc.color = tagColors[tagColorIdx++];
		//					if ( tagColorIdx >= tagColors.Length )
		//						tagColorIdx = 0;
		//					stepLocs.Add(tagKey,stepLoc);
		//					stepText += taggedText;
		//				}
		//				catch (Exception ex)
		//				{
		//					string msg = ex.Message;
		//				}
		//			}
		//		}
		//		if ( prevMatchIndex < taggedStepTextLine.Length )
		//			stepText += taggedStepTextLine.Substring(prevMatchIndex);
		//		prevMatchIndex = 0;
		//	}
		//}
	}
}
