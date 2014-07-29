namespace ZGUI.UserControls
{
	partial class frmZTaskAdd
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
			this.txtTask = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.grpTarget = new System.Windows.Forms.GroupBox();
			this.cboProperty = new System.Windows.Forms.ComboBox();
			this.lblTargetProperty = new System.Windows.Forms.Label();
			this.cboTargetType = new System.Windows.Forms.ComboBox();
			this.lblTargetType = new System.Windows.Forms.Label();
			this.grpLocation = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cboTargetLocationType = new System.Windows.Forms.ComboBox();
			this.txtTargetLocationAddress = new System.Windows.Forms.RichTextBox();
			this.lblTargetLocationType = new System.Windows.Forms.Label();
			this.txtTargetLocationContext = new System.Windows.Forms.RichTextBox();
			this.lblTargetLocationContext = new System.Windows.Forms.Label();
			this.cmdAddTask = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.txtContentExpression = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtAnswerValue = new System.Windows.Forms.RichTextBox();
			this.lblContentValue = new System.Windows.Forms.Label();
			this.cmdSynchTask = new System.Windows.Forms.Button();
			this.txtStep = new System.Windows.Forms.RichTextBox();
			this.cmdRefreshJson = new System.Windows.Forms.Button();
			this.txtJson = new System.Windows.Forms.RichTextBox();
			this.grpTarget.SuspendLayout();
			this.grpLocation.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtTask
			// 
			this.txtTask.EnableAutoDragDrop = true;
			this.txtTask.Location = new System.Drawing.Point(104, 34);
			this.txtTask.MaxLength = 512;
			this.txtTask.Name = "txtTask";
			this.txtTask.Size = new System.Drawing.Size(340, 100);
			this.txtTask.TabIndex = 0;
			this.txtTask.Tag = "Value";
			this.txtTask.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(40, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Task";
			// 
			// grpTarget
			// 
			this.grpTarget.Controls.Add(this.cboProperty);
			this.grpTarget.Controls.Add(this.lblTargetProperty);
			this.grpTarget.Controls.Add(this.cboTargetType);
			this.grpTarget.Controls.Add(this.lblTargetType);
			this.grpTarget.Controls.Add(this.grpLocation);
			this.grpTarget.Location = new System.Drawing.Point(36, 140);
			this.grpTarget.Name = "grpTarget";
			this.grpTarget.Size = new System.Drawing.Size(408, 140);
			this.grpTarget.TabIndex = 1;
			this.grpTarget.TabStop = false;
			this.grpTarget.Tag = "Target";
			this.grpTarget.Text = "Target";
			// 
			// cboProperty
			// 
			this.cboProperty.FormattingEnabled = true;
			this.cboProperty.Items.AddRange(new object[] {
            "",
            "Text",
            "Formula",
            "FormulaR1C1"});
			this.cboProperty.Location = new System.Drawing.Point(222, 25);
			this.cboProperty.Name = "cboProperty";
			this.cboProperty.Size = new System.Drawing.Size(168, 21);
			this.cboProperty.TabIndex = 1;
			this.cboProperty.Tag = "Property";
			this.cboProperty.SelectionChangeCommitted += new System.EventHandler(this.cboTarget_SelectionChangeCommitted);
			// 
			// lblTargetProperty
			// 
			this.lblTargetProperty.AutoSize = true;
			this.lblTargetProperty.Location = new System.Drawing.Point(159, 29);
			this.lblTargetProperty.Name = "lblTargetProperty";
			this.lblTargetProperty.Size = new System.Drawing.Size(46, 13);
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
			this.cboTargetType.Location = new System.Drawing.Point(68, 25);
			this.cboTargetType.Name = "cboTargetType";
			this.cboTargetType.Size = new System.Drawing.Size(85, 21);
			this.cboTargetType.TabIndex = 0;
			this.cboTargetType.Tag = "Type";
			this.cboTargetType.SelectionChangeCommitted += new System.EventHandler(this.cboTarget_SelectionChangeCommitted);
			// 
			// lblTargetType
			// 
			this.lblTargetType.AutoSize = true;
			this.lblTargetType.Location = new System.Drawing.Point(18, 28);
			this.lblTargetType.Name = "lblTargetType";
			this.lblTargetType.Size = new System.Drawing.Size(31, 13);
			this.lblTargetType.TabIndex = 7;
			this.lblTargetType.Text = "Type";
			// 
			// grpLocation
			// 
			this.grpLocation.Controls.Add(this.label6);
			this.grpLocation.Controls.Add(this.cboTargetLocationType);
			this.grpLocation.Controls.Add(this.txtTargetLocationAddress);
			this.grpLocation.Controls.Add(this.lblTargetLocationType);
			this.grpLocation.Controls.Add(this.txtTargetLocationContext);
			this.grpLocation.Controls.Add(this.lblTargetLocationContext);
			this.grpLocation.Location = new System.Drawing.Point(0, 58);
			this.grpLocation.Name = "grpLocation";
			this.grpLocation.Size = new System.Drawing.Size(391, 76);
			this.grpLocation.TabIndex = 2;
			this.grpLocation.TabStop = false;
			this.grpLocation.Tag = "Location";
			this.grpLocation.Text = "Location";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(159, 23);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Address";
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
			this.cboTargetLocationType.Location = new System.Drawing.Point(68, 21);
			this.cboTargetLocationType.Name = "cboTargetLocationType";
			this.cboTargetLocationType.Size = new System.Drawing.Size(85, 21);
			this.cboTargetLocationType.TabIndex = 0;
			this.cboTargetLocationType.Tag = "Type";
			this.cboTargetLocationType.SelectionChangeCommitted += new System.EventHandler(this.cboTarget_SelectionChangeCommitted);
			this.cboTargetLocationType.SelectedValueChanged += new System.EventHandler(this.cboTarget_SelectionChangeCommitted);
			// 
			// txtTargetLocationAddress
			// 
			this.txtTargetLocationAddress.EnableAutoDragDrop = true;
			this.txtTargetLocationAddress.Location = new System.Drawing.Point(222, 23);
			this.txtTargetLocationAddress.MaxLength = 64;
			this.txtTargetLocationAddress.Multiline = false;
			this.txtTargetLocationAddress.Name = "txtTargetLocationAddress";
			this.txtTargetLocationAddress.ReadOnly = true;
			this.txtTargetLocationAddress.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.txtTargetLocationAddress.Size = new System.Drawing.Size(138, 20);
			this.txtTargetLocationAddress.TabIndex = 1;
			this.txtTargetLocationAddress.TabStop = false;
			this.txtTargetLocationAddress.Tag = "Address";
			this.txtTargetLocationAddress.Text = "";
			// 
			// lblTargetLocationType
			// 
			this.lblTargetLocationType.AutoSize = true;
			this.lblTargetLocationType.Location = new System.Drawing.Point(18, 25);
			this.lblTargetLocationType.Name = "lblTargetLocationType";
			this.lblTargetLocationType.Size = new System.Drawing.Size(31, 13);
			this.lblTargetLocationType.TabIndex = 0;
			this.lblTargetLocationType.Text = "Type";
			// 
			// txtTargetLocationContext
			// 
			this.txtTargetLocationContext.EnableAutoDragDrop = true;
			this.txtTargetLocationContext.Location = new System.Drawing.Point(68, 50);
			this.txtTargetLocationContext.MaxLength = 64;
			this.txtTargetLocationContext.Multiline = false;
			this.txtTargetLocationContext.Name = "txtTargetLocationContext";
			this.txtTargetLocationContext.ReadOnly = true;
			this.txtTargetLocationContext.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.txtTargetLocationContext.Size = new System.Drawing.Size(232, 20);
			this.txtTargetLocationContext.TabIndex = 2;
			this.txtTargetLocationContext.TabStop = false;
			this.txtTargetLocationContext.Tag = "Context";
			this.txtTargetLocationContext.Text = "";
			// 
			// lblTargetLocationContext
			// 
			this.lblTargetLocationContext.AutoSize = true;
			this.lblTargetLocationContext.Location = new System.Drawing.Point(18, 47);
			this.lblTargetLocationContext.Name = "lblTargetLocationContext";
			this.lblTargetLocationContext.Size = new System.Drawing.Size(35, 13);
			this.lblTargetLocationContext.TabIndex = 2;
			this.lblTargetLocationContext.Text = "Sheet";
			// 
			// cmdAddTask
			// 
			this.cmdAddTask.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdAddTask.Location = new System.Drawing.Point(198, 286);
			this.cmdAddTask.Name = "cmdAddTask";
			this.cmdAddTask.Size = new System.Drawing.Size(75, 23);
			this.cmdAddTask.TabIndex = 2;
			this.cmdAddTask.Text = "Add Task";
			this.cmdAddTask.UseVisualStyleBackColor = true;
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(288, 286);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// txtContentExpression
			// 
			this.txtContentExpression.EnableAutoDragDrop = true;
			this.txtContentExpression.Location = new System.Drawing.Point(583, 249);
			this.txtContentExpression.MaxLength = 64;
			this.txtContentExpression.Name = "txtContentExpression";
			this.txtContentExpression.ReadOnly = true;
			this.txtContentExpression.Size = new System.Drawing.Size(415, 37);
			this.txtContentExpression.TabIndex = 12;
			this.txtContentExpression.Tag = "TaggedExpression";
			this.txtContentExpression.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(483, 259);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Expression";
			// 
			// txtAnswerValue
			// 
			this.txtAnswerValue.EnableAutoDragDrop = true;
			this.txtAnswerValue.Location = new System.Drawing.Point(583, 206);
			this.txtAnswerValue.MaxLength = 512;
			this.txtAnswerValue.Name = "txtAnswerValue";
			this.txtAnswerValue.ReadOnly = true;
			this.txtAnswerValue.Size = new System.Drawing.Size(415, 37);
			this.txtAnswerValue.TabIndex = 10;
			this.txtAnswerValue.Tag = "Value";
			this.txtAnswerValue.Text = "";
			// 
			// lblContentValue
			// 
			this.lblContentValue.AutoSize = true;
			this.lblContentValue.Location = new System.Drawing.Point(483, 216);
			this.lblContentValue.Name = "lblContentValue";
			this.lblContentValue.Size = new System.Drawing.Size(34, 13);
			this.lblContentValue.TabIndex = 9;
			this.lblContentValue.Text = "Value";
			// 
			// cmdSynchTask
			// 
			this.cmdSynchTask.CausesValidation = false;
			this.cmdSynchTask.Location = new System.Drawing.Point(104, 286);
			this.cmdSynchTask.Name = "cmdSynchTask";
			this.cmdSynchTask.Size = new System.Drawing.Size(75, 23);
			this.cmdSynchTask.TabIndex = 13;
			this.cmdSynchTask.Text = "Synch Task";
			this.cmdSynchTask.UseVisualStyleBackColor = true;
			this.cmdSynchTask.Click += new System.EventHandler(this.cmdSynchTask_Click);
			// 
			// txtStep
			// 
			this.txtStep.AcceptsTab = true;
			this.txtStep.EnableAutoDragDrop = true;
			this.txtStep.Location = new System.Drawing.Point(467, 7);
			this.txtStep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtStep.Name = "txtStep";
			this.txtStep.Size = new System.Drawing.Size(567, 186);
			this.txtStep.TabIndex = 14;
			this.txtStep.Text = "";
			// 
			// cmdRefreshJson
			// 
			this.cmdRefreshJson.Location = new System.Drawing.Point(486, 285);
			this.cmdRefreshJson.Name = "cmdRefreshJson";
			this.cmdRefreshJson.Size = new System.Drawing.Size(75, 23);
			this.cmdRefreshJson.TabIndex = 15;
			this.cmdRefreshJson.Text = "Refresh json";
			this.cmdRefreshJson.UseVisualStyleBackColor = true;
			this.cmdRefreshJson.Click += new System.EventHandler(this.cmdRefreshJson_Click);
			// 
			// txtJson
			// 
			this.txtJson.AcceptsTab = true;
			this.txtJson.EnableAutoDragDrop = true;
			this.txtJson.Location = new System.Drawing.Point(43, 341);
			this.txtJson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtJson.Name = "txtJson";
			this.txtJson.Size = new System.Drawing.Size(991, 375);
			this.txtJson.TabIndex = 16;
			this.txtJson.Text = "";
			// 
			// frmZTaskAdd
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1046, 742);
			this.Controls.Add(this.txtJson);
			this.Controls.Add(this.cmdRefreshJson);
			this.Controls.Add(this.txtStep);
			this.Controls.Add(this.cmdSynchTask);
			this.Controls.Add(this.txtContentExpression);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtAnswerValue);
			this.Controls.Add(this.lblContentValue);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdAddTask);
			this.Controls.Add(this.grpTarget);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtTask);
			this.Name = "frmZTaskAdd";
			this.Text = "Add New Task";
			this.grpTarget.ResumeLayout(false);
			this.grpTarget.PerformLayout();
			this.grpLocation.ResumeLayout(false);
			this.grpLocation.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtTask;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox grpTarget;
		private System.Windows.Forms.ComboBox cboProperty;
		private System.Windows.Forms.Label lblTargetProperty;
		private System.Windows.Forms.ComboBox cboTargetType;
		private System.Windows.Forms.Label lblTargetType;
		private System.Windows.Forms.GroupBox grpLocation;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cboTargetLocationType;
		private System.Windows.Forms.RichTextBox txtTargetLocationAddress;
		private System.Windows.Forms.Label lblTargetLocationType;
		private System.Windows.Forms.RichTextBox txtTargetLocationContext;
		private System.Windows.Forms.Label lblTargetLocationContext;
		private System.Windows.Forms.Button cmdAddTask;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.RichTextBox txtContentExpression;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RichTextBox txtAnswerValue;
		private System.Windows.Forms.Label lblContentValue;
		private System.Windows.Forms.Button cmdSynchTask;
		private System.Windows.Forms.RichTextBox txtStep;
		private System.Windows.Forms.Button cmdRefreshJson;
		private System.Windows.Forms.RichTextBox txtJson;
	}
}