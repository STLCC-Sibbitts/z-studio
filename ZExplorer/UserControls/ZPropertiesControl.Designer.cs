namespace ZStudio.UserControls
{
	partial class ZPropertiesControl
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
			this.components = new System.ComponentModel.Container();
			this.dgNodeProperties = new System.Windows.Forms.DataGridView();
			this.dgColPropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgColPropertyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnProperties = new System.Windows.Forms.Button();
			this.cmnuStep = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmnuPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.cboPropertyValue = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dgNodeProperties)).BeginInit();
			this.cmnuStep.SuspendLayout();
			this.SuspendLayout();
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
			this.dgNodeProperties.Location = new System.Drawing.Point(0, 0);
			this.dgNodeProperties.MultiSelect = false;
			this.dgNodeProperties.Name = "dgNodeProperties";
			this.dgNodeProperties.RowHeadersVisible = false;
			this.dgNodeProperties.RowTemplate.Height = 24;
			this.dgNodeProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dgNodeProperties.Size = new System.Drawing.Size(321, 329);
			this.dgNodeProperties.TabIndex = 5;
			this.dgNodeProperties.CancelRowEdit += new System.Windows.Forms.QuestionEventHandler(this.dgNodeProperties_CancelRowEdit);
			this.dgNodeProperties.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgNodeProperties_CellBeginEdit);
			this.dgNodeProperties.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PropertySelected);
			this.dgNodeProperties.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PropertySelected);
			this.dgNodeProperties.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgNodeProperties_CellEndEdit);
			this.dgNodeProperties.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgNodeProperties_CellMouseClick);
			this.dgNodeProperties.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgNodeProperties_CellValidating);
			this.dgNodeProperties.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgNodeProperties_DragDrop);
			this.dgNodeProperties.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgNodeProperties_DragEnter);
			this.dgNodeProperties.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.dgNodeProperties_QueryContinueDrag);
			this.dgNodeProperties.Validating += new System.ComponentModel.CancelEventHandler(this.dgNodeProperties_Validating);
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
			// btnProperties
			// 
			this.btnProperties.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnProperties.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.btnProperties.Location = new System.Drawing.Point(0, 0);
			this.btnProperties.Margin = new System.Windows.Forms.Padding(0);
			this.btnProperties.Name = "btnProperties";
			this.btnProperties.Size = new System.Drawing.Size(26, 28);
			this.btnProperties.TabIndex = 6;
			this.btnProperties.Text = ". . .";
			this.btnProperties.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnProperties.UseMnemonic = false;
			this.btnProperties.UseVisualStyleBackColor = false;
			this.btnProperties.Visible = false;
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
			// cboPropertyValue
			// 
			this.cboPropertyValue.FormattingEnabled = true;
			this.cboPropertyValue.Location = new System.Drawing.Point(8, 8);
			this.cboPropertyValue.Name = "cboPropertyValue";
			this.cboPropertyValue.Size = new System.Drawing.Size(121, 24);
			this.cboPropertyValue.TabIndex = 7;
			this.cboPropertyValue.Visible = false;
			this.cboPropertyValue.SelectionChangeCommitted += new System.EventHandler(this.cboPropertyValue_SelectionChangeCommitted);
			// 
			// ZPropertiesControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboPropertyValue);
			this.Controls.Add(this.btnProperties);
			this.Controls.Add(this.dgNodeProperties);
			this.Name = "ZPropertiesControl";
			this.Size = new System.Drawing.Size(321, 329);
			((System.ComponentModel.ISupportInitialize)(this.dgNodeProperties)).EndInit();
			this.cmnuStep.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgNodeProperties;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgColPropertyName;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgColPropertyValue;
		private System.Windows.Forms.Button btnProperties;
		private System.Windows.Forms.ContextMenuStrip cmnuStep;
		private System.Windows.Forms.ToolStripMenuItem cmnuPaste;
		private System.Windows.Forms.ComboBox cboPropertyValue;

	}
}
