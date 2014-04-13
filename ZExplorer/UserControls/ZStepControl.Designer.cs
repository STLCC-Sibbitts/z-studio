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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPts = new System.Windows.Forms.TextBox();
			this.scStep = new System.Windows.Forms.SplitContainer();
			this.txtStep = new System.Windows.Forms.RichTextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.scTask = new System.Windows.Forms.SplitContainer();
			this.dgTasks = new System.Windows.Forms.DataGridView();
			this.scTaskDetails = new System.Windows.Forms.SplitContainer();
			this.grpSource = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cboSourceLocationType = new System.Windows.Forms.ComboBox();
			this.txtSourceLocationAddress = new System.Windows.Forms.RichTextBox();
			this.lblSourceLocationType = new System.Windows.Forms.Label();
			this.txtSourceLocationContext = new System.Windows.Forms.RichTextBox();
			this.lblSourceLocationContext = new System.Windows.Forms.Label();
			this.grpTarget = new System.Windows.Forms.GroupBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.lblTargetProperty = new System.Windows.Forms.Label();
			this.cboTargetType = new System.Windows.Forms.ComboBox();
			this.lblTargetType = new System.Windows.Forms.Label();
			this.grpLocation = new System.Windows.Forms.GroupBox();
			this.cboTargetLocationType = new System.Windows.Forms.ComboBox();
			this.txtTargetLocationAddress = new System.Windows.Forms.RichTextBox();
			this.lblTargetLocationType = new System.Windows.Forms.Label();
			this.txtTargetLocationContext = new System.Windows.Forms.RichTextBox();
			this.lblTargetLocationContext = new System.Windows.Forms.Label();
			this.tabsTaskProps = new System.Windows.Forms.TabControl();
			this.tabContent = new System.Windows.Forms.TabPage();
			this.label5 = new System.Windows.Forms.Label();
			this.txtContentAltExpression = new System.Windows.Forms.RichTextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtContentExpression = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtAnswerValue = new System.Windows.Forms.RichTextBox();
			this.lblContentValue = new System.Windows.Forms.Label();
			this.tabFormat = new System.Windows.Forms.TabPage();
			this.cmnuTask = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmnuTaskUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.cmnuTaskAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.cmnuTarget = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mniSubmission = new System.Windows.Forms.ToolStripMenuItem();
			this.mniSubmissionSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.colText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colCategory = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.colDifficulty = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.colPts = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colAction = new System.Windows.Forms.DataGridViewComboBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.scStep)).BeginInit();
			this.scStep.Panel2.SuspendLayout();
			this.scStep.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scTask)).BeginInit();
			this.scTask.Panel1.SuspendLayout();
			this.scTask.Panel2.SuspendLayout();
			this.scTask.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgTasks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.scTaskDetails)).BeginInit();
			this.scTaskDetails.Panel1.SuspendLayout();
			this.scTaskDetails.Panel2.SuspendLayout();
			this.scTaskDetails.SuspendLayout();
			this.grpSource.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.grpTarget.SuspendLayout();
			this.grpLocation.SuspendLayout();
			this.tabsTaskProps.SuspendLayout();
			this.tabContent.SuspendLayout();
			this.cmnuTask.SuspendLayout();
			this.cmnuTarget.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(57, 7);
			this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(44, 22);
			this.txtName.TabIndex = 0;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Step:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(30, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Pts:";
			// 
			// txtPts
			// 
			this.txtPts.Location = new System.Drawing.Point(57, 36);
			this.txtPts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
			this.scStep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
			this.scStep.Size = new System.Drawing.Size(1000, 186);
			this.scStep.SplitterDistance = 120;
			this.scStep.TabIndex = 5;
			// 
			// txtStep
			// 
			this.txtStep.AcceptsTab = true;
			this.txtStep.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtStep.EnableAutoDragDrop = true;
			this.txtStep.Location = new System.Drawing.Point(0, 0);
			this.txtStep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtStep.Name = "txtStep";
			this.txtStep.Size = new System.Drawing.Size(876, 186);
			this.txtStep.TabIndex = 6;
			this.txtStep.Text = "";
			this.txtStep.TextChanged += new System.EventHandler(this.txtStep_TextChanged);
			this.txtStep.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtStep_MouseDoubleClick);
			this.txtStep.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtStep_MouseDown);
			this.txtStep.MouseLeave += new System.EventHandler(this.txtStep_MouseLeave);
			this.txtStep.MouseHover += new System.EventHandler(this.txtStep_MouseHover);
			this.txtStep.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtStep_MouseMove);
			this.txtStep.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtStep_MouseUp);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
			this.splitContainer1.Panel2.Controls.Add(this.scTask);
			this.splitContainer1.Panel2MinSize = 50;
			this.splitContainer1.Size = new System.Drawing.Size(1000, 690);
			this.splitContainer1.SplitterDistance = 186;
			this.splitContainer1.TabIndex = 6;
			// 
			// scTask
			// 
			this.scTask.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scTask.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.scTask.Location = new System.Drawing.Point(0, 0);
			this.scTask.Margin = new System.Windows.Forms.Padding(4);
			this.scTask.MinimumSize = new System.Drawing.Size(1000, 500);
			this.scTask.Name = "scTask";
			this.scTask.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scTask.Panel1
			// 
			this.scTask.Panel1.Controls.Add(this.dgTasks);
			this.scTask.Panel1MinSize = 135;
			// 
			// scTask.Panel2
			// 
			this.scTask.Panel2.Controls.Add(this.scTaskDetails);
			this.scTask.Panel2MinSize = 350;
			this.scTask.Size = new System.Drawing.Size(1000, 500);
			this.scTask.SplitterDistance = 138;
			this.scTask.SplitterWidth = 5;
			this.scTask.TabIndex = 1;
			// 
			// dgTasks
			// 
			this.dgTasks.AllowDrop = true;
			this.dgTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colText,
            this.colCategory,
            this.colDifficulty,
            this.colPts,
            this.colAction});
			this.dgTasks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgTasks.Location = new System.Drawing.Point(0, 0);
			this.dgTasks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgTasks.MinimumSize = new System.Drawing.Size(0, 70);
			this.dgTasks.Name = "dgTasks";
			this.dgTasks.RowTemplate.Height = 24;
			this.dgTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dgTasks.Size = new System.Drawing.Size(1000, 138);
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
			this.dgTasks.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgTasks_RowPostPaint);
			this.dgTasks.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgTasks_RowPrePaint);
			this.dgTasks.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgTasks_RowsAdded);
			this.dgTasks.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgTasks_RowStateChanged);
			this.dgTasks.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTasks_RowValidated);
			this.dgTasks.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgTasks_RowValidating);
			this.dgTasks.SelectionChanged += new System.EventHandler(this.dgTasks_SelectionChanged);
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
			// scTaskDetails
			// 
			this.scTaskDetails.CausesValidation = false;
			this.scTaskDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scTaskDetails.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.scTaskDetails.Location = new System.Drawing.Point(0, 0);
			this.scTaskDetails.Margin = new System.Windows.Forms.Padding(4);
			this.scTaskDetails.Name = "scTaskDetails";
			// 
			// scTaskDetails.Panel1
			// 
			this.scTaskDetails.Panel1.Controls.Add(this.grpSource);
			this.scTaskDetails.Panel1.Controls.Add(this.grpTarget);
			this.scTaskDetails.Panel1MinSize = 400;
			// 
			// scTaskDetails.Panel2
			// 
			this.scTaskDetails.Panel2.Controls.Add(this.tabsTaskProps);
			this.scTaskDetails.Panel2MinSize = 595;
			this.scTaskDetails.Size = new System.Drawing.Size(1000, 357);
			this.scTaskDetails.SplitterDistance = 400;
			this.scTaskDetails.SplitterWidth = 5;
			this.scTaskDetails.TabIndex = 1;
			// 
			// grpSource
			// 
			this.grpSource.Controls.Add(this.groupBox1);
			this.grpSource.Location = new System.Drawing.Point(8, 160);
			this.grpSource.Name = "grpSource";
			this.grpSource.Size = new System.Drawing.Size(408, 134);
			this.grpSource.TabIndex = 11;
			this.grpSource.TabStop = false;
			this.grpSource.Tag = "Source";
			this.grpSource.Text = "Source";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cboSourceLocationType);
			this.groupBox1.Controls.Add(this.txtSourceLocationAddress);
			this.groupBox1.Controls.Add(this.lblSourceLocationType);
			this.groupBox1.Controls.Add(this.txtSourceLocationContext);
			this.groupBox1.Controls.Add(this.lblSourceLocationContext);
			this.groupBox1.Location = new System.Drawing.Point(5, 29);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(391, 76);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Tag = "Location";
			this.groupBox1.Text = "Location";
			// 
			// cboSourceLocationType
			// 
			this.cboSourceLocationType.FormattingEnabled = true;
			this.cboSourceLocationType.Items.AddRange(new object[] {
            "",
            "Cell",
            "Range",
            "Sheet",
            "Workbook"});
			this.cboSourceLocationType.Location = new System.Drawing.Point(82, 21);
			this.cboSourceLocationType.Name = "cboSourceLocationType";
			this.cboSourceLocationType.Size = new System.Drawing.Size(85, 24);
			this.cboSourceLocationType.TabIndex = 1;
			this.cboSourceLocationType.Tag = "Type";
			// 
			// txtSourceLocationAddress
			// 
			this.txtSourceLocationAddress.EnableAutoDragDrop = true;
			this.txtSourceLocationAddress.Location = new System.Drawing.Point(173, 23);
			this.txtSourceLocationAddress.MaxLength = 64;
			this.txtSourceLocationAddress.Multiline = false;
			this.txtSourceLocationAddress.Name = "txtSourceLocationAddress";
			this.txtSourceLocationAddress.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.txtSourceLocationAddress.Size = new System.Drawing.Size(138, 20);
			this.txtSourceLocationAddress.TabIndex = 5;
			this.txtSourceLocationAddress.Tag = "Address";
			this.txtSourceLocationAddress.Text = "";
			// 
			// lblSourceLocationType
			// 
			this.lblSourceLocationType.AutoSize = true;
			this.lblSourceLocationType.Location = new System.Drawing.Point(16, 25);
			this.lblSourceLocationType.Name = "lblSourceLocationType";
			this.lblSourceLocationType.Size = new System.Drawing.Size(40, 16);
			this.lblSourceLocationType.TabIndex = 0;
			this.lblSourceLocationType.Text = "Type";
			// 
			// txtSourceLocationContext
			// 
			this.txtSourceLocationContext.EnableAutoDragDrop = true;
			this.txtSourceLocationContext.Location = new System.Drawing.Point(82, 50);
			this.txtSourceLocationContext.MaxLength = 64;
			this.txtSourceLocationContext.Multiline = false;
			this.txtSourceLocationContext.Name = "txtSourceLocationContext";
			this.txtSourceLocationContext.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.txtSourceLocationContext.Size = new System.Drawing.Size(232, 20);
			this.txtSourceLocationContext.TabIndex = 3;
			this.txtSourceLocationContext.Tag = "Context";
			this.txtSourceLocationContext.Text = "";
			// 
			// lblSourceLocationContext
			// 
			this.lblSourceLocationContext.AutoSize = true;
			this.lblSourceLocationContext.Location = new System.Drawing.Point(19, 47);
			this.lblSourceLocationContext.Name = "lblSourceLocationContext";
			this.lblSourceLocationContext.Size = new System.Drawing.Size(43, 16);
			this.lblSourceLocationContext.TabIndex = 2;
			this.lblSourceLocationContext.Text = "Sheet";
			// 
			// grpTarget
			// 
			this.grpTarget.Controls.Add(this.comboBox1);
			this.grpTarget.Controls.Add(this.lblTargetProperty);
			this.grpTarget.Controls.Add(this.cboTargetType);
			this.grpTarget.Controls.Add(this.lblTargetType);
			this.grpTarget.Controls.Add(this.grpLocation);
			this.grpTarget.Location = new System.Drawing.Point(8, 5);
			this.grpTarget.Name = "grpTarget";
			this.grpTarget.Size = new System.Drawing.Size(408, 140);
			this.grpTarget.TabIndex = 2;
			this.grpTarget.TabStop = false;
			this.grpTarget.Tag = "Target";
			this.grpTarget.Text = "Target";
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "",
            "Text",
            "Formula",
            "FormulaR1C1"});
			this.comboBox1.Location = new System.Drawing.Point(208, 25);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(168, 24);
			this.comboBox1.TabIndex = 10;
			this.comboBox1.Tag = "Property";
			// 
			// lblTargetProperty
			// 
			this.lblTargetProperty.AutoSize = true;
			this.lblTargetProperty.Location = new System.Drawing.Point(143, 29);
			this.lblTargetProperty.Name = "lblTargetProperty";
			this.lblTargetProperty.Size = new System.Drawing.Size(59, 16);
			this.lblTargetProperty.TabIndex = 9;
			this.lblTargetProperty.Text = "Property";
			// 
			// cboTargetType
			// 
			this.cboTargetType.FormattingEnabled = true;
			this.cboTargetType.Items.AddRange(new object[] {
            "",
            "Content",
            "Format",
            "Property"});
			this.cboTargetType.Location = new System.Drawing.Point(52, 25);
			this.cboTargetType.Name = "cboTargetType";
			this.cboTargetType.Size = new System.Drawing.Size(85, 24);
			this.cboTargetType.TabIndex = 8;
			this.cboTargetType.Tag = "Type";
			// 
			// lblTargetType
			// 
			this.lblTargetType.AutoSize = true;
			this.lblTargetType.Location = new System.Drawing.Point(6, 29);
			this.lblTargetType.Name = "lblTargetType";
			this.lblTargetType.Size = new System.Drawing.Size(40, 16);
			this.lblTargetType.TabIndex = 7;
			this.lblTargetType.Text = "Type";
			// 
			// grpLocation
			// 
			this.grpLocation.Controls.Add(this.cboTargetLocationType);
			this.grpLocation.Controls.Add(this.txtTargetLocationAddress);
			this.grpLocation.Controls.Add(this.lblTargetLocationType);
			this.grpLocation.Controls.Add(this.txtTargetLocationContext);
			this.grpLocation.Controls.Add(this.lblTargetLocationContext);
			this.grpLocation.Location = new System.Drawing.Point(0, 58);
			this.grpLocation.Name = "grpLocation";
			this.grpLocation.Size = new System.Drawing.Size(391, 76);
			this.grpLocation.TabIndex = 6;
			this.grpLocation.TabStop = false;
			this.grpLocation.Tag = "Location";
			this.grpLocation.Text = "Location";
			// 
			// cboTargetLocationType
			// 
			this.cboTargetLocationType.FormattingEnabled = true;
			this.cboTargetLocationType.Items.AddRange(new object[] {
            "",
            "Cell",
            "Range",
            "Sheet",
            "Workbook"});
			this.cboTargetLocationType.Location = new System.Drawing.Point(82, 21);
			this.cboTargetLocationType.Name = "cboTargetLocationType";
			this.cboTargetLocationType.Size = new System.Drawing.Size(85, 24);
			this.cboTargetLocationType.TabIndex = 1;
			this.cboTargetLocationType.Tag = "Type";
			// 
			// txtTargetLocationAddress
			// 
			this.txtTargetLocationAddress.EnableAutoDragDrop = true;
			this.txtTargetLocationAddress.Location = new System.Drawing.Point(173, 23);
			this.txtTargetLocationAddress.MaxLength = 64;
			this.txtTargetLocationAddress.Multiline = false;
			this.txtTargetLocationAddress.Name = "txtTargetLocationAddress";
			this.txtTargetLocationAddress.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.txtTargetLocationAddress.Size = new System.Drawing.Size(138, 20);
			this.txtTargetLocationAddress.TabIndex = 5;
			this.txtTargetLocationAddress.Tag = "Address";
			this.txtTargetLocationAddress.Text = "";
			this.txtTargetLocationAddress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtTargetLocationAddress_MouseDown);
			// 
			// lblTargetLocationType
			// 
			this.lblTargetLocationType.AutoSize = true;
			this.lblTargetLocationType.Location = new System.Drawing.Point(16, 25);
			this.lblTargetLocationType.Name = "lblTargetLocationType";
			this.lblTargetLocationType.Size = new System.Drawing.Size(40, 16);
			this.lblTargetLocationType.TabIndex = 0;
			this.lblTargetLocationType.Text = "Type";
			// 
			// txtTargetLocationContext
			// 
			this.txtTargetLocationContext.EnableAutoDragDrop = true;
			this.txtTargetLocationContext.Location = new System.Drawing.Point(82, 50);
			this.txtTargetLocationContext.MaxLength = 64;
			this.txtTargetLocationContext.Multiline = false;
			this.txtTargetLocationContext.Name = "txtTargetLocationContext";
			this.txtTargetLocationContext.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.txtTargetLocationContext.Size = new System.Drawing.Size(232, 20);
			this.txtTargetLocationContext.TabIndex = 3;
			this.txtTargetLocationContext.Tag = "Context";
			this.txtTargetLocationContext.Text = "";
			// 
			// lblTargetLocationContext
			// 
			this.lblTargetLocationContext.AutoSize = true;
			this.lblTargetLocationContext.Location = new System.Drawing.Point(19, 47);
			this.lblTargetLocationContext.Name = "lblTargetLocationContext";
			this.lblTargetLocationContext.Size = new System.Drawing.Size(43, 16);
			this.lblTargetLocationContext.TabIndex = 2;
			this.lblTargetLocationContext.Text = "Sheet";
			// 
			// tabsTaskProps
			// 
			this.tabsTaskProps.Controls.Add(this.tabContent);
			this.tabsTaskProps.Controls.Add(this.tabFormat);
			this.tabsTaskProps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabsTaskProps.Location = new System.Drawing.Point(0, 0);
			this.tabsTaskProps.Margin = new System.Windows.Forms.Padding(4);
			this.tabsTaskProps.Name = "tabsTaskProps";
			this.tabsTaskProps.SelectedIndex = 0;
			this.tabsTaskProps.Size = new System.Drawing.Size(596, 357);
			this.tabsTaskProps.TabIndex = 0;
			// 
			// tabContent
			// 
			this.tabContent.BackColor = System.Drawing.SystemColors.Control;
			this.tabContent.Controls.Add(this.label5);
			this.tabContent.Controls.Add(this.txtContentAltExpression);
			this.tabContent.Controls.Add(this.label4);
			this.tabContent.Controls.Add(this.txtContentExpression);
			this.tabContent.Controls.Add(this.label3);
			this.tabContent.Controls.Add(this.txtAnswerValue);
			this.tabContent.Controls.Add(this.lblContentValue);
			this.tabContent.Location = new System.Drawing.Point(4, 25);
			this.tabContent.Margin = new System.Windows.Forms.Padding(4);
			this.tabContent.Name = "tabContent";
			this.tabContent.Padding = new System.Windows.Forms.Padding(4);
			this.tabContent.Size = new System.Drawing.Size(588, 328);
			this.tabContent.TabIndex = 0;
			this.tabContent.Tag = "Answer";
			this.tabContent.Text = "Content";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(18, 217);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(509, 39);
			this.label5.TabIndex = 9;
			this.label5.Text = "add a scenario control to enable multiple scenarios or not, or other options, inc" +
    "luding task specific preferences";
			// 
			// txtContentAltExpression
			// 
			this.txtContentAltExpression.EnableAutoDragDrop = true;
			this.txtContentAltExpression.Location = new System.Drawing.Point(112, 99);
			this.txtContentAltExpression.MaxLength = 64;
			this.txtContentAltExpression.Name = "txtContentAltExpression";
			this.txtContentAltExpression.Size = new System.Drawing.Size(415, 37);
			this.txtContentAltExpression.TabIndex = 8;
			this.txtContentAltExpression.Tag = "AltTaggedExpression";
			this.txtContentAltExpression.Text = "";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 109);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Alt. Expression";
			// 
			// txtContentExpression
			// 
			this.txtContentExpression.EnableAutoDragDrop = true;
			this.txtContentExpression.Location = new System.Drawing.Point(111, 56);
			this.txtContentExpression.MaxLength = 64;
			this.txtContentExpression.Name = "txtContentExpression";
			this.txtContentExpression.Size = new System.Drawing.Size(415, 37);
			this.txtContentExpression.TabIndex = 6;
			this.txtContentExpression.Tag = "TaggedExpression";
			this.txtContentExpression.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Expression";
			// 
			// txtAnswerValue
			// 
			this.txtAnswerValue.EnableAutoDragDrop = true;
			this.txtAnswerValue.Location = new System.Drawing.Point(111, 13);
			this.txtAnswerValue.MaxLength = 512;
			this.txtAnswerValue.Name = "txtAnswerValue";
			this.txtAnswerValue.Size = new System.Drawing.Size(415, 37);
			this.txtAnswerValue.TabIndex = 4;
			this.txtAnswerValue.Tag = "Value";
			this.txtAnswerValue.Text = "";
			// 
			// lblContentValue
			// 
			this.lblContentValue.AutoSize = true;
			this.lblContentValue.Location = new System.Drawing.Point(11, 23);
			this.lblContentValue.Name = "lblContentValue";
			this.lblContentValue.Size = new System.Drawing.Size(43, 16);
			this.lblContentValue.TabIndex = 0;
			this.lblContentValue.Text = "Value";
			// 
			// tabFormat
			// 
			this.tabFormat.BackColor = System.Drawing.SystemColors.Control;
			this.tabFormat.Location = new System.Drawing.Point(4, 22);
			this.tabFormat.Margin = new System.Windows.Forms.Padding(4);
			this.tabFormat.Name = "tabFormat";
			this.tabFormat.Padding = new System.Windows.Forms.Padding(4);
			this.tabFormat.Size = new System.Drawing.Size(588, 331);
			this.tabFormat.TabIndex = 1;
			this.tabFormat.Text = "Format";
			// 
			// cmnuTask
			// 
			this.cmnuTask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuTaskUpdate,
            this.cmnuTaskAdd});
			this.cmnuTask.Name = "cmnuTask";
			this.cmnuTask.Size = new System.Drawing.Size(140, 48);
			// 
			// cmnuTaskUpdate
			// 
			this.cmnuTaskUpdate.Name = "cmnuTaskUpdate";
			this.cmnuTaskUpdate.Size = new System.Drawing.Size(139, 22);
			this.cmnuTaskUpdate.Text = "Update Task";
			this.cmnuTaskUpdate.Click += new System.EventHandler(this.cmnuTaskUpdate_Click);
			// 
			// cmnuTaskAdd
			// 
			this.cmnuTaskAdd.Name = "cmnuTaskAdd";
			this.cmnuTaskAdd.Size = new System.Drawing.Size(139, 22);
			this.cmnuTaskAdd.Text = "Add Task";
			this.cmnuTaskAdd.Click += new System.EventHandler(this.cmnuTaskAdd_Click);
			// 
			// cmnuTarget
			// 
			this.cmnuTarget.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniSubmission});
			this.cmnuTarget.Name = "cmnuTarget";
			this.cmnuTarget.Size = new System.Drawing.Size(136, 26);
			// 
			// mniSubmission
			// 
			this.mniSubmission.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniSubmissionSelect});
			this.mniSubmission.Name = "mniSubmission";
			this.mniSubmission.Size = new System.Drawing.Size(135, 22);
			this.mniSubmission.Text = "Submission";
			// 
			// mniSubmissionSelect
			// 
			this.mniSubmissionSelect.Name = "mniSubmissionSelect";
			this.mniSubmissionSelect.Size = new System.Drawing.Size(105, 22);
			this.mniSubmissionSelect.Text = "Select";
			this.mniSubmissionSelect.Click += new System.EventHandler(this.mniSubmissionSelect_Click);
			// 
			// colText
			// 
			this.colText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colText.DataPropertyName = "Text";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.colText.DefaultCellStyle = dataGridViewCellStyle6;
			this.colText.HeaderText = "Text";
			this.colText.MinimumWidth = 200;
			this.colText.Name = "colText";
			// 
			// colCategory
			// 
			this.colCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.colCategory.DataPropertyName = "category";
			dataGridViewCellStyle7.NullValue = "LO";
			this.colCategory.DefaultCellStyle = dataGridViewCellStyle7;
			this.colCategory.HeaderText = "Category";
			this.colCategory.Items.AddRange(new object[] {
            "",
            "NCE",
            "EE",
            "LO"});
			this.colCategory.MinimumWidth = 60;
			this.colCategory.Name = "colCategory";
			this.colCategory.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colCategory.Width = 69;
			// 
			// colDifficulty
			// 
			this.colDifficulty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colDifficulty.DataPropertyName = "difficulty";
			dataGridViewCellStyle8.NullValue = "Normal";
			this.colDifficulty.DefaultCellStyle = dataGridViewCellStyle8;
			this.colDifficulty.DividerWidth = 2;
			this.colDifficulty.HeaderText = "Difficulty";
			this.colDifficulty.Items.AddRange(new object[] {
            "",
            "Easy",
            "Normal",
            "Hard",
            "Challenging"});
			this.colDifficulty.MinimumWidth = 100;
			this.colDifficulty.Name = "colDifficulty";
			this.colDifficulty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colDifficulty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// colPts
			// 
			this.colPts.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colPts.DataPropertyName = "pts";
			dataGridViewCellStyle9.Format = "N2";
			dataGridViewCellStyle9.NullValue = "0";
			this.colPts.DefaultCellStyle = dataGridViewCellStyle9;
			this.colPts.HeaderText = "Pts";
			this.colPts.Name = "colPts";
			this.colPts.ReadOnly = true;
			this.colPts.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colPts.Width = 52;
			// 
			// colAction
			// 
			this.colAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colAction.DataPropertyName = "type";
			dataGridViewCellStyle10.NullValue = "Create";
			this.colAction.DefaultCellStyle = dataGridViewCellStyle10;
			this.colAction.HeaderText = "Action";
			this.colAction.Items.AddRange(new object[] {
            "Copy",
            "Create",
            "Cut",
            "Delete",
            "Modify"});
			this.colAction.MinimumWidth = 90;
			this.colAction.Name = "colAction";
			this.colAction.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colAction.Width = 90;
			// 
			// ZStepControl
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MinimumSize = new System.Drawing.Size(700, 180);
			this.Name = "ZStepControl";
			this.Size = new System.Drawing.Size(1000, 690);
			this.scStep.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scStep)).EndInit();
			this.scStep.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.scTask.Panel1.ResumeLayout(false);
			this.scTask.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scTask)).EndInit();
			this.scTask.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgTasks)).EndInit();
			this.scTaskDetails.Panel1.ResumeLayout(false);
			this.scTaskDetails.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scTaskDetails)).EndInit();
			this.scTaskDetails.ResumeLayout(false);
			this.grpSource.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.grpTarget.ResumeLayout(false);
			this.grpTarget.PerformLayout();
			this.grpLocation.ResumeLayout(false);
			this.grpLocation.PerformLayout();
			this.tabsTaskProps.ResumeLayout(false);
			this.tabContent.ResumeLayout(false);
			this.tabContent.PerformLayout();
			this.cmnuTask.ResumeLayout(false);
			this.cmnuTarget.ResumeLayout(false);
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
		private System.Windows.Forms.SplitContainer scTask;
		private System.Windows.Forms.SplitContainer scTaskDetails;
		private System.Windows.Forms.TabControl tabsTaskProps;
		private System.Windows.Forms.TabPage tabContent;
		private System.Windows.Forms.TabPage tabFormat;
		private System.Windows.Forms.GroupBox grpSource;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cboSourceLocationType;
		private System.Windows.Forms.RichTextBox txtSourceLocationAddress;
		private System.Windows.Forms.Label lblSourceLocationType;
		private System.Windows.Forms.RichTextBox txtSourceLocationContext;
		private System.Windows.Forms.Label lblSourceLocationContext;
		private System.Windows.Forms.GroupBox grpTarget;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label lblTargetProperty;
		private System.Windows.Forms.ComboBox cboTargetType;
		private System.Windows.Forms.Label lblTargetType;
		private System.Windows.Forms.GroupBox grpLocation;
		private System.Windows.Forms.ComboBox cboTargetLocationType;
		private System.Windows.Forms.RichTextBox txtTargetLocationAddress;
		private System.Windows.Forms.Label lblTargetLocationType;
		private System.Windows.Forms.RichTextBox txtTargetLocationContext;
		private System.Windows.Forms.Label lblTargetLocationContext;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.RichTextBox txtContentAltExpression;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RichTextBox txtContentExpression;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RichTextBox txtAnswerValue;
		private System.Windows.Forms.Label lblContentValue;
		private System.Windows.Forms.ContextMenuStrip cmnuTarget;
		private System.Windows.Forms.ToolStripMenuItem mniSubmission;
		private System.Windows.Forms.ToolStripMenuItem mniSubmissionSelect;
		private System.Windows.Forms.DataGridViewTextBoxColumn colText;
		private System.Windows.Forms.DataGridViewComboBoxColumn colCategory;
		private System.Windows.Forms.DataGridViewComboBoxColumn colDifficulty;
		private System.Windows.Forms.DataGridViewTextBoxColumn colPts;
		private System.Windows.Forms.DataGridViewComboBoxColumn colAction;
	}
}
