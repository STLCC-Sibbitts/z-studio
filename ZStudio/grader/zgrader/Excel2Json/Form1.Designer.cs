namespace Excel2Json
{
	partial class Form1
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
			this.button1 = new System.Windows.Forms.Button();
			this.txtJson = new System.Windows.Forms.RichTextBox();
			this.btnDumpIt = new System.Windows.Forms.Button();
			this.txtZpath = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(506, 486);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(164, 74);
			this.button1.TabIndex = 0;
			this.button1.Text = "DoIt";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtJson
			// 
			this.txtJson.Location = new System.Drawing.Point(27, 74);
			this.txtJson.Name = "txtJson";
			this.txtJson.Size = new System.Drawing.Size(717, 406);
			this.txtJson.TabIndex = 1;
			this.txtJson.Text = "";
			this.txtJson.WordWrap = false;
			// 
			// btnDumpIt
			// 
			this.btnDumpIt.Location = new System.Drawing.Point(234, 486);
			this.btnDumpIt.Name = "btnDumpIt";
			this.btnDumpIt.Size = new System.Drawing.Size(164, 74);
			this.btnDumpIt.TabIndex = 2;
			this.btnDumpIt.Text = "Dump It";
			this.btnDumpIt.UseVisualStyleBackColor = true;
			this.btnDumpIt.Click += new System.EventHandler(this.btnDumpIt_Click);
			// 
			// txtZpath
			// 
			this.txtZpath.Location = new System.Drawing.Point(129, 25);
			this.txtZpath.Name = "txtZpath";
			this.txtZpath.Size = new System.Drawing.Size(586, 29);
			this.txtZpath.TabIndex = 3;
			this.txtZpath.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(27, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 20);
			this.label1.TabIndex = 4;
			this.label1.Text = "zPath";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(785, 572);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtZpath);
			this.Controls.Add(this.btnDumpIt);
			this.Controls.Add(this.txtJson);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox txtJson;
		private System.Windows.Forms.Button btnDumpIt;
		private System.Windows.Forms.RichTextBox txtZpath;
		private System.Windows.Forms.Label label1;
	}
}

