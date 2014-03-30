namespace ZStudio.UserControls
{
	partial class TVRubric
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tvJson = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// tvJson
			// 
			this.tvJson.AllowDrop = true;
			this.tvJson.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvJson.LabelEdit = true;
			this.tvJson.Location = new System.Drawing.Point(0, 0);
			this.tvJson.Name = "tvJson";
			this.tvJson.Size = new System.Drawing.Size(310, 351);
			this.tvJson.TabIndex = 1;
			// 
			// tvRubric
			// 
			this.Controls.Add(this.tvJson);
			this.Name = "tvRubric";
			this.Size = new System.Drawing.Size(310, 351);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tvJson;
	}
}
