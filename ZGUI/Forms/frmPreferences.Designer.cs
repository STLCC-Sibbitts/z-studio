namespace ZGUI
{
    partial class frmPreferences
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Default Scenarios");
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.tabsPreferences = new System.Windows.Forms.TabControl();
			this.tabBasicPreferences = new System.Windows.Forms.TabPage();
			this.grpMultipliers = new System.Windows.Forms.GroupBox();
			this.grpSkillLevels = new System.Windows.Forms.GroupBox();
			this.txtSkillLevelMultiplierExpert = new System.Windows.Forms.MaskedTextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.txtSkillLevelMultiplierAdvanced = new System.Windows.Forms.MaskedTextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.txtSkillLevelMultiplierIntermediate = new System.Windows.Forms.MaskedTextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.txtSkillLevelMultiplierBeginner = new System.Windows.Forms.MaskedTextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.txtSkillLevelMultiplierNovice = new System.Windows.Forms.MaskedTextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.chkSkillLevels = new System.Windows.Forms.CheckBox();
			this.grpDeductions = new System.Windows.Forms.GroupBox();
			this.txtDeductionPctFull = new System.Windows.Forms.MaskedTextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.txtDeductionPctMajor = new System.Windows.Forms.MaskedTextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtDeductionPctModerate = new System.Windows.Forms.MaskedTextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtDeductionPctMinor = new System.Windows.Forms.MaskedTextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.txtDeductionPctNone = new System.Windows.Forms.MaskedTextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.chkDeductions = new System.Windows.Forms.CheckBox();
			this.grpDifficulties = new System.Windows.Forms.GroupBox();
			this.txtDifficultyChallenge = new System.Windows.Forms.MaskedTextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtDifficultyHard = new System.Windows.Forms.MaskedTextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtDifficultyNormal = new System.Windows.Forms.MaskedTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtDifficultyEasy = new System.Windows.Forms.MaskedTextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.chkDifficulties = new System.Windows.Forms.CheckBox();
			this.grpRounding = new System.Windows.Forms.GroupBox();
			this.radRounding50 = new System.Windows.Forms.RadioButton();
			this.radRounding25 = new System.Windows.Forms.RadioButton();
			this.radRounding10 = new System.Windows.Forms.RadioButton();
			this.radRoundingNone = new System.Windows.Forms.RadioButton();
			this.chkPartialCredit = new System.Windows.Forms.CheckBox();
			this.tabContentPreferences = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.tvContentDefaultSceenarios = new System.Windows.Forms.TreeView();
			this.txtNodeNotes = new System.Windows.Forms.RichTextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.txtScenarioPath = new System.Windows.Forms.TextBox();
			this.grpDefaultScenario = new System.Windows.Forms.GroupBox();
			this.label28 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cboAllocationCategory = new System.Windows.Forms.ComboBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.cboDeductionType = new System.Windows.Forms.ComboBox();
			this.grpRemediation = new System.Windows.Forms.GroupBox();
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.txtFeedback = new System.Windows.Forms.RichTextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.txtNotes = new System.Windows.Forms.RichTextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.nudThreshold = new System.Windows.Forms.NumericUpDown();
			this.label21 = new System.Windows.Forms.Label();
			this.chkScenarioEnabled = new System.Windows.Forms.CheckBox();
			this.tabFormattingPreferences = new System.Windows.Forms.TabPage();
			this.tabProjectPreferences = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtPPE_LO = new System.Windows.Forms.TextBox();
			this.txtPtsLO = new System.Windows.Forms.TextBox();
			this.chkLO = new System.Windows.Forms.CheckBox();
			this.nudLO = new System.Windows.Forms.NumericUpDown();
			this.txtPPE_EE = new System.Windows.Forms.TextBox();
			this.txtPtsEE = new System.Windows.Forms.TextBox();
			this.chkEE = new System.Windows.Forms.CheckBox();
			this.nudEE = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPPE_NCE = new System.Windows.Forms.TextBox();
			this.txtPtsNCE = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.chkNCE = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.nudNCE = new System.Windows.Forms.NumericUpDown();
			this.txtDefaultProjectPoints = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.nudThresholdPct = new System.Windows.Forms.NumericUpDown();
			this.tabsPreferences.SuspendLayout();
			this.tabBasicPreferences.SuspendLayout();
			this.grpMultipliers.SuspendLayout();
			this.grpSkillLevels.SuspendLayout();
			this.grpDeductions.SuspendLayout();
			this.grpDifficulties.SuspendLayout();
			this.grpRounding.SuspendLayout();
			this.tabContentPreferences.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.grpDefaultScenario.SuspendLayout();
			this.panel1.SuspendLayout();
			this.grpRemediation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).BeginInit();
			this.tabProjectPreferences.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLO)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudEE)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudNCE)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudThresholdPct)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(735, 12);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(639, 12);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// tabsPreferences
			// 
			this.tabsPreferences.Controls.Add(this.tabBasicPreferences);
			this.tabsPreferences.Controls.Add(this.tabContentPreferences);
			this.tabsPreferences.Controls.Add(this.tabFormattingPreferences);
			this.tabsPreferences.Controls.Add(this.tabProjectPreferences);
			this.tabsPreferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabsPreferences.Location = new System.Drawing.Point(0, 0);
			this.tabsPreferences.Name = "tabsPreferences";
			this.tabsPreferences.SelectedIndex = 0;
			this.tabsPreferences.Size = new System.Drawing.Size(931, 663);
			this.tabsPreferences.TabIndex = 3;
			// 
			// tabBasicPreferences
			// 
			this.tabBasicPreferences.Controls.Add(this.grpMultipliers);
			this.tabBasicPreferences.Controls.Add(this.grpRounding);
			this.tabBasicPreferences.Controls.Add(this.chkPartialCredit);
			this.tabBasicPreferences.Location = new System.Drawing.Point(4, 22);
			this.tabBasicPreferences.Name = "tabBasicPreferences";
			this.tabBasicPreferences.Padding = new System.Windows.Forms.Padding(3);
			this.tabBasicPreferences.Size = new System.Drawing.Size(923, 637);
			this.tabBasicPreferences.TabIndex = 0;
			this.tabBasicPreferences.Text = "Basic";
			this.tabBasicPreferences.UseVisualStyleBackColor = true;
			// 
			// grpMultipliers
			// 
			this.grpMultipliers.Controls.Add(this.grpSkillLevels);
			this.grpMultipliers.Controls.Add(this.grpDeductions);
			this.grpMultipliers.Controls.Add(this.grpDifficulties);
			this.grpMultipliers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpMultipliers.Location = new System.Drawing.Point(160, 19);
			this.grpMultipliers.Name = "grpMultipliers";
			this.grpMultipliers.Size = new System.Drawing.Size(639, 199);
			this.grpMultipliers.TabIndex = 11;
			this.grpMultipliers.TabStop = false;
			this.grpMultipliers.Text = "Multipliers";
			// 
			// grpSkillLevels
			// 
			this.grpSkillLevels.Controls.Add(this.txtSkillLevelMultiplierExpert);
			this.grpSkillLevels.Controls.Add(this.label19);
			this.grpSkillLevels.Controls.Add(this.txtSkillLevelMultiplierAdvanced);
			this.grpSkillLevels.Controls.Add(this.label14);
			this.grpSkillLevels.Controls.Add(this.txtSkillLevelMultiplierIntermediate);
			this.grpSkillLevels.Controls.Add(this.label15);
			this.grpSkillLevels.Controls.Add(this.txtSkillLevelMultiplierBeginner);
			this.grpSkillLevels.Controls.Add(this.label16);
			this.grpSkillLevels.Controls.Add(this.txtSkillLevelMultiplierNovice);
			this.grpSkillLevels.Controls.Add(this.label17);
			this.grpSkillLevels.Controls.Add(this.chkSkillLevels);
			this.grpSkillLevels.Location = new System.Drawing.Point(421, 24);
			this.grpSkillLevels.Name = "grpSkillLevels";
			this.grpSkillLevels.Size = new System.Drawing.Size(200, 152);
			this.grpSkillLevels.TabIndex = 10;
			this.grpSkillLevels.TabStop = false;
			this.grpSkillLevels.Tag = "SkillLevels";
			this.grpSkillLevels.Text = "SkillLevels";
			// 
			// txtSkillLevelMultiplierExpert
			// 
			this.txtSkillLevelMultiplierExpert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSkillLevelMultiplierExpert.Location = new System.Drawing.Point(95, 122);
			this.txtSkillLevelMultiplierExpert.Mask = "0.00";
			this.txtSkillLevelMultiplierExpert.Name = "txtSkillLevelMultiplierExpert";
			this.txtSkillLevelMultiplierExpert.Size = new System.Drawing.Size(44, 23);
			this.txtSkillLevelMultiplierExpert.TabIndex = 10;
			this.txtSkillLevelMultiplierExpert.Tag = "Multipliers[Expert]";
			this.txtSkillLevelMultiplierExpert.Text = "200";
			this.txtSkillLevelMultiplierExpert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label19.Location = new System.Drawing.Point(6, 122);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(75, 23);
			this.label19.TabIndex = 9;
			this.label19.Text = "Expert";
			// 
			// txtSkillLevelMultiplierAdvanced
			// 
			this.txtSkillLevelMultiplierAdvanced.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSkillLevelMultiplierAdvanced.Location = new System.Drawing.Point(95, 97);
			this.txtSkillLevelMultiplierAdvanced.Mask = "0.00";
			this.txtSkillLevelMultiplierAdvanced.Name = "txtSkillLevelMultiplierAdvanced";
			this.txtSkillLevelMultiplierAdvanced.Size = new System.Drawing.Size(44, 23);
			this.txtSkillLevelMultiplierAdvanced.TabIndex = 8;
			this.txtSkillLevelMultiplierAdvanced.Tag = "Multipliers[Advanced]";
			this.txtSkillLevelMultiplierAdvanced.Text = "175";
			this.txtSkillLevelMultiplierAdvanced.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(6, 97);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(75, 23);
			this.label14.TabIndex = 7;
			this.label14.Text = "Advanced";
			// 
			// txtSkillLevelMultiplierIntermediate
			// 
			this.txtSkillLevelMultiplierIntermediate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSkillLevelMultiplierIntermediate.Location = new System.Drawing.Point(95, 73);
			this.txtSkillLevelMultiplierIntermediate.Mask = "0.00";
			this.txtSkillLevelMultiplierIntermediate.Name = "txtSkillLevelMultiplierIntermediate";
			this.txtSkillLevelMultiplierIntermediate.Size = new System.Drawing.Size(44, 23);
			this.txtSkillLevelMultiplierIntermediate.TabIndex = 6;
			this.txtSkillLevelMultiplierIntermediate.Tag = "Multipliers[Intermediate]";
			this.txtSkillLevelMultiplierIntermediate.Text = "150";
			this.txtSkillLevelMultiplierIntermediate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(6, 72);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(97, 23);
			this.label15.TabIndex = 5;
			this.label15.Text = "Intermediate";
			// 
			// txtSkillLevelMultiplierBeginner
			// 
			this.txtSkillLevelMultiplierBeginner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSkillLevelMultiplierBeginner.Location = new System.Drawing.Point(95, 47);
			this.txtSkillLevelMultiplierBeginner.Mask = "0.00";
			this.txtSkillLevelMultiplierBeginner.Name = "txtSkillLevelMultiplierBeginner";
			this.txtSkillLevelMultiplierBeginner.Size = new System.Drawing.Size(44, 23);
			this.txtSkillLevelMultiplierBeginner.TabIndex = 4;
			this.txtSkillLevelMultiplierBeginner.Tag = "Multipliers[Beginner]";
			this.txtSkillLevelMultiplierBeginner.Text = "125";
			this.txtSkillLevelMultiplierBeginner.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(6, 47);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(75, 23);
			this.label16.TabIndex = 3;
			this.label16.Text = "Beginner";
			// 
			// txtSkillLevelMultiplierNovice
			// 
			this.txtSkillLevelMultiplierNovice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSkillLevelMultiplierNovice.Location = new System.Drawing.Point(95, 22);
			this.txtSkillLevelMultiplierNovice.Mask = "0.00";
			this.txtSkillLevelMultiplierNovice.Name = "txtSkillLevelMultiplierNovice";
			this.txtSkillLevelMultiplierNovice.Size = new System.Drawing.Size(44, 23);
			this.txtSkillLevelMultiplierNovice.TabIndex = 2;
			this.txtSkillLevelMultiplierNovice.Tag = "Multipliers[Novice]";
			this.txtSkillLevelMultiplierNovice.Text = "100";
			this.txtSkillLevelMultiplierNovice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label17.Location = new System.Drawing.Point(6, 22);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(75, 23);
			this.label17.TabIndex = 1;
			this.label17.Text = "Novice";
			// 
			// chkSkillLevels
			// 
			this.chkSkillLevels.AutoSize = true;
			this.chkSkillLevels.BackColor = System.Drawing.SystemColors.Window;
			this.chkSkillLevels.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkSkillLevels.Location = new System.Drawing.Point(6, 0);
			this.chkSkillLevels.Name = "chkSkillLevels";
			this.chkSkillLevels.Size = new System.Drawing.Size(97, 21);
			this.chkSkillLevels.TabIndex = 0;
			this.chkSkillLevels.Tag = "Enabled";
			this.chkSkillLevels.Text = "Skill Levels";
			this.chkSkillLevels.UseVisualStyleBackColor = false;
			this.chkSkillLevels.CheckedChanged += new System.EventHandler(this.chkMultiplierOptions_CheckedChanged);
			// 
			// grpDeductions
			// 
			this.grpDeductions.Controls.Add(this.txtDeductionPctFull);
			this.grpDeductions.Controls.Add(this.label18);
			this.grpDeductions.Controls.Add(this.txtDeductionPctMajor);
			this.grpDeductions.Controls.Add(this.label10);
			this.grpDeductions.Controls.Add(this.txtDeductionPctModerate);
			this.grpDeductions.Controls.Add(this.label11);
			this.grpDeductions.Controls.Add(this.txtDeductionPctMinor);
			this.grpDeductions.Controls.Add(this.label12);
			this.grpDeductions.Controls.Add(this.txtDeductionPctNone);
			this.grpDeductions.Controls.Add(this.label13);
			this.grpDeductions.Controls.Add(this.chkDeductions);
			this.grpDeductions.Location = new System.Drawing.Point(215, 24);
			this.grpDeductions.Name = "grpDeductions";
			this.grpDeductions.Size = new System.Drawing.Size(200, 152);
			this.grpDeductions.TabIndex = 9;
			this.grpDeductions.TabStop = false;
			this.grpDeductions.Tag = "Deductions";
			this.grpDeductions.Text = "Deductions";
			// 
			// txtDeductionPctFull
			// 
			this.txtDeductionPctFull.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDeductionPctFull.Location = new System.Drawing.Point(95, 122);
			this.txtDeductionPctFull.Mask = "0.00";
			this.txtDeductionPctFull.Name = "txtDeductionPctFull";
			this.txtDeductionPctFull.Size = new System.Drawing.Size(44, 23);
			this.txtDeductionPctFull.TabIndex = 10;
			this.txtDeductionPctFull.Tag = "Multipliers[Full]";
			this.txtDeductionPctFull.Text = "100";
			this.txtDeductionPctFull.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label18.Location = new System.Drawing.Point(7, 122);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(75, 23);
			this.label18.TabIndex = 9;
			this.label18.Text = "Full";
			// 
			// txtDeductionPctMajor
			// 
			this.txtDeductionPctMajor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDeductionPctMajor.Location = new System.Drawing.Point(95, 97);
			this.txtDeductionPctMajor.Mask = "0.00";
			this.txtDeductionPctMajor.Name = "txtDeductionPctMajor";
			this.txtDeductionPctMajor.Size = new System.Drawing.Size(44, 23);
			this.txtDeductionPctMajor.TabIndex = 8;
			this.txtDeductionPctMajor.Tag = "Multipliers[Major]";
			this.txtDeductionPctMajor.Text = "075";
			this.txtDeductionPctMajor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(6, 97);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(75, 23);
			this.label10.TabIndex = 7;
			this.label10.Text = "Major";
			// 
			// txtDeductionPctModerate
			// 
			this.txtDeductionPctModerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDeductionPctModerate.Location = new System.Drawing.Point(95, 73);
			this.txtDeductionPctModerate.Mask = "0.00";
			this.txtDeductionPctModerate.Name = "txtDeductionPctModerate";
			this.txtDeductionPctModerate.Size = new System.Drawing.Size(44, 23);
			this.txtDeductionPctModerate.TabIndex = 6;
			this.txtDeductionPctModerate.Tag = "Multipliers[Moderate]";
			this.txtDeductionPctModerate.Text = "050";
			this.txtDeductionPctModerate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(6, 71);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(75, 23);
			this.label11.TabIndex = 5;
			this.label11.Text = "Moderate";
			// 
			// txtDeductionPctMinor
			// 
			this.txtDeductionPctMinor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDeductionPctMinor.Location = new System.Drawing.Point(95, 47);
			this.txtDeductionPctMinor.Mask = "0.00";
			this.txtDeductionPctMinor.Name = "txtDeductionPctMinor";
			this.txtDeductionPctMinor.Size = new System.Drawing.Size(44, 23);
			this.txtDeductionPctMinor.TabIndex = 4;
			this.txtDeductionPctMinor.Tag = "Multipliers[Minor]";
			this.txtDeductionPctMinor.Text = "025";
			this.txtDeductionPctMinor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(6, 47);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(75, 23);
			this.label12.TabIndex = 3;
			this.label12.Text = "Minor";
			// 
			// txtDeductionPctNone
			// 
			this.txtDeductionPctNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDeductionPctNone.Location = new System.Drawing.Point(95, 22);
			this.txtDeductionPctNone.Mask = "0.00";
			this.txtDeductionPctNone.Name = "txtDeductionPctNone";
			this.txtDeductionPctNone.Size = new System.Drawing.Size(44, 23);
			this.txtDeductionPctNone.TabIndex = 2;
			this.txtDeductionPctNone.Tag = "Multipliers[None]";
			this.txtDeductionPctNone.Text = "000";
			this.txtDeductionPctNone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(6, 22);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(75, 23);
			this.label13.TabIndex = 1;
			this.label13.Text = "None";
			// 
			// chkDeductions
			// 
			this.chkDeductions.AutoSize = true;
			this.chkDeductions.BackColor = System.Drawing.SystemColors.Window;
			this.chkDeductions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkDeductions.Location = new System.Drawing.Point(6, 0);
			this.chkDeductions.Name = "chkDeductions";
			this.chkDeductions.Size = new System.Drawing.Size(98, 21);
			this.chkDeductions.TabIndex = 0;
			this.chkDeductions.Tag = "Enabled";
			this.chkDeductions.Text = "Deductions";
			this.chkDeductions.UseVisualStyleBackColor = false;
			this.chkDeductions.CheckedChanged += new System.EventHandler(this.chkMultiplierOptions_CheckedChanged);
			// 
			// grpDifficulties
			// 
			this.grpDifficulties.Controls.Add(this.txtDifficultyChallenge);
			this.grpDifficulties.Controls.Add(this.label9);
			this.grpDifficulties.Controls.Add(this.txtDifficultyHard);
			this.grpDifficulties.Controls.Add(this.label8);
			this.grpDifficulties.Controls.Add(this.txtDifficultyNormal);
			this.grpDifficulties.Controls.Add(this.label7);
			this.grpDifficulties.Controls.Add(this.txtDifficultyEasy);
			this.grpDifficulties.Controls.Add(this.label6);
			this.grpDifficulties.Controls.Add(this.chkDifficulties);
			this.grpDifficulties.Location = new System.Drawing.Point(9, 24);
			this.grpDifficulties.Name = "grpDifficulties";
			this.grpDifficulties.Size = new System.Drawing.Size(200, 152);
			this.grpDifficulties.TabIndex = 5;
			this.grpDifficulties.TabStop = false;
			this.grpDifficulties.Tag = "Difficulties";
			this.grpDifficulties.Text = "Difficulties";
			// 
			// txtDifficultyChallenge
			// 
			this.txtDifficultyChallenge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDifficultyChallenge.Location = new System.Drawing.Point(95, 97);
			this.txtDifficultyChallenge.Mask = "0.00";
			this.txtDifficultyChallenge.Name = "txtDifficultyChallenge";
			this.txtDifficultyChallenge.Size = new System.Drawing.Size(44, 23);
			this.txtDifficultyChallenge.TabIndex = 8;
			this.txtDifficultyChallenge.Tag = "Multipliers[Challenge]";
			this.txtDifficultyChallenge.Text = "200";
			this.txtDifficultyChallenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(6, 97);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(75, 23);
			this.label9.TabIndex = 7;
			this.label9.Text = "Challenge";
			// 
			// txtDifficultyHard
			// 
			this.txtDifficultyHard.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDifficultyHard.Location = new System.Drawing.Point(95, 73);
			this.txtDifficultyHard.Mask = "0.00";
			this.txtDifficultyHard.Name = "txtDifficultyHard";
			this.txtDifficultyHard.Size = new System.Drawing.Size(44, 23);
			this.txtDifficultyHard.TabIndex = 6;
			this.txtDifficultyHard.Tag = "Multipliers[Hard]";
			this.txtDifficultyHard.Text = "150";
			this.txtDifficultyHard.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(6, 71);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(75, 23);
			this.label8.TabIndex = 5;
			this.label8.Text = "Hard";
			// 
			// txtDifficultyNormal
			// 
			this.txtDifficultyNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDifficultyNormal.Location = new System.Drawing.Point(95, 47);
			this.txtDifficultyNormal.Mask = "0.00";
			this.txtDifficultyNormal.Name = "txtDifficultyNormal";
			this.txtDifficultyNormal.Size = new System.Drawing.Size(44, 23);
			this.txtDifficultyNormal.TabIndex = 4;
			this.txtDifficultyNormal.Tag = "Multipliers[Normal]";
			this.txtDifficultyNormal.Text = "100";
			this.txtDifficultyNormal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(6, 47);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(75, 23);
			this.label7.TabIndex = 3;
			this.label7.Text = "Normal";
			// 
			// txtDifficultyEasy
			// 
			this.txtDifficultyEasy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDifficultyEasy.Location = new System.Drawing.Point(95, 22);
			this.txtDifficultyEasy.Mask = "0.00";
			this.txtDifficultyEasy.Name = "txtDifficultyEasy";
			this.txtDifficultyEasy.Size = new System.Drawing.Size(44, 23);
			this.txtDifficultyEasy.TabIndex = 2;
			this.txtDifficultyEasy.Tag = "Multipliers[Easy]";
			this.txtDifficultyEasy.Text = "025";
			this.txtDifficultyEasy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(6, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 23);
			this.label6.TabIndex = 1;
			this.label6.Text = "Easy";
			// 
			// chkDifficulties
			// 
			this.chkDifficulties.AutoSize = true;
			this.chkDifficulties.BackColor = System.Drawing.SystemColors.Window;
			this.chkDifficulties.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkDifficulties.Location = new System.Drawing.Point(6, 0);
			this.chkDifficulties.Name = "chkDifficulties";
			this.chkDifficulties.Size = new System.Drawing.Size(91, 21);
			this.chkDifficulties.TabIndex = 0;
			this.chkDifficulties.Tag = "Enabled";
			this.chkDifficulties.Text = "Difficulties";
			this.chkDifficulties.UseVisualStyleBackColor = false;
			this.chkDifficulties.CheckedChanged += new System.EventHandler(this.chkMultiplierOptions_CheckedChanged);
			// 
			// grpRounding
			// 
			this.grpRounding.Controls.Add(this.radRounding50);
			this.grpRounding.Controls.Add(this.radRounding25);
			this.grpRounding.Controls.Add(this.radRounding10);
			this.grpRounding.Controls.Add(this.radRoundingNone);
			this.grpRounding.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpRounding.Location = new System.Drawing.Point(28, 46);
			this.grpRounding.Name = "grpRounding";
			this.grpRounding.Size = new System.Drawing.Size(83, 116);
			this.grpRounding.TabIndex = 3;
			this.grpRounding.TabStop = false;
			this.grpRounding.Text = "Rounding";
			// 
			// radRounding50
			// 
			this.radRounding50.AutoSize = true;
			this.radRounding50.Location = new System.Drawing.Point(7, 90);
			this.radRounding50.Name = "radRounding50";
			this.radRounding50.Size = new System.Drawing.Size(62, 21);
			this.radRounding50.TabIndex = 3;
			this.radRounding50.Tag = "50";
			this.radRounding50.Text = "1/2 pt";
			this.radRounding50.UseVisualStyleBackColor = true;
			this.radRounding50.CheckedChanged += new System.EventHandler(this.radRounding_CheckedChanged);
			// 
			// radRounding25
			// 
			this.radRounding25.AutoSize = true;
			this.radRounding25.Checked = true;
			this.radRounding25.Location = new System.Drawing.Point(7, 66);
			this.radRounding25.Name = "radRounding25";
			this.radRounding25.Size = new System.Drawing.Size(62, 21);
			this.radRounding25.TabIndex = 2;
			this.radRounding25.TabStop = true;
			this.radRounding25.Tag = "25";
			this.radRounding25.Text = "1/4 pt";
			this.radRounding25.UseVisualStyleBackColor = true;
			this.radRounding25.CheckedChanged += new System.EventHandler(this.radRounding_CheckedChanged);
			// 
			// radRounding10
			// 
			this.radRounding10.AutoSize = true;
			this.radRounding10.Location = new System.Drawing.Point(7, 42);
			this.radRounding10.Name = "radRounding10";
			this.radRounding10.Size = new System.Drawing.Size(70, 21);
			this.radRounding10.TabIndex = 1;
			this.radRounding10.Tag = "10";
			this.radRounding10.Text = "1/10 pt";
			this.radRounding10.UseVisualStyleBackColor = true;
			this.radRounding10.CheckedChanged += new System.EventHandler(this.radRounding_CheckedChanged);
			// 
			// radRoundingNone
			// 
			this.radRoundingNone.AutoSize = true;
			this.radRoundingNone.Location = new System.Drawing.Point(7, 18);
			this.radRoundingNone.Name = "radRoundingNone";
			this.radRoundingNone.Size = new System.Drawing.Size(60, 21);
			this.radRoundingNone.TabIndex = 0;
			this.radRoundingNone.Tag = "0";
			this.radRoundingNone.Text = "None";
			this.radRoundingNone.UseVisualStyleBackColor = true;
			this.radRoundingNone.CheckedChanged += new System.EventHandler(this.radRounding_CheckedChanged);
			// 
			// chkPartialCredit
			// 
			this.chkPartialCredit.AutoSize = true;
			this.chkPartialCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkPartialCredit.Location = new System.Drawing.Point(28, 19);
			this.chkPartialCredit.Name = "chkPartialCredit";
			this.chkPartialCredit.Size = new System.Drawing.Size(108, 21);
			this.chkPartialCredit.TabIndex = 0;
			this.chkPartialCredit.Text = "Partial Credit";
			this.chkPartialCredit.UseVisualStyleBackColor = true;
			// 
			// tabContentPreferences
			// 
			this.tabContentPreferences.Controls.Add(this.splitContainer1);
			this.tabContentPreferences.Location = new System.Drawing.Point(4, 22);
			this.tabContentPreferences.Name = "tabContentPreferences";
			this.tabContentPreferences.Padding = new System.Windows.Forms.Padding(3);
			this.tabContentPreferences.Size = new System.Drawing.Size(923, 637);
			this.tabContentPreferences.TabIndex = 1;
			this.tabContentPreferences.Text = "Content";
			this.tabContentPreferences.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.label20);
			this.splitContainer1.Panel2.Controls.Add(this.txtScenarioPath);
			this.splitContainer1.Panel2.Controls.Add(this.grpDefaultScenario);
			this.splitContainer1.Size = new System.Drawing.Size(917, 631);
			this.splitContainer1.SplitterDistance = 218;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.tvContentDefaultSceenarios);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.txtNodeNotes);
			this.splitContainer2.Size = new System.Drawing.Size(218, 631);
			this.splitContainer2.SplitterDistance = 471;
			this.splitContainer2.TabIndex = 0;
			// 
			// tvContentDefaultSceenarios
			// 
			this.tvContentDefaultSceenarios.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvContentDefaultSceenarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tvContentDefaultSceenarios.FullRowSelect = true;
			this.tvContentDefaultSceenarios.Location = new System.Drawing.Point(0, 0);
			this.tvContentDefaultSceenarios.Name = "tvContentDefaultSceenarios";
			treeNode1.Name = "tnDefaultContentScenarios";
			treeNode1.Tag = "Preferences[Content]";
			treeNode1.Text = "Default Scenarios";
			this.tvContentDefaultSceenarios.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
			this.tvContentDefaultSceenarios.PathSeparator = "->";
			this.tvContentDefaultSceenarios.ShowNodeToolTips = true;
			this.tvContentDefaultSceenarios.Size = new System.Drawing.Size(218, 471);
			this.tvContentDefaultSceenarios.TabIndex = 0;
			this.tvContentDefaultSceenarios.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvContentDefaultSceenarios_BeforeSelect);
			this.tvContentDefaultSceenarios.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvContentDefaultSceenarios_AfterSelect);
			// 
			// txtNodeNotes
			// 
			this.txtNodeNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtNodeNotes.Location = new System.Drawing.Point(0, 0);
			this.txtNodeNotes.Name = "txtNodeNotes";
			this.txtNodeNotes.Size = new System.Drawing.Size(218, 156);
			this.txtNodeNotes.TabIndex = 1;
			this.txtNodeNotes.Text = "";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label20.Location = new System.Drawing.Point(10, 18);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(64, 17);
			this.label20.TabIndex = 2;
			this.label20.Text = "Scenario";
			// 
			// txtScenarioPath
			// 
			this.txtScenarioPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtScenarioPath.Location = new System.Drawing.Point(178, 15);
			this.txtScenarioPath.Name = "txtScenarioPath";
			this.txtScenarioPath.Size = new System.Drawing.Size(512, 23);
			this.txtScenarioPath.TabIndex = 1;
			// 
			// grpDefaultScenario
			// 
			this.grpDefaultScenario.Controls.Add(this.nudThresholdPct);
			this.grpDefaultScenario.Controls.Add(this.label28);
			this.grpDefaultScenario.Controls.Add(this.panel1);
			this.grpDefaultScenario.Controls.Add(this.grpRemediation);
			this.grpDefaultScenario.Controls.Add(this.txtNotes);
			this.grpDefaultScenario.Controls.Add(this.label24);
			this.grpDefaultScenario.Controls.Add(this.nudThreshold);
			this.grpDefaultScenario.Controls.Add(this.label21);
			this.grpDefaultScenario.Controls.Add(this.chkScenarioEnabled);
			this.grpDefaultScenario.Location = new System.Drawing.Point(3, 56);
			this.grpDefaultScenario.Name = "grpDefaultScenario";
			this.grpDefaultScenario.Size = new System.Drawing.Size(687, 546);
			this.grpDefaultScenario.TabIndex = 0;
			this.grpDefaultScenario.TabStop = false;
			this.grpDefaultScenario.Tag = "OName";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label28.Location = new System.Drawing.Point(333, 31);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(149, 17);
			this.label28.TabIndex = 12;
			this.label28.Text = "Threshold Percentage";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cboAllocationCategory);
			this.panel1.Controls.Add(this.label23);
			this.panel1.Controls.Add(this.label22);
			this.panel1.Controls.Add(this.cboDeductionType);
			this.panel1.Location = new System.Drawing.Point(0, 58);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(316, 68);
			this.panel1.TabIndex = 11;
			this.panel1.Tag = "Deduction";
			// 
			// cboAllocationCategory
			// 
			this.cboAllocationCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboAllocationCategory.FormattingEnabled = true;
			this.cboAllocationCategory.Items.AddRange(new object[] {
            "",
            "NCE",
            "EE",
            "LO"});
			this.cboAllocationCategory.Location = new System.Drawing.Point(178, 7);
			this.cboAllocationCategory.Name = "cboAllocationCategory";
			this.cboAllocationCategory.Size = new System.Drawing.Size(121, 24);
			this.cboAllocationCategory.TabIndex = 9;
			this.cboAllocationCategory.Tag = "Category";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label23.Location = new System.Drawing.Point(12, 10);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(130, 17);
			this.label23.TabIndex = 8;
			this.label23.Tag = "";
			this.label23.Text = "Allocation Category";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label22.Location = new System.Drawing.Point(12, 39);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(108, 17);
			this.label22.TabIndex = 7;
			this.label22.Tag = "";
			this.label22.Text = "Deduction Type";
			// 
			// cboDeductionType
			// 
			this.cboDeductionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboDeductionType.FormattingEnabled = true;
			this.cboDeductionType.Items.AddRange(new object[] {
            "",
            "None",
            "Minor",
            "Moderate",
            "Major",
            "Full"});
			this.cboDeductionType.Location = new System.Drawing.Point(178, 36);
			this.cboDeductionType.Name = "cboDeductionType";
			this.cboDeductionType.Size = new System.Drawing.Size(121, 24);
			this.cboDeductionType.TabIndex = 6;
			this.cboDeductionType.Tag = "Type";
			// 
			// grpRemediation
			// 
			this.grpRemediation.Controls.Add(this.richTextBox2);
			this.grpRemediation.Controls.Add(this.label27);
			this.grpRemediation.Controls.Add(this.richTextBox1);
			this.grpRemediation.Controls.Add(this.label26);
			this.grpRemediation.Controls.Add(this.txtFeedback);
			this.grpRemediation.Controls.Add(this.label25);
			this.grpRemediation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpRemediation.Location = new System.Drawing.Point(6, 124);
			this.grpRemediation.Name = "grpRemediation";
			this.grpRemediation.Size = new System.Drawing.Size(675, 317);
			this.grpRemediation.TabIndex = 9;
			this.grpRemediation.TabStop = false;
			this.grpRemediation.Tag = "Remediation";
			this.grpRemediation.Text = "Remediation";
			// 
			// richTextBox2
			// 
			this.richTextBox2.Location = new System.Drawing.Point(176, 208);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.Size = new System.Drawing.Size(491, 77);
			this.richTextBox2.TabIndex = 5;
			this.richTextBox2.Tag = "MissingFeedback";
			this.richTextBox2.Text = "";
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(8, 208);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(55, 17);
			this.label27.TabIndex = 4;
			this.label27.Text = "Missing";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(176, 119);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(491, 77);
			this.richTextBox1.TabIndex = 3;
			this.richTextBox1.Tag = "PartialCreditFeedback";
			this.richTextBox1.Text = "";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(8, 119);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(89, 17);
			this.label26.TabIndex = 2;
			this.label26.Text = "Partial Credit";
			// 
			// txtFeedback
			// 
			this.txtFeedback.Location = new System.Drawing.Point(178, 30);
			this.txtFeedback.Name = "txtFeedback";
			this.txtFeedback.Size = new System.Drawing.Size(491, 77);
			this.txtFeedback.TabIndex = 1;
			this.txtFeedback.Tag = "Feedback";
			this.txtFeedback.Text = "";
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(10, 30);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(70, 17);
			this.label25.TabIndex = 0;
			this.label25.Text = "Feedback";
			// 
			// txtNotes
			// 
			this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNotes.Location = new System.Drawing.Point(178, 456);
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.Size = new System.Drawing.Size(491, 77);
			this.txtNotes.TabIndex = 8;
			this.txtNotes.Tag = "Notes";
			this.txtNotes.Text = "";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label24.Location = new System.Drawing.Point(10, 456);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(45, 17);
			this.label24.TabIndex = 7;
			this.label24.Text = "Notes";
			// 
			// nudThreshold
			// 
			this.nudThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nudThreshold.Location = new System.Drawing.Point(178, 29);
			this.nudThreshold.Name = "nudThreshold";
			this.nudThreshold.Size = new System.Drawing.Size(38, 23);
			this.nudThreshold.TabIndex = 2;
			this.nudThreshold.Tag = "Threshold";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label21.Location = new System.Drawing.Point(10, 31);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(72, 17);
			this.label21.TabIndex = 1;
			this.label21.Text = "Threshold";
			// 
			// chkScenarioEnabled
			// 
			this.chkScenarioEnabled.AutoSize = true;
			this.chkScenarioEnabled.BackColor = System.Drawing.SystemColors.Window;
			this.chkScenarioEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkScenarioEnabled.Location = new System.Drawing.Point(30, 0);
			this.chkScenarioEnabled.Name = "chkScenarioEnabled";
			this.chkScenarioEnabled.Size = new System.Drawing.Size(139, 21);
			this.chkScenarioEnabled.TabIndex = 0;
			this.chkScenarioEnabled.Tag = "Enabled";
			this.chkScenarioEnabled.Text = "Scenario Enabled";
			this.chkScenarioEnabled.UseVisualStyleBackColor = false;
			// 
			// tabFormattingPreferences
			// 
			this.tabFormattingPreferences.Location = new System.Drawing.Point(4, 22);
			this.tabFormattingPreferences.Name = "tabFormattingPreferences";
			this.tabFormattingPreferences.Size = new System.Drawing.Size(923, 637);
			this.tabFormattingPreferences.TabIndex = 3;
			this.tabFormattingPreferences.Text = "Formatting";
			this.tabFormattingPreferences.UseVisualStyleBackColor = true;
			// 
			// tabProjectPreferences
			// 
			this.tabProjectPreferences.Controls.Add(this.groupBox1);
			this.tabProjectPreferences.Controls.Add(this.txtDefaultProjectPoints);
			this.tabProjectPreferences.Controls.Add(this.label1);
			this.tabProjectPreferences.Location = new System.Drawing.Point(4, 22);
			this.tabProjectPreferences.Name = "tabProjectPreferences";
			this.tabProjectPreferences.Padding = new System.Windows.Forms.Padding(3);
			this.tabProjectPreferences.Size = new System.Drawing.Size(923, 637);
			this.tabProjectPreferences.TabIndex = 2;
			this.tabProjectPreferences.Text = "Project Defaults";
			this.tabProjectPreferences.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtPPE_LO);
			this.groupBox1.Controls.Add(this.txtPtsLO);
			this.groupBox1.Controls.Add(this.chkLO);
			this.groupBox1.Controls.Add(this.nudLO);
			this.groupBox1.Controls.Add(this.txtPPE_EE);
			this.groupBox1.Controls.Add(this.txtPtsEE);
			this.groupBox1.Controls.Add(this.chkEE);
			this.groupBox1.Controls.Add(this.nudEE);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtPPE_NCE);
			this.groupBox1.Controls.Add(this.txtPtsNCE);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.chkNCE);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.nudNCE);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(62, 57);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(418, 145);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Allocations";
			// 
			// txtPPE_LO
			// 
			this.txtPPE_LO.Location = new System.Drawing.Point(315, 103);
			this.txtPPE_LO.Name = "txtPPE_LO";
			this.txtPPE_LO.ReadOnly = true;
			this.txtPPE_LO.Size = new System.Drawing.Size(31, 23);
			this.txtPPE_LO.TabIndex = 20;
			// 
			// txtPtsLO
			// 
			this.txtPtsLO.Location = new System.Drawing.Point(240, 103);
			this.txtPtsLO.Name = "txtPtsLO";
			this.txtPtsLO.ReadOnly = true;
			this.txtPtsLO.Size = new System.Drawing.Size(31, 23);
			this.txtPtsLO.TabIndex = 19;
			this.txtPtsLO.Text = "37.5";
			this.txtPtsLO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// chkLO
			// 
			this.chkLO.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkLO.Location = new System.Drawing.Point(6, 96);
			this.chkLO.Name = "chkLO";
			this.chkLO.Size = new System.Drawing.Size(143, 26);
			this.chkLO.TabIndex = 18;
			this.chkLO.Text = "SLO Errors";
			this.chkLO.UseVisualStyleBackColor = true;
			// 
			// nudLO
			// 
			this.nudLO.Location = new System.Drawing.Point(173, 103);
			this.nudLO.Name = "nudLO";
			this.nudLO.Size = new System.Drawing.Size(43, 23);
			this.nudLO.TabIndex = 17;
			this.nudLO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudLO.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
			// 
			// txtPPE_EE
			// 
			this.txtPPE_EE.Location = new System.Drawing.Point(315, 71);
			this.txtPPE_EE.Name = "txtPPE_EE";
			this.txtPPE_EE.ReadOnly = true;
			this.txtPPE_EE.Size = new System.Drawing.Size(31, 23);
			this.txtPPE_EE.TabIndex = 16;
			// 
			// txtPtsEE
			// 
			this.txtPtsEE.Location = new System.Drawing.Point(240, 71);
			this.txtPtsEE.Name = "txtPtsEE";
			this.txtPtsEE.ReadOnly = true;
			this.txtPtsEE.Size = new System.Drawing.Size(31, 23);
			this.txtPtsEE.TabIndex = 15;
			this.txtPtsEE.Text = "7.5";
			this.txtPtsEE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// chkEE
			// 
			this.chkEE.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkEE.Location = new System.Drawing.Point(6, 65);
			this.chkEE.Name = "chkEE";
			this.chkEE.Size = new System.Drawing.Size(143, 25);
			this.chkEE.TabIndex = 14;
			this.chkEE.Text = "Execution Errors";
			this.chkEE.UseVisualStyleBackColor = true;
			// 
			// nudEE
			// 
			this.nudEE.Location = new System.Drawing.Point(173, 71);
			this.nudEE.Name = "nudEE";
			this.nudEE.Size = new System.Drawing.Size(43, 23);
			this.nudEE.TabIndex = 13;
			this.nudEE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudEE.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(306, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 17);
			this.label5.TabIndex = 12;
			this.label5.Text = "Pts/Error";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(233, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 17);
			this.label4.TabIndex = 11;
			this.label4.Text = "Max Pts";
			// 
			// txtPPE_NCE
			// 
			this.txtPPE_NCE.Location = new System.Drawing.Point(315, 36);
			this.txtPPE_NCE.Name = "txtPPE_NCE";
			this.txtPPE_NCE.ReadOnly = true;
			this.txtPPE_NCE.Size = new System.Drawing.Size(31, 23);
			this.txtPPE_NCE.TabIndex = 10;
			// 
			// txtPtsNCE
			// 
			this.txtPtsNCE.Location = new System.Drawing.Point(240, 36);
			this.txtPtsNCE.Name = "txtPtsNCE";
			this.txtPtsNCE.ReadOnly = true;
			this.txtPtsNCE.Size = new System.Drawing.Size(31, 23);
			this.txtPtsNCE.TabIndex = 9;
			this.txtPtsNCE.Text = "5";
			this.txtPtsNCE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(177, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Pct %";
			// 
			// chkNCE
			// 
			this.chkNCE.AutoSize = true;
			this.chkNCE.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkNCE.Location = new System.Drawing.Point(6, 38);
			this.chkNCE.Name = "chkNCE";
			this.chkNCE.Size = new System.Drawing.Size(143, 21);
			this.chkNCE.TabIndex = 7;
			this.chkNCE.Text = "Non-Critical Errors";
			this.chkNCE.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(123, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Lock";
			// 
			// nudNCE
			// 
			this.nudNCE.Location = new System.Drawing.Point(173, 36);
			this.nudNCE.Name = "nudNCE";
			this.nudNCE.Size = new System.Drawing.Size(43, 23);
			this.nudNCE.TabIndex = 5;
			this.nudNCE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudNCE.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// txtDefaultProjectPoints
			// 
			this.txtDefaultProjectPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDefaultProjectPoints.Location = new System.Drawing.Point(189, 31);
			this.txtDefaultProjectPoints.Name = "txtDefaultProjectPoints";
			this.txtDefaultProjectPoints.Size = new System.Drawing.Size(57, 23);
			this.txtDefaultProjectPoints.TabIndex = 1;
			this.txtDefaultProjectPoints.Text = "50";
			this.txtDefaultProjectPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(69, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Default TotalPts";
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.tabsPreferences);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.btnOK);
			this.splitContainer3.Panel2.Controls.Add(this.btnCancel);
			this.splitContainer3.Panel2MinSize = 40;
			this.splitContainer3.Size = new System.Drawing.Size(931, 707);
			this.splitContainer3.SplitterDistance = 663;
			this.splitContainer3.TabIndex = 4;
			// 
			// nudThresholdPct
			// 
			this.nudThresholdPct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nudThresholdPct.Location = new System.Drawing.Point(503, 31);
			this.nudThresholdPct.Name = "nudThresholdPct";
			this.nudThresholdPct.Size = new System.Drawing.Size(38, 23);
			this.nudThresholdPct.TabIndex = 14;
			this.nudThresholdPct.Tag = "ThresholdPct";
			// 
			// frmPreferences
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(931, 707);
			this.Controls.Add(this.splitContainer3);
			this.Name = "frmPreferences";
			this.ShowInTaskbar = false;
			this.Text = "z-Preferences";
			this.tabsPreferences.ResumeLayout(false);
			this.tabBasicPreferences.ResumeLayout(false);
			this.tabBasicPreferences.PerformLayout();
			this.grpMultipliers.ResumeLayout(false);
			this.grpSkillLevels.ResumeLayout(false);
			this.grpSkillLevels.PerformLayout();
			this.grpDeductions.ResumeLayout(false);
			this.grpDeductions.PerformLayout();
			this.grpDifficulties.ResumeLayout(false);
			this.grpDifficulties.PerformLayout();
			this.grpRounding.ResumeLayout(false);
			this.grpRounding.PerformLayout();
			this.tabContentPreferences.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.grpDefaultScenario.ResumeLayout(false);
			this.grpDefaultScenario.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.grpRemediation.ResumeLayout(false);
			this.grpRemediation.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).EndInit();
			this.tabProjectPreferences.ResumeLayout(false);
			this.tabProjectPreferences.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLO)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudEE)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudNCE)).EndInit();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nudThresholdPct)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabsPreferences;
        private System.Windows.Forms.TabPage tabBasicPreferences;
        private System.Windows.Forms.TabPage tabContentPreferences;
        private System.Windows.Forms.GroupBox grpRounding;
        private System.Windows.Forms.RadioButton radRounding50;
        private System.Windows.Forms.RadioButton radRounding25;
        private System.Windows.Forms.RadioButton radRounding10;
        private System.Windows.Forms.RadioButton radRoundingNone;
        private System.Windows.Forms.CheckBox chkPartialCredit;
        private System.Windows.Forms.TabPage tabFormattingPreferences;
        private System.Windows.Forms.TabPage tabProjectPreferences;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDefaultProjectPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkNCE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudNCE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPPE_NCE;
        private System.Windows.Forms.TextBox txtPtsNCE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPPE_LO;
        private System.Windows.Forms.TextBox txtPtsLO;
        private System.Windows.Forms.CheckBox chkLO;
        private System.Windows.Forms.NumericUpDown nudLO;
        private System.Windows.Forms.TextBox txtPPE_EE;
        private System.Windows.Forms.TextBox txtPtsEE;
        private System.Windows.Forms.CheckBox chkEE;
		private System.Windows.Forms.NumericUpDown nudEE;
		private System.Windows.Forms.GroupBox grpDifficulties;
		private System.Windows.Forms.MaskedTextBox txtDifficultyChallenge;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.MaskedTextBox txtDifficultyHard;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.MaskedTextBox txtDifficultyNormal;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.MaskedTextBox txtDifficultyEasy;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox chkDifficulties;
		private System.Windows.Forms.GroupBox grpMultipliers;
		private System.Windows.Forms.GroupBox grpSkillLevels;
		private System.Windows.Forms.MaskedTextBox txtSkillLevelMultiplierExpert;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.MaskedTextBox txtSkillLevelMultiplierAdvanced;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.MaskedTextBox txtSkillLevelMultiplierIntermediate;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.MaskedTextBox txtSkillLevelMultiplierBeginner;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.MaskedTextBox txtSkillLevelMultiplierNovice;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.CheckBox chkSkillLevels;
		private System.Windows.Forms.GroupBox grpDeductions;
		private System.Windows.Forms.MaskedTextBox txtDeductionPctFull;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.MaskedTextBox txtDeductionPctMajor;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.MaskedTextBox txtDeductionPctModerate;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.MaskedTextBox txtDeductionPctMinor;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.MaskedTextBox txtDeductionPctNone;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.CheckBox chkDeductions;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView tvContentDefaultSceenarios;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.RichTextBox txtNodeNotes;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox txtScenarioPath;
		private System.Windows.Forms.GroupBox grpDefaultScenario;
		private System.Windows.Forms.GroupBox grpRemediation;
		private System.Windows.Forms.RichTextBox txtNotes;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.ComboBox cboDeductionType;
		private System.Windows.Forms.NumericUpDown nudThreshold;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.CheckBox chkScenarioEnabled;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.RichTextBox txtFeedback;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.RichTextBox richTextBox2;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.ComboBox cboAllocationCategory;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.NumericUpDown nudThresholdPct;
    }
}