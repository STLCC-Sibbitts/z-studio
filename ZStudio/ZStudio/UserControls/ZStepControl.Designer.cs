namespace ZStudio.UserControls
{
	partial class ZStepControl
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPts = new System.Windows.Forms.TextBox();
			this.scStep = new System.Windows.Forms.SplitContainer();
			this.txtStep = new System.Windows.Forms.RichTextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.dgTasks = new System.Windows.Forms.DataGridView();
			this.cmnuTask = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmnuTaskUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.cmnuTaskAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colCategory = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.colWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colPts = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.colProperty = new System.Windows.Forms.DataGridViewComboBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.scStep)).BeginInit();
			this.scStep.Panel2.SuspendLayout();
			this.scStep.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgTasks)).BeginInit();
			this.cmnuTask.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(57, 7);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(44, 22);
			this.txtName.TabIndex = 0;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Step:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Pts:";
			// 
			// txtPts
			// 
			this.txtPts.Location = new System.Drawing.Point(57, 36);
			this.txtPts.Name = "txtPts";
			this.txtPts.Size = new System.Drawing.Size(44, 22);
			this.txtPts.TabIndex = 2;
			// 
			// scStep
			// 
			this.scStep.AllowDrop = true;
			this.scStep.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scStep.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.scStep.IsSplitterFixed = true;
			this.scStep.Location = new System.Drawing.Point(0, 0);
			this.scStep.MinimumSize = new System.Drawing.Size(0, 100);
			this.scStep.Name = "scStep";
			// 
			// scStep.Panel1
			// 
			this.scStep.Panel1.AllowDrop = true;
			this.scStep.Panel1.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.scStep_Panel1_QueryContinueDrag);
			this.scStep.Panel1MinSize = 100;
			// 
			// scStep.Panel2
			// 
			this.scStep.Panel2.Controls.Add(this.txtStep);
			this.scStep.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.scStep_Panel2_Paint);
			this.scStep.Panel2MinSize = 100;
			this.scStep.Size = new System.Drawing.Size(880, 105);
			this.scStep.SplitterDistance = 120;
			this.scStep.TabIndex = 5;
			// 
			// txtStep
			// 
			this.txtStep.AcceptsTab = true;
			this.txtStep.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtStep.EnableAutoDragDrop = true;
			this.txtStep.Location = new System.Drawing.Point(0, 0);
			this.txtStep.Name = "txtStep";
			this.txtStep.Size = new System.Drawing.Size(756, 105);
			this.txtStep.TabIndex = 6;
			this.txtStep.Text = "";
			this.txtStep.TextChanged += new System.EventHandler(this.txtStep_TextChanged);
			this.txtStep.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtStep_MouseDown);
			this.txtStep.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtStep_MouseUp);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.txtName);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.txtPts);
			this.splitContainer1.Panel1.Controls.Add(this.scStep);
			this.splitContainer1.Panel1MinSize = 100;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AllowDrop = true;
			this.splitContainer1.Panel2.AutoScroll = true;
			this.splitContainer1.Panel2.Controls.Add(this.dgTasks);
			this.splitContainer1.Panel2MinSize = 50;
			this.splitContainer1.Size = new System.Drawing.Size(880, 180);
			this.splitContainer1.SplitterDistance = 105;
			this.splitContainer1.TabIndex = 6;
			// 
			// dgTasks
			// 
			this.dgTasks.AllowDrop = true;
			this.dgTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colText,
            this.colCategory,
            this.colWeight,
            this.colPts,
            this.colType,
            this.colProperty});
			this.dgTasks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgTasks.Location = new System.Drawing.Point(0, 0);
			this.dgTasks.MinimumSize = new System.Drawing.Size(0, 70);
			this.dgTasks.Name = "dgTasks";
			this.dgTasks.RowTemplate.Height = 24;
			this.dgTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dgTasks.Size = new System.Drawing.Size(880, 71);
			this.dgTasks.TabIndex = 0;
			this.dgTasks.EditModeChanged += new System.EventHandler(this.dgTasks_EditModeChanged);
			this.dgTasks.CancelRowEdit += new System.Windows.Forms.QuestionEventHandler(this.dgTasks_CancelRowEdit);
			this.dgTasks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTasks_CellContentClick);
			this.dgTasks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTasks_CellEndEdit);
			this.dgTasks.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTasks_CellMouseEnter);
			this.dgTasks.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTasks_CellMouseLeave);
			this.dgTasks.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgTasks_CellMouseMove);
			this.dgTasks.NewRowNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgTasks_NewRowNeeded);
			this.dgTasks.RowDirtyStateNeeded += new System.Windows.Forms.QuestionEventHandler(this.dgTasks_RowDirtyStateNeeded);
			this.dgTasks.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgTasks_RowPrePaint);
			this.dgTasks.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgTasks_RowsAdded);
			this.dgTasks.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgTasks_RowStateChanged);
			this.dgTasks.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTasks_RowValidated);
			this.dgTasks.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgTasks_RowValidating);
			this.dgTasks.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgTasks_UserAddedRow);
			this.dgTasks.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgTasks_UserDeletingRow);
			this.dgTasks.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgTasks_DragDrop);
			this.dgTasks.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgTasks_DragEnter);
			this.dgTasks.DragOver += new System.Windows.Forms.DragEventHandler(this.dgTasks_DragOver);
			this.dgTasks.DragLeave += new System.EventHandler(this.dgTasks_DragLeave);
			this.dgTasks.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.dgTasks_QueryContinueDrag);
			this.dgTasks.MouseEnter += new System.EventHandler(this.dgTasks_MouseEnter);
			this.dgTasks.MouseHover += new System.EventHandler(this.dgTasks_MouseHover);
			// 
			// cmnuTask
			// 
			this.cmnuTask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuTaskUpdate,
            this.cmnuTaskAdd});
			this.cmnuTask.Name = "cmnuTask";
			this.cmnuTask.Size = new System.Drawing.Size(161, 52);
			// 
			// cmnuTaskUpdate
			// 
			this.cmnuTaskUpdate.Name = "cmnuTaskUpdate";
			this.cmnuTaskUpdate.Size = new System.Drawing.Size(160, 24);
			this.cmnuTaskUpdate.Text = "Update Task";
			this.cmnuTaskUpdate.Click += new System.EventHandler(this.cmnuTaskUpdate_Click);
			// 
			// cmnuTaskAdd
			// 
			this.cmnuTaskAdd.Name = "cmnuTaskAdd";
			this.cmnuTaskAdd.Size = new System.Drawing.Size(160, 24);
			this.cmnuTaskAdd.Text = "Add Task";
			this.cmnuTaskAdd.Click += new System.EventHandler(this.cmnuTaskAdd_Click);
			// 
			// colName
			// 
			this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.colName.DataPropertyName = "name";
			this.colName.Frozen = true;
			this.colName.HeaderText = "Task";
			this.colName.MinimumWidth = 64;
			this.colName.Name = "colName";
			this.colName.ReadOnly = true;
			this.colName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colName.Width = 64;
			// 
			// colText
			// 
			this.colText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colText.DataPropertyName = "Text";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.colText.DefaultCellStyle = dataGridViewCellStyle1;
			this.colText.HeaderText = "Text";
			this.colText.MinimumWidth = 200;
			this.colText.Name = "colText";
			// 
			// colCategory
			// 
			this.colCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.colCategory.DataPropertyName = "category";
			dataGridViewCellStyle2.NullValue = "Primary";
			this.colCategory.DefaultCellStyle = dataGridViewCellStyle2;
			this.colCategory.HeaderText = "Category";
			this.colCategory.Items.AddRange(new object[] {
            "Primary",
            "Accuracy",
            "Neatness"});
			this.colCategory.MinimumWidth = 100;
			this.colCategory.Name = "colCategory";
			this.colCategory.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// colWeight
			// 
			this.colWeight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colWeight.DataPropertyName = "weight";
			dataGridViewCellStyle3.Format = "N2";
			dataGridViewCellStyle3.NullValue = "1";
			this.colWeight.DefaultCellStyle = dataGridViewCellStyle3;
			this.colWeight.HeaderText = "Weight";
			this.colWeight.MaxInputLength = 6;
			this.colWeight.MinimumWidth = 55;
			this.colWeight.Name = "colWeight";
			this.colWeight.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colWeight.Width = 55;
			// 
			// colPts
			// 
			this.colPts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colPts.DataPropertyName = "pts";
			dataGridViewCellStyle4.Format = "N2";
			dataGridViewCellStyle4.NullValue = "0";
			this.colPts.DefaultCellStyle = dataGridViewCellStyle4;
			this.colPts.HeaderText = "Pts";
			this.colPts.Name = "colPts";
			this.colPts.ReadOnly = true;
			this.colPts.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colPts.Width = 53;
			// 
			// colType
			// 
			this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colType.DataPropertyName = "type";
			dataGridViewCellStyle5.NullValue = "Content";
			this.colType.DefaultCellStyle = dataGridViewCellStyle5;
			this.colType.HeaderText = "Type";
			this.colType.Items.AddRange(new object[] {
            "Content",
            "Format"});
			this.colType.MinimumWidth = 90;
			this.colType.Name = "colType";
			this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colType.Width = 90;
			// 
			// colProperty
			// 
			this.colProperty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle6.NullValue = "Value";
			this.colProperty.DefaultCellStyle = dataGridViewCellStyle6;
			this.colProperty.HeaderText = "Property";
			this.colProperty.Items.AddRange(new object[] {
            "Value",
            "Formula"});
			this.colProperty.MinimumWidth = 90;
			this.colProperty.Name = "colProperty";
			this.colProperty.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colProperty.Width = 90;
			// 
			// ZStepControl
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.splitContainer1);
			this.MinimumSize = new System.Drawing.Size(700, 180);
			this.Name = "ZStepControl";
			this.Size = new System.Drawing.Size(880, 180);
			this.scStep.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scStep)).EndInit();
			this.scStep.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgTasks)).EndInit();
			this.cmnuTask.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPts;
		private System.Windows.Forms.SplitContainer scStep;
		private System.Windows.Forms.RichTextBox txtStep;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridView dgTasks;
		private System.Windows.Forms.ContextMenuStrip cmnuTask;
		private System.Windows.Forms.ToolStripMenuItem cmnuTaskUpdate;
		private System.Windows.Forms.ToolStripMenuItem cmnuTaskAdd;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colText;
		private System.Windows.Forms.DataGridViewComboBoxColumn colCategory;
		private System.Windows.Forms.DataGridViewTextBoxColumn colWeight;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPts;
		private System.Windows.Forms.DataGridViewComboBoxColumn colType;
		private System.Windows.Forms.DataGridViewComboBoxColumn colProperty;
	}
}
