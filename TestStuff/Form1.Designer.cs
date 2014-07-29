namespace TestStuff
{
	partial class frmTester
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTester));
			this.txtStep = new System.Windows.Forms.RichTextBox();
			this.cmdParse = new System.Windows.Forms.Button();
			this.txtOut = new System.Windows.Forms.TextBox();
			this.grpParseOpts = new System.Windows.Forms.GroupBox();
			this.radStepTags = new System.Windows.Forms.RadioButton();
			this.radReportIt = new System.Windows.Forms.RadioButton();
			this.radLeven = new System.Windows.Forms.RadioButton();
			this.radDumpRange = new System.Windows.Forms.RadioButton();
			this.radCellRange = new System.Windows.Forms.RadioButton();
			this.radRubric = new System.Windows.Forms.RadioButton();
			this.radGradeIt = new System.Windows.Forms.RadioButton();
			this.radFormula = new System.Windows.Forms.RadioButton();
			this.radRegEx = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtRubric = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tvTestCases = new System.Windows.Forms.TreeView();
			this.btnPreferences = new System.Windows.Forms.Button();
			this.cmdAddTask = new System.Windows.Forms.Button();
			this.grpParseOpts.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtStep
			// 
			this.txtStep.Location = new System.Drawing.Point(18, 27);
			this.txtStep.Margin = new System.Windows.Forms.Padding(2);
			this.txtStep.Name = "txtStep";
			this.txtStep.Size = new System.Drawing.Size(822, 87);
			this.txtStep.TabIndex = 0;
			this.txtStep.Text = resources.GetString("txtStep.Text");
			// 
			// cmdParse
			// 
			this.cmdParse.Location = new System.Drawing.Point(1076, 145);
			this.cmdParse.Margin = new System.Windows.Forms.Padding(2);
			this.cmdParse.Name = "cmdParse";
			this.cmdParse.Size = new System.Drawing.Size(56, 19);
			this.cmdParse.TabIndex = 2;
			this.cmdParse.Text = "Parse";
			this.cmdParse.UseVisualStyleBackColor = true;
			this.cmdParse.Click += new System.EventHandler(this.cmdParse_Click);
			// 
			// txtOut
			// 
			this.txtOut.Location = new System.Drawing.Point(20, 328);
			this.txtOut.Margin = new System.Windows.Forms.Padding(2);
			this.txtOut.Multiline = true;
			this.txtOut.Name = "txtOut";
			this.txtOut.ReadOnly = true;
			this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOut.Size = new System.Drawing.Size(708, 247);
			this.txtOut.TabIndex = 3;
			// 
			// grpParseOpts
			// 
			this.grpParseOpts.Controls.Add(this.radStepTags);
			this.grpParseOpts.Controls.Add(this.radReportIt);
			this.grpParseOpts.Controls.Add(this.radLeven);
			this.grpParseOpts.Controls.Add(this.radDumpRange);
			this.grpParseOpts.Controls.Add(this.radCellRange);
			this.grpParseOpts.Controls.Add(this.radRubric);
			this.grpParseOpts.Controls.Add(this.radGradeIt);
			this.grpParseOpts.Controls.Add(this.radFormula);
			this.grpParseOpts.Controls.Add(this.radRegEx);
			this.grpParseOpts.Location = new System.Drawing.Point(866, 11);
			this.grpParseOpts.Margin = new System.Windows.Forms.Padding(2);
			this.grpParseOpts.Name = "grpParseOpts";
			this.grpParseOpts.Padding = new System.Windows.Forms.Padding(2);
			this.grpParseOpts.Size = new System.Drawing.Size(311, 114);
			this.grpParseOpts.TabIndex = 4;
			this.grpParseOpts.TabStop = false;
			this.grpParseOpts.Text = "Options";
			// 
			// radStepTags
			// 
			this.radStepTags.AutoSize = true;
			this.radStepTags.Location = new System.Drawing.Point(187, 63);
			this.radStepTags.Name = "radStepTags";
			this.radStepTags.Size = new System.Drawing.Size(74, 17);
			this.radStepTags.TabIndex = 8;
			this.radStepTags.TabStop = true;
			this.radStepTags.Text = "Step Tags";
			this.radStepTags.UseVisualStyleBackColor = true;
			// 
			// radReportIt
			// 
			this.radReportIt.AutoSize = true;
			this.radReportIt.Location = new System.Drawing.Point(187, 40);
			this.radReportIt.Margin = new System.Windows.Forms.Padding(2);
			this.radReportIt.Name = "radReportIt";
			this.radReportIt.Size = new System.Drawing.Size(66, 17);
			this.radReportIt.TabIndex = 7;
			this.radReportIt.Text = "Report It";
			this.radReportIt.UseVisualStyleBackColor = true;
			// 
			// radLeven
			// 
			this.radLeven.AutoSize = true;
			this.radLeven.Location = new System.Drawing.Point(4, 86);
			this.radLeven.Margin = new System.Windows.Forms.Padding(2);
			this.radLeven.Name = "radLeven";
			this.radLeven.Size = new System.Drawing.Size(55, 17);
			this.radLeven.TabIndex = 6;
			this.radLeven.Text = "Leven";
			this.radLeven.UseVisualStyleBackColor = true;
			// 
			// radDumpRange
			// 
			this.radDumpRange.AutoSize = true;
			this.radDumpRange.Location = new System.Drawing.Point(5, 61);
			this.radDumpRange.Margin = new System.Windows.Forms.Padding(2);
			this.radDumpRange.Name = "radDumpRange";
			this.radDumpRange.Size = new System.Drawing.Size(88, 17);
			this.radDumpRange.TabIndex = 5;
			this.radDumpRange.Text = "Dump Range";
			this.radDumpRange.UseVisualStyleBackColor = true;
			// 
			// radCellRange
			// 
			this.radCellRange.AutoSize = true;
			this.radCellRange.Location = new System.Drawing.Point(68, 40);
			this.radCellRange.Margin = new System.Windows.Forms.Padding(2);
			this.radCellRange.Name = "radCellRange";
			this.radCellRange.Size = new System.Drawing.Size(79, 17);
			this.radCellRange.TabIndex = 4;
			this.radCellRange.Text = "Cell/Range";
			this.radCellRange.UseVisualStyleBackColor = true;
			// 
			// radRubric
			// 
			this.radRubric.AutoSize = true;
			this.radRubric.Location = new System.Drawing.Point(68, 18);
			this.radRubric.Margin = new System.Windows.Forms.Padding(2);
			this.radRubric.Name = "radRubric";
			this.radRubric.Size = new System.Drawing.Size(56, 17);
			this.radRubric.TabIndex = 3;
			this.radRubric.Text = "Rubric";
			this.radRubric.UseVisualStyleBackColor = true;
			// 
			// radGradeIt
			// 
			this.radGradeIt.AutoSize = true;
			this.radGradeIt.Checked = true;
			this.radGradeIt.Location = new System.Drawing.Point(187, 17);
			this.radGradeIt.Margin = new System.Windows.Forms.Padding(2);
			this.radGradeIt.Name = "radGradeIt";
			this.radGradeIt.Size = new System.Drawing.Size(63, 17);
			this.radGradeIt.TabIndex = 2;
			this.radGradeIt.TabStop = true;
			this.radGradeIt.Text = "Grade It";
			this.radGradeIt.UseVisualStyleBackColor = true;
			// 
			// radFormula
			// 
			this.radFormula.AutoSize = true;
			this.radFormula.Location = new System.Drawing.Point(5, 40);
			this.radFormula.Margin = new System.Windows.Forms.Padding(2);
			this.radFormula.Name = "radFormula";
			this.radFormula.Size = new System.Drawing.Size(62, 17);
			this.radFormula.TabIndex = 1;
			this.radFormula.Text = "Formula";
			this.radFormula.UseVisualStyleBackColor = true;
			// 
			// radRegEx
			// 
			this.radRegEx.AutoSize = true;
			this.radRegEx.Location = new System.Drawing.Point(5, 18);
			this.radRegEx.Margin = new System.Windows.Forms.Padding(2);
			this.radRegEx.Name = "radRegEx";
			this.radRegEx.Size = new System.Drawing.Size(57, 17);
			this.radRegEx.TabIndex = 0;
			this.radRegEx.Text = "RegEx";
			this.radRegEx.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Input";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 310);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Results";
			// 
			// txtRubric
			// 
			this.txtRubric.Location = new System.Drawing.Point(18, 145);
			this.txtRubric.Margin = new System.Windows.Forms.Padding(2);
			this.txtRubric.Name = "txtRubric";
			this.txtRubric.Size = new System.Drawing.Size(710, 144);
			this.txtRubric.TabIndex = 7;
			this.txtRubric.Text = "Type \tLowest Salary\t in \tE27\t and \tAverage Salary\t in \tE28\t.\n";
			this.txtRubric.TextChanged += new System.EventHandler(this.txtRubric_TextChanged);
			this.txtRubric.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtRubric_MouseDoubleClick);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 126);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Rubric";
			// 
			// tvTestCases
			// 
			this.tvTestCases.Location = new System.Drawing.Point(761, 145);
			this.tvTestCases.Name = "tvTestCases";
			this.tvTestCases.Size = new System.Drawing.Size(220, 347);
			this.tvTestCases.TabIndex = 9;
			this.tvTestCases.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTestCases_AfterSelect);
			// 
			// btnPreferences
			// 
			this.btnPreferences.Location = new System.Drawing.Point(1076, 178);
			this.btnPreferences.Name = "btnPreferences";
			this.btnPreferences.Size = new System.Drawing.Size(75, 23);
			this.btnPreferences.TabIndex = 10;
			this.btnPreferences.Text = "Preferences";
			this.btnPreferences.UseVisualStyleBackColor = true;
			this.btnPreferences.Click += new System.EventHandler(this.btnPreferences_Click);
			// 
			// cmdAddTask
			// 
			this.cmdAddTask.Location = new System.Drawing.Point(1076, 226);
			this.cmdAddTask.Name = "cmdAddTask";
			this.cmdAddTask.Size = new System.Drawing.Size(75, 23);
			this.cmdAddTask.TabIndex = 11;
			this.cmdAddTask.Text = "Add Task";
			this.cmdAddTask.UseVisualStyleBackColor = true;
			this.cmdAddTask.Click += new System.EventHandler(this.cmdAddTask_Click);
			// 
			// frmTester
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1205, 608);
			this.Controls.Add(this.cmdAddTask);
			this.Controls.Add(this.btnPreferences);
			this.Controls.Add(this.tvTestCases);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtRubric);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.grpParseOpts);
			this.Controls.Add(this.txtOut);
			this.Controls.Add(this.cmdParse);
			this.Controls.Add(this.txtStep);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "frmTester";
			this.Text = "Z-tester";
			this.grpParseOpts.ResumeLayout(false);
			this.grpParseOpts.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtStep;
		private System.Windows.Forms.Button cmdParse;
		private System.Windows.Forms.TextBox txtOut;
		private System.Windows.Forms.GroupBox grpParseOpts;
		private System.Windows.Forms.RadioButton radRegEx;
		private System.Windows.Forms.RadioButton radFormula;
		private System.Windows.Forms.RadioButton radRubric;
		private System.Windows.Forms.RadioButton radGradeIt;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RichTextBox txtRubric;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton radCellRange;
		private System.Windows.Forms.RadioButton radDumpRange;
		private System.Windows.Forms.RadioButton radLeven;
        private System.Windows.Forms.TreeView tvTestCases;
        private System.Windows.Forms.Button btnPreferences;
		private System.Windows.Forms.RadioButton radReportIt;
		private System.Windows.Forms.RadioButton radStepTags;
		private System.Windows.Forms.Button cmdAddTask;
	}
}

