using Excel = Microsoft.Office.Interop.Excel;

namespace ZGUI
{
	partial class ZExcelViewer
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
				if (m_oExcelApp != null)
					if (m_oExcelApp.ActiveWorkbook != null)
						m_oExcelApp.ActiveWorkbook.Close(Excel.XlSaveAction.xlDoNotSaveChanges);
				m_oExcelApp = null;

			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.excelBrowswer = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// excelBrowswer
			// 
			this.excelBrowswer.AllowNavigation = false;
			this.excelBrowswer.CausesValidation = false;
			this.excelBrowswer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.excelBrowswer.Location = new System.Drawing.Point(0, 0);
			this.excelBrowswer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.excelBrowswer.MinimumSize = new System.Drawing.Size(15, 16);
			this.excelBrowswer.Name = "excelBrowswer";
			this.excelBrowswer.Size = new System.Drawing.Size(712, 342);
			this.excelBrowswer.TabIndex = 1;
			this.excelBrowswer.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.excelBrowswer_DocumentCompleted);
			this.excelBrowswer.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.excelBrowswer_Navigated);
			// 
			// ZExcelViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.excelBrowswer);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "ZExcelViewer";
			this.Size = new System.Drawing.Size(712, 342);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser excelBrowswer;
	}
}
