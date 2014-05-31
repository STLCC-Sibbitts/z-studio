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


namespace ZLib.ZRubric
{
	public class ZTaskLoc
	{
		public enum LocType
		{
			value,
			tag
		}
		public ZTaskLoc(string key, int startPos, int length, LocType locType)
		{
			this.startPos = startPos;
			this.length = length;
			this.key = key;
			this.locType = locType;
		}
		public ZTaskLoc(ZStepLocs stepLocs, string key, int startPos, int length, LocType locType)
		{
			this.startPos = startPos;
			this.length = length;
			this.key = key;
			this.locType = locType;
			this.stepLocs = stepLocs;
		}
		public ZStepLocs	stepLocs { get; set; }
		public int			startPos { get; set; }
		public int			length { get; set; }
		public string		key { get; set; }
		public Color		color
		{
			get
			{
				ZStepLoc	stepLoc = stepLocs[key];
				Color		value	= stepLoc.color;
				return value;
			}
		}
		public LocType		locType { get; set; }
	}
	public class ZTaskLocs
	{
		private List<ZTaskLoc> m_locs;
		public string text { get; set; }
		public List<ZTaskLoc> locs  
		{
			get
			{
				return m_locs;
			}
		}
		public List<ZTaskLoc> Values
		{
			get
			{
				return m_locs;
			}
		}
		public ZTaskLocs(ZStepLocs stepLocs, ZTask task)
		{
			m_locs = new List<ZTaskLoc>();
			this.text = ParsedText(stepLocs, task.taggedText);
		}
		public ZTaskLocs(ZStepLocs stepLocs, string taskText)
		{
			m_locs = new List<ZTaskLoc>();
			this.text = ParsedText(stepLocs, taskText);
		}
		static string tagPattern		= @"(?'preText'[^<]*)(<\{(?'taskKey'[^\},]*)(,(?'tagKey'[^\}]*))?\})(?'text'[^>]*)>(?'postText'[^<]*)";
		static public string StrippedText(string taskText)
		{
			if ( !taskText.Contains("<{") )
				return taskText;
			string results		= "";
			string text			= "";
			int tagStart	= 0;
			int tagLen		= 0;
			foreach (Match tagMatch in Regex.Matches(taskText, tagPattern))
			{
				string taskKey		= tagMatch.Groups["taskKey"].Value;
				string tagKey		= tagMatch.Groups["tagKey"].Value;
				text				= tagMatch.Groups["text"].Value;
				string preTagText	= tagMatch.Groups["preText"].Value;
				string postTagText	= tagMatch.Groups["postText"].Value;
				results				+= preTagText;
				tagStart			= results.Length;
				tagLen				= text.Length;
				results				+= text + postTagText;
			}
			if ( results.Length == 0 && taskText.Length > 0 )
				results = taskText;
			return results;
		}
		private string ParsedText(ZStepLocs stepLocs, string taskText)
		{
			string results		= "";
			string text			= "";
			int tagStart	= 0;
			int tagLen		= 0;
			foreach (Match tagMatch in Regex.Matches(taskText, tagPattern))
			{
				string taskKey		= tagMatch.Groups["taskKey"].Value;
				string tagKey		= tagMatch.Groups["tagKey"].Value;
				text				= tagMatch.Groups["text"].Value;
				string preTagText	= tagMatch.Groups["preText"].Value;
				string postTagText	= tagMatch.Groups["postText"].Value;
				results				+= preTagText;
				tagStart			= results.Length;
				tagLen				= text.Length;
				results				+= text + postTagText;
				ZTaskLoc taskLoc	= new ZTaskLoc(stepLocs, taskKey + "." + tagKey, tagStart, tagLen, ZTaskLoc.LocType.tag);
				m_locs.Add(taskLoc);
			}
			if ( m_locs.Count == 1 && text == results )
				m_locs[0].locType = ZTaskLoc.LocType.value;

			return results;
		}
	}

}
