namespace JsonExplorer
{
	partial class FrmProps
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
			this.components = new System.ComponentModel.Container();
			this.scPanel = new System.Windows.Forms.Panel();
			this.tabNodeProperties = new System.Windows.Forms.TabControl();
			this.tpProperties = new System.Windows.Forms.TabPage();
			this.btnProperties = new System.Windows.Forms.Button();
			this.dgNodeProperties = new System.Windows.Forms.DataGridView();
			this.dgColPropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgColPropertyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cmnuStep = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmnuPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.scPanel.SuspendLayout();
			this.tabNodeProperties.SuspendLayout();
			this.tpProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgNodeProperties)).BeginInit();
			this.cmnuStep.SuspendLayout();
			this.SuspendLayout();
			// 
			// scPanel
			// 
			this.scPanel.AllowDrop = true;
			this.scPanel.AutoSize = true;
			this.scPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.scPanel.Controls.Add(this.tabNodeProperties);
			this.scPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scPanel.Location = new System.Drawing.Point(0, 0);
			this.scPanel.Name = "scPanel";
			this.scPanel.Size = new System.Drawing.Size(408, 506);
			this.scPanel.TabIndex = 6;
			this.scPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgNodeProperties_DragDrop);
			// 
			// tabNodeProperties
			// 
			this.tabNodeProperties.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabNodeProperties.AllowDrop = true;
			this.tabNodeProperties.Controls.Add(this.tpProperties);
			this.tabNodeProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabNodeProperties.Location = new System.Drawing.Point(0, 0);
			this.tabNodeProperties.Name = "tabNodeProperties";
			this.tabNodeProperties.SelectedIndex = 0;
			this.tabNodeProperties.Size = new System.Drawing.Size(408, 506);
			this.tabNodeProperties.TabIndex = 2;
			// 
			// tpProperties
			// 
			this.tpProperties.AllowDrop = true;
			this.tpProperties.AutoScroll = true;
			this.tpProperties.Controls.Add(this.btnProperties);
			this.tpProperties.Controls.Add(this.dgNodeProperties);
			this.tpProperties.Location = new System.Drawing.Point(4, 4);
			this.tpProperties.Name = "tpProperties";
			this.tpProperties.Padding = new System.Windows.Forms.Padding(3);
			this.tpProperties.Size = new System.Drawing.Size(400, 477);
			this.tpProperties.TabIndex = 0;
			this.tpProperties.Text = "Properties";
			this.tpProperties.UseVisualStyleBackColor = true;
			// 
			// btnProperties
			// 
			this.btnProperties.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnProperties.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.btnProperties.Location = new System.Drawing.Point(0, 0);
			this.btnProperties.Margin = new System.Windows.Forms.Padding(0);
			this.btnProperties.Name = "btnProperties";
			this.btnProperties.Size = new System.Drawing.Size(26, 28);
			this.btnProperties.TabIndex = 5;
			this.btnProperties.Text = ". . .";
			this.btnProperties.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnProperties.UseMnemonic = false;
			this.btnProperties.UseVisualStyleBackColor = false;
			this.btnProperties.Visible = false;
			this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
			// 
			// dgNodeProperties
			// 
			this.dgNodeProperties.AllowDrop = true;
			this.dgNodeProperties.AllowUserToAddRows = false;
			this.dgNodeProperties.AllowUserToDeleteRows = false;
			this.dgNodeProperties.AllowUserToResizeRows = false;
			this.dgNodeProperties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgNodeProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgNodeProperties.ColumnHeadersVisible = false;
			this.dgNodeProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgColPropertyName,
            this.dgColPropertyValue});
			this.dgNodeProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgNodeProperties.Location = new System.Drawing.Point(3, 3);
			this.dgNodeProperties.MultiSelect = false;
			this.dgNodeProperties.Name = "dgNodeProperties";
			this.dgNodeProperties.RowHeadersVisible = false;
			this.dgNodeProperties.RowTemplate.Height = 24;
			this.dgNodeProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dgNodeProperties.Size = new System.Drawing.Size(394, 471);
			this.dgNodeProperties.TabIndex = 4;
			// 
			// dgColPropertyName
			// 
			this.dgColPropertyName.HeaderText = "Name";
			this.dgColPropertyName.Name = "dgColPropertyName";
			// 
			// dgColPropertyValue
			// 
			this.dgColPropertyValue.HeaderText = "Value";
			this.dgColPropertyValue.Name = "dgColPropertyValue";
			this.dgColPropertyValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// cmnuStep
			// 
			this.cmnuStep.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuPaste});
			this.cmnuStep.Name = "cmnuStep";
			this.cmnuStep.Size = new System.Drawing.Size(165, 28);
			// 
			// cmnuPaste
			// 
			this.cmnuPaste.Name = "cmnuPaste";
			this.cmnuPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.cmnuPaste.Size = new System.Drawing.Size(164, 24);
			this.cmnuPaste.Text = "Paste";
			this.cmnuPaste.Click += new System.EventHandler(this.cmnuPaste_Click);
			// 
			// FrmProps
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.scPanel);
			this.Name = "FrmProps";
			this.Size = new System.Drawing.Size(408, 506);
			this.scPanel.ResumeLayout(false);
			this.tabNodeProperties.ResumeLayout(false);
			this.tpProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgNodeProperties)).EndInit();
			this.cmnuStep.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel scPanel;
		private System.Windows.Forms.ContextMenuStrip cmnuStep;
		private System.Windows.Forms.ToolStripMenuItem cmnuPaste;
		private System.Windows.Forms.TabControl tabNodeProperties;
		private System.Windows.Forms.TabPage tpProperties;
		private System.Windows.Forms.Button btnProperties;
		private System.Windows.Forms.DataGridView dgNodeProperties;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgColPropertyName;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgColPropertyValue;
	}
}

