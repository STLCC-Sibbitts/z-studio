using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using ZLib.ZRubric;

namespace ZStudio
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//ZStepLoc sl = new ZStepLoc(5, 5);
			//string step = sl.ToString();
			//JObject job = (JObject)sl;
			//ZTask zTask = new ZTask(JObject.Parse(@"{""Name"":""test""}"));
			//zTask.text = "my test text";
			//zTask.stepLocation = sl;
			//sl.startPos = 14;
			//zTask.stepLocation = sl;
			//ZStepLoc s2 = (ZStepLoc)job;
			ZExplorer zExplorer = new ZExplorer();
			statusStrip = zExplorer.statusStrip;
			Application.Run(zExplorer);
		}
		static public StatusStrip statusStrip = null;
	}
}
