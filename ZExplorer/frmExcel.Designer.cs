namespace ZStudio
{
	partial class frmExcel
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
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.zExcelViewer = new ZStudio.UserControls.ZExcelViewer();
			this.SuspendLayout();
			// 
			// zExcelViewer
			// 
			this.zExcelViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.zExcelViewer.fileName = "";
			this.zExcelViewer.Location = new System.Drawing.Point(0, 0);
			this.zExcelViewer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.zExcelViewer.Name = "zExcelViewer";
			this.zExcelViewer.Size = new System.Drawing.Size(778, 529);
			this.zExcelViewer.TabIndex = 0;
			// 
			// frmExcel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 529);
			this.Controls.Add(this.zExcelViewer);
			this.Name = "frmExcel";
			this.Text = "frmExcel";
			this.ResumeLayout(false);

		}

		#endregion

		private UserControls.ZExcelViewer zExcelViewer;
	}
}