using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZStudio.UserControls;

namespace ZStudio
{
	public partial class frmExcel : Form
	{
		public frmExcel()
		{
			InitializeComponent();
		}
		public void Open(string filename, bool submission = true)
		{
			zExcelViewer.fileName = filename;
			string fileType = "SUBMISSION";
			if ( ! submission )
				fileType = "KEY";
			Text = "z-studio [" + fileType + "]: " + filename;
		}
		public ZExcelViewer viewer
		{
			get { return zExcelViewer; }
		}
	}
}
