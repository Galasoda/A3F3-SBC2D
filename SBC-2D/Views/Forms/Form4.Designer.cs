namespace SBC_2D.Views.Forms
{
    partial class Form4
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
            this.tabControlLogs = new System.Windows.Forms.TabControl();
            this.tabPageErrorLog = new System.Windows.Forms.TabPage();
            this.listBoxErrors = new System.Windows.Forms.ListBox();
            this.tabPageSystemLog = new System.Windows.Forms.TabPage();
            this.listBoxHistory = new System.Windows.Forms.ListBox();
            this.tabPageBarcodeLog = new System.Windows.Forms.TabPage();
            this.listBoxBarcodes = new System.Windows.Forms.ListBox();
            this.tabPageCodeTrace = new System.Windows.Forms.TabPage();
            this.listBoxCodeTrace = new System.Windows.Forms.ListBox();
            this.tabPagePremission = new System.Windows.Forms.TabPage();
            this.groupBoxUserLogin = new System.Windows.Forms.GroupBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelNewPw = new System.Windows.Forms.Label();
            this.textBoxNewPw = new System.Windows.Forms.TextBox();
            this.labelId = new System.Windows.Forms.Label();
            this.textBoxPw = new System.Windows.Forms.TextBox();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.labelPw = new System.Windows.Forms.Label();
            this.richTextBoxLogedMessage = new System.Windows.Forms.RichTextBox();
            this.buttonOperator = new System.Windows.Forms.Button();
            this.buttonEngineer = new System.Windows.Forms.Button();
            this.buttonVendor = new System.Windows.Forms.Button();
            this.buttonChangePw = new System.Windows.Forms.Button();
            this.groupBoxLockMachineSetting = new System.Windows.Forms.GroupBox();
            this.buttonResetErrorCount = new System.Windows.Forms.Button();
            this.labelCumulativeErrorCount = new System.Windows.Forms.Label();
            this.labelConsecutiveErrirCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSetT2 = new System.Windows.Forms.Button();
            this.textBoxCumulativeError_IntervalTime = new System.Windows.Forms.TextBox();
            this.buttonSetT1 = new System.Windows.Forms.Button();
            this.textBoxConsecutiveError_IntervalTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxXmlSetting = new System.Windows.Forms.GroupBox();
            this.radioButtonPahtTypeB = new System.Windows.Forms.RadioButton();
            this.richTextBoxXmlPath = new System.Windows.Forms.RichTextBox();
            this.radioButtonPahtTypeA = new System.Windows.Forms.RadioButton();
            this.buttonFileExplorer = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControlLogs.SuspendLayout();
            this.tabPageErrorLog.SuspendLayout();
            this.tabPageSystemLog.SuspendLayout();
            this.tabPageBarcodeLog.SuspendLayout();
            this.tabPageCodeTrace.SuspendLayout();
            this.tabPagePremission.SuspendLayout();
            this.groupBoxUserLogin.SuspendLayout();
            this.groupBoxLockMachineSetting.SuspendLayout();
            this.groupBoxXmlSetting.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlLogs
            // 
            this.tabControlLogs.Controls.Add(this.tabPageErrorLog);
            this.tabControlLogs.Controls.Add(this.tabPageSystemLog);
            this.tabControlLogs.Controls.Add(this.tabPageBarcodeLog);
            this.tabControlLogs.Controls.Add(this.tabPageCodeTrace);
            this.tabControlLogs.Controls.Add(this.tabPagePremission);
            this.tabControlLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLogs.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.tabControlLogs.Location = new System.Drawing.Point(0, 0);
            this.tabControlLogs.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlLogs.Name = "tabControlLogs";
            this.tabControlLogs.SelectedIndex = 0;
            this.tabControlLogs.Size = new System.Drawing.Size(1034, 816);
            this.tabControlLogs.TabIndex = 3;
            // 
            // tabPageErrorLog
            // 
            this.tabPageErrorLog.Controls.Add(this.listBoxErrors);
            this.tabPageErrorLog.Location = new System.Drawing.Point(4, 23);
            this.tabPageErrorLog.Name = "tabPageErrorLog";
            this.tabPageErrorLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrorLog.Size = new System.Drawing.Size(1026, 789);
            this.tabPageErrorLog.TabIndex = 1;
            this.tabPageErrorLog.Text = "警報";
            this.tabPageErrorLog.UseVisualStyleBackColor = true;
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxErrors.Font = new System.Drawing.Font("新細明體", 12F);
            this.listBoxErrors.FormattingEnabled = true;
            this.listBoxErrors.ItemHeight = 16;
            this.listBoxErrors.Location = new System.Drawing.Point(3, 3);
            this.listBoxErrors.Name = "listBoxErrors";
            this.listBoxErrors.ScrollAlwaysVisible = true;
            this.listBoxErrors.Size = new System.Drawing.Size(1020, 783);
            this.listBoxErrors.TabIndex = 1;
            // 
            // tabPageSystemLog
            // 
            this.tabPageSystemLog.Controls.Add(this.listBoxHistory);
            this.tabPageSystemLog.Location = new System.Drawing.Point(4, 23);
            this.tabPageSystemLog.Name = "tabPageSystemLog";
            this.tabPageSystemLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSystemLog.Size = new System.Drawing.Size(1026, 789);
            this.tabPageSystemLog.TabIndex = 0;
            this.tabPageSystemLog.Text = "系統";
            this.tabPageSystemLog.UseVisualStyleBackColor = true;
            // 
            // listBoxHistory
            // 
            this.listBoxHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxHistory.Font = new System.Drawing.Font("新細明體", 12F);
            this.listBoxHistory.FormattingEnabled = true;
            this.listBoxHistory.ItemHeight = 16;
            this.listBoxHistory.Location = new System.Drawing.Point(3, 3);
            this.listBoxHistory.Name = "listBoxHistory";
            this.listBoxHistory.Size = new System.Drawing.Size(1020, 783);
            this.listBoxHistory.TabIndex = 0;
            // 
            // tabPageBarcodeLog
            // 
            this.tabPageBarcodeLog.Controls.Add(this.listBoxBarcodes);
            this.tabPageBarcodeLog.Location = new System.Drawing.Point(4, 23);
            this.tabPageBarcodeLog.Name = "tabPageBarcodeLog";
            this.tabPageBarcodeLog.Size = new System.Drawing.Size(1026, 789);
            this.tabPageBarcodeLog.TabIndex = 2;
            this.tabPageBarcodeLog.Text = "條碼";
            this.tabPageBarcodeLog.UseVisualStyleBackColor = true;
            // 
            // listBoxBarcodes
            // 
            this.listBoxBarcodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxBarcodes.Font = new System.Drawing.Font("新細明體", 12F);
            this.listBoxBarcodes.FormattingEnabled = true;
            this.listBoxBarcodes.ItemHeight = 16;
            this.listBoxBarcodes.Location = new System.Drawing.Point(0, 0);
            this.listBoxBarcodes.Name = "listBoxBarcodes";
            this.listBoxBarcodes.Size = new System.Drawing.Size(1026, 789);
            this.listBoxBarcodes.TabIndex = 1;
            // 
            // tabPageCodeTrace
            // 
            this.tabPageCodeTrace.Controls.Add(this.listBoxCodeTrace);
            this.tabPageCodeTrace.Location = new System.Drawing.Point(4, 23);
            this.tabPageCodeTrace.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageCodeTrace.Name = "tabPageCodeTrace";
            this.tabPageCodeTrace.Size = new System.Drawing.Size(1026, 789);
            this.tabPageCodeTrace.TabIndex = 4;
            this.tabPageCodeTrace.Text = "程式";
            this.tabPageCodeTrace.UseVisualStyleBackColor = true;
            // 
            // listBoxCodeTrace
            // 
            this.listBoxCodeTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCodeTrace.Font = new System.Drawing.Font("新細明體", 12F);
            this.listBoxCodeTrace.FormattingEnabled = true;
            this.listBoxCodeTrace.ItemHeight = 16;
            this.listBoxCodeTrace.Location = new System.Drawing.Point(0, 0);
            this.listBoxCodeTrace.Name = "listBoxCodeTrace";
            this.listBoxCodeTrace.Size = new System.Drawing.Size(1026, 789);
            this.listBoxCodeTrace.TabIndex = 2;
            // 
            // tabPagePremission
            // 
            this.tabPagePremission.Controls.Add(this.groupBoxUserLogin);
            this.tabPagePremission.Controls.Add(this.groupBoxLockMachineSetting);
            this.tabPagePremission.Controls.Add(this.groupBoxXmlSetting);
            this.tabPagePremission.Location = new System.Drawing.Point(4, 23);
            this.tabPagePremission.Margin = new System.Windows.Forms.Padding(2);
            this.tabPagePremission.Name = "tabPagePremission";
            this.tabPagePremission.Size = new System.Drawing.Size(1026, 789);
            this.tabPagePremission.TabIndex = 3;
            this.tabPagePremission.Text = "權限";
            this.tabPagePremission.UseVisualStyleBackColor = true;
            // 
            // groupBoxUserLogin
            // 
            this.groupBoxUserLogin.AutoSize = true;
            this.groupBoxUserLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxUserLogin.Controls.Add(this.labelNewPw);
            this.groupBoxUserLogin.Controls.Add(this.textBoxNewPw);
            this.groupBoxUserLogin.Controls.Add(this.labelId);
            this.groupBoxUserLogin.Controls.Add(this.panel1);
            this.groupBoxUserLogin.Controls.Add(this.textBoxPw);
            this.groupBoxUserLogin.Controls.Add(this.textBoxId);
            this.groupBoxUserLogin.Controls.Add(this.labelPw);
            this.groupBoxUserLogin.Controls.Add(this.richTextBoxLogedMessage);
            this.groupBoxUserLogin.Controls.Add(this.buttonOperator);
            this.groupBoxUserLogin.Controls.Add(this.buttonEngineer);
            this.groupBoxUserLogin.Controls.Add(this.buttonVendor);
            this.groupBoxUserLogin.Controls.Add(this.buttonChangePw);
            this.groupBoxUserLogin.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.groupBoxUserLogin.Location = new System.Drawing.Point(8, 6);
            this.groupBoxUserLogin.Name = "groupBoxUserLogin";
            this.groupBoxUserLogin.Size = new System.Drawing.Size(345, 344);
            this.groupBoxUserLogin.TabIndex = 11011;
            this.groupBoxUserLogin.TabStop = false;
            this.groupBoxUserLogin.Text = "使用者";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLogin.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.buttonLogin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonLogin.Location = new System.Drawing.Point(0, 0);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(226, 41);
            this.buttonLogin.TabIndex = 10991;
            this.buttonLogin.Text = "確定";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // labelNewPw
            // 
            this.labelNewPw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNewPw.AutoSize = true;
            this.labelNewPw.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.labelNewPw.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelNewPw.Location = new System.Drawing.Point(4, 172);
            this.labelNewPw.Margin = new System.Windows.Forms.Padding(3);
            this.labelNewPw.Name = "labelNewPw";
            this.labelNewPw.Size = new System.Drawing.Size(47, 17);
            this.labelNewPw.TabIndex = 11014;
            this.labelNewPw.Text = "新密碼";
            this.labelNewPw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxNewPw
            // 
            this.textBoxNewPw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewPw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNewPw.Location = new System.Drawing.Point(57, 167);
            this.textBoxNewPw.Name = "textBoxNewPw";
            this.textBoxNewPw.PasswordChar = '*';
            this.textBoxNewPw.Size = new System.Drawing.Size(280, 25);
            this.textBoxNewPw.TabIndex = 11013;
            this.textBoxNewPw.TextChanged += new System.EventHandler(this.TextBoxNewPw_TextChanged);
            // 
            // labelId
            // 
            this.labelId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelId.AutoSize = true;
            this.labelId.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.labelId.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelId.Location = new System.Drawing.Point(17, 92);
            this.labelId.Margin = new System.Windows.Forms.Padding(3);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(34, 17);
            this.labelId.TabIndex = 10992;
            this.labelId.Text = "工號";
            this.labelId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPw
            // 
            this.textBoxPw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPw.Location = new System.Drawing.Point(57, 126);
            this.textBoxPw.Name = "textBoxPw";
            this.textBoxPw.PasswordChar = '*';
            this.textBoxPw.Size = new System.Drawing.Size(280, 25);
            this.textBoxPw.TabIndex = 10994;
            this.textBoxPw.TextChanged += new System.EventHandler(this.TextBoxPw_TextChanged);
            // 
            // textBoxId
            // 
            this.textBoxId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxId.Location = new System.Drawing.Point(57, 88);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.PasswordChar = '*';
            this.textBoxId.Size = new System.Drawing.Size(280, 25);
            this.textBoxId.TabIndex = 3;
            this.textBoxId.TextChanged += new System.EventHandler(this.TextBoxId_TextChanged);
            // 
            // labelPw
            // 
            this.labelPw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPw.AutoSize = true;
            this.labelPw.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.labelPw.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPw.Location = new System.Drawing.Point(17, 130);
            this.labelPw.Margin = new System.Windows.Forms.Padding(3);
            this.labelPw.Name = "labelPw";
            this.labelPw.Size = new System.Drawing.Size(34, 17);
            this.labelPw.TabIndex = 10995;
            this.labelPw.Text = "密碼";
            this.labelPw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBoxLogedMessage
            // 
            this.richTextBoxLogedMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLogedMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.richTextBoxLogedMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLogedMessage.Location = new System.Drawing.Point(8, 253);
            this.richTextBoxLogedMessage.Name = "richTextBoxLogedMessage";
            this.richTextBoxLogedMessage.ReadOnly = true;
            this.richTextBoxLogedMessage.Size = new System.Drawing.Size(331, 86);
            this.richTextBoxLogedMessage.TabIndex = 11008;
            this.richTextBoxLogedMessage.Text = "";
            // 
            // buttonOperator
            // 
            this.buttonOperator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOperator.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.buttonOperator.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOperator.Location = new System.Drawing.Point(8, 23);
            this.buttonOperator.Name = "buttonOperator";
            this.buttonOperator.Size = new System.Drawing.Size(97, 49);
            this.buttonOperator.TabIndex = 10993;
            this.buttonOperator.Tag = "";
            this.buttonOperator.Text = "作業員";
            this.buttonOperator.UseVisualStyleBackColor = true;
            this.buttonOperator.Click += new System.EventHandler(this.ButtonRole_Click);
            // 
            // buttonEngineer
            // 
            this.buttonEngineer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineer.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.buttonEngineer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonEngineer.Location = new System.Drawing.Point(125, 23);
            this.buttonEngineer.Name = "buttonEngineer";
            this.buttonEngineer.Size = new System.Drawing.Size(97, 49);
            this.buttonEngineer.TabIndex = 1;
            this.buttonEngineer.Tag = "";
            this.buttonEngineer.Text = "工程師";
            this.buttonEngineer.UseVisualStyleBackColor = true;
            this.buttonEngineer.Click += new System.EventHandler(this.ButtonRole_Click);
            // 
            // buttonVendor
            // 
            this.buttonVendor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVendor.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.buttonVendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonVendor.Location = new System.Drawing.Point(242, 24);
            this.buttonVendor.Name = "buttonVendor";
            this.buttonVendor.Size = new System.Drawing.Size(97, 49);
            this.buttonVendor.TabIndex = 2;
            this.buttonVendor.Tag = "";
            this.buttonVendor.Text = "原廠";
            this.buttonVendor.UseVisualStyleBackColor = true;
            this.buttonVendor.Click += new System.EventHandler(this.ButtonRole_Click);
            // 
            // buttonChangePw
            // 
            this.buttonChangePw.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.buttonChangePw.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonChangePw.Location = new System.Drawing.Point(8, 159);
            this.buttonChangePw.Name = "buttonChangePw";
            this.buttonChangePw.Size = new System.Drawing.Size(331, 40);
            this.buttonChangePw.TabIndex = 11012;
            this.buttonChangePw.Text = "修改密碼";
            this.buttonChangePw.UseVisualStyleBackColor = true;
            this.buttonChangePw.Click += new System.EventHandler(this.ButtonChangePw_Click);
            // 
            // groupBoxLockMachineSetting
            // 
            this.groupBoxLockMachineSetting.Controls.Add(this.buttonResetErrorCount);
            this.groupBoxLockMachineSetting.Controls.Add(this.labelCumulativeErrorCount);
            this.groupBoxLockMachineSetting.Controls.Add(this.labelConsecutiveErrirCount);
            this.groupBoxLockMachineSetting.Controls.Add(this.label8);
            this.groupBoxLockMachineSetting.Controls.Add(this.label4);
            this.groupBoxLockMachineSetting.Controls.Add(this.buttonSetT2);
            this.groupBoxLockMachineSetting.Controls.Add(this.textBoxCumulativeError_IntervalTime);
            this.groupBoxLockMachineSetting.Controls.Add(this.buttonSetT1);
            this.groupBoxLockMachineSetting.Controls.Add(this.textBoxConsecutiveError_IntervalTime);
            this.groupBoxLockMachineSetting.Controls.Add(this.label3);
            this.groupBoxLockMachineSetting.Controls.Add(this.label7);
            this.groupBoxLockMachineSetting.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.groupBoxLockMachineSetting.Location = new System.Drawing.Point(507, 193);
            this.groupBoxLockMachineSetting.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxLockMachineSetting.Name = "groupBoxLockMachineSetting";
            this.groupBoxLockMachineSetting.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxLockMachineSetting.Size = new System.Drawing.Size(355, 155);
            this.groupBoxLockMachineSetting.TabIndex = 11010;
            this.groupBoxLockMachineSetting.TabStop = false;
            this.groupBoxLockMachineSetting.Text = "鎖機條件";
            // 
            // buttonResetErrorCount
            // 
            this.buttonResetErrorCount.AutoSize = true;
            this.buttonResetErrorCount.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.buttonResetErrorCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonResetErrorCount.Location = new System.Drawing.Point(295, 24);
            this.buttonResetErrorCount.Margin = new System.Windows.Forms.Padding(4);
            this.buttonResetErrorCount.Name = "buttonResetErrorCount";
            this.buttonResetErrorCount.Size = new System.Drawing.Size(54, 118);
            this.buttonResetErrorCount.TabIndex = 11006;
            this.buttonResetErrorCount.Text = "重置";
            this.buttonResetErrorCount.UseVisualStyleBackColor = true;
            // 
            // labelCumulativeErrorCount
            // 
            this.labelCumulativeErrorCount.AutoSize = true;
            this.labelCumulativeErrorCount.Font = new System.Drawing.Font("新細明體", 12F);
            this.labelCumulativeErrorCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCumulativeErrorCount.Location = new System.Drawing.Point(235, 111);
            this.labelCumulativeErrorCount.Name = "labelCumulativeErrorCount";
            this.labelCumulativeErrorCount.Size = new System.Drawing.Size(45, 16);
            this.labelCumulativeErrorCount.TabIndex = 11005;
            this.labelCumulativeErrorCount.Text = "label2";
            // 
            // labelConsecutiveErrirCount
            // 
            this.labelConsecutiveErrirCount.AutoSize = true;
            this.labelConsecutiveErrirCount.Font = new System.Drawing.Font("新細明體", 12F);
            this.labelConsecutiveErrirCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelConsecutiveErrirCount.Location = new System.Drawing.Point(235, 43);
            this.labelConsecutiveErrirCount.Name = "labelConsecutiveErrirCount";
            this.labelConsecutiveErrirCount.Size = new System.Drawing.Size(45, 16);
            this.labelConsecutiveErrirCount.TabIndex = 11004;
            this.labelConsecutiveErrirCount.Text = "label2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(88, 52);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 16);
            this.label8.TabIndex = 11003;
            this.label8.Text = "分鐘";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(88, 120);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 11002;
            this.label4.Text = "分鐘";
            // 
            // buttonSetT2
            // 
            this.buttonSetT2.AutoSize = true;
            this.buttonSetT2.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.buttonSetT2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSetT2.Location = new System.Drawing.Point(139, 110);
            this.buttonSetT2.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSetT2.Name = "buttonSetT2";
            this.buttonSetT2.Size = new System.Drawing.Size(76, 32);
            this.buttonSetT2.TabIndex = 11000;
            this.buttonSetT2.Text = "設定";
            this.buttonSetT2.UseVisualStyleBackColor = true;
            // 
            // textBoxCumulativeError_IntervalTime
            // 
            this.textBoxCumulativeError_IntervalTime.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.textBoxCumulativeError_IntervalTime.Location = new System.Drawing.Point(16, 116);
            this.textBoxCumulativeError_IntervalTime.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCumulativeError_IntervalTime.Name = "textBoxCumulativeError_IntervalTime";
            this.textBoxCumulativeError_IntervalTime.Size = new System.Drawing.Size(65, 25);
            this.textBoxCumulativeError_IntervalTime.TabIndex = 10999;
            // 
            // buttonSetT1
            // 
            this.buttonSetT1.AutoSize = true;
            this.buttonSetT1.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.buttonSetT1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSetT1.Location = new System.Drawing.Point(139, 43);
            this.buttonSetT1.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSetT1.Name = "buttonSetT1";
            this.buttonSetT1.Size = new System.Drawing.Size(76, 32);
            this.buttonSetT1.TabIndex = 10996;
            this.buttonSetT1.Text = "設定";
            this.buttonSetT1.UseVisualStyleBackColor = true;
            // 
            // textBoxConsecutiveError_IntervalTime
            // 
            this.textBoxConsecutiveError_IntervalTime.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.textBoxConsecutiveError_IntervalTime.Location = new System.Drawing.Point(16, 48);
            this.textBoxConsecutiveError_IntervalTime.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxConsecutiveError_IntervalTime.Name = "textBoxConsecutiveError_IntervalTime";
            this.textBoxConsecutiveError_IntervalTime.Size = new System.Drawing.Size(65, 25);
            this.textBoxConsecutiveError_IntervalTime.TabIndex = 10995;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(12, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 56);
            this.label3.TabIndex = 10998;
            this.label3.Text = "設定時間內發生3次相同錯誤";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(13, 91);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(215, 56);
            this.label7.TabIndex = 10994;
            this.label7.Text = "設定時間內發生3次不同錯誤";
            // 
            // groupBoxXmlSetting
            // 
            this.groupBoxXmlSetting.Controls.Add(this.radioButtonPahtTypeB);
            this.groupBoxXmlSetting.Controls.Add(this.richTextBoxXmlPath);
            this.groupBoxXmlSetting.Controls.Add(this.radioButtonPahtTypeA);
            this.groupBoxXmlSetting.Controls.Add(this.buttonFileExplorer);
            this.groupBoxXmlSetting.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.groupBoxXmlSetting.Location = new System.Drawing.Point(507, 6);
            this.groupBoxXmlSetting.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxXmlSetting.Name = "groupBoxXmlSetting";
            this.groupBoxXmlSetting.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxXmlSetting.Size = new System.Drawing.Size(510, 120);
            this.groupBoxXmlSetting.TabIndex = 11009;
            this.groupBoxXmlSetting.TabStop = false;
            this.groupBoxXmlSetting.Text = "XML路徑";
            // 
            // radioButtonPahtTypeB
            // 
            this.radioButtonPahtTypeB.AutoSize = true;
            this.radioButtonPahtTypeB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonPahtTypeB.Location = new System.Drawing.Point(355, 17);
            this.radioButtonPahtTypeB.Name = "radioButtonPahtTypeB";
            this.radioButtonPahtTypeB.Size = new System.Drawing.Size(66, 21);
            this.radioButtonPahtTypeB.TabIndex = 11038;
            this.radioButtonPahtTypeB.TabStop = true;
            this.radioButtonPahtTypeB.Text = "B Type";
            this.radioButtonPahtTypeB.UseVisualStyleBackColor = true;
            // 
            // richTextBoxXmlPath
            // 
            this.richTextBoxXmlPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxXmlPath.Location = new System.Drawing.Point(6, 44);
            this.richTextBoxXmlPath.Name = "richTextBoxXmlPath";
            this.richTextBoxXmlPath.ReadOnly = true;
            this.richTextBoxXmlPath.Size = new System.Drawing.Size(415, 69);
            this.richTextBoxXmlPath.TabIndex = 11010;
            this.richTextBoxXmlPath.Text = "";
            // 
            // radioButtonPahtTypeA
            // 
            this.radioButtonPahtTypeA.AutoSize = true;
            this.radioButtonPahtTypeA.Checked = true;
            this.radioButtonPahtTypeA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonPahtTypeA.Location = new System.Drawing.Point(282, 17);
            this.radioButtonPahtTypeA.Name = "radioButtonPahtTypeA";
            this.radioButtonPahtTypeA.Size = new System.Drawing.Size(67, 21);
            this.radioButtonPahtTypeA.TabIndex = 11037;
            this.radioButtonPahtTypeA.TabStop = true;
            this.radioButtonPahtTypeA.Text = "A Type";
            this.radioButtonPahtTypeA.UseVisualStyleBackColor = true;
            // 
            // buttonFileExplorer
            // 
            this.buttonFileExplorer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonFileExplorer.Location = new System.Drawing.Point(428, 44);
            this.buttonFileExplorer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFileExplorer.Name = "buttonFileExplorer";
            this.buttonFileExplorer.Size = new System.Drawing.Size(67, 69);
            this.buttonFileExplorer.TabIndex = 10984;
            this.buttonFileExplorer.Text = "更換";
            this.buttonFileExplorer.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Location = new System.Drawing.Point(8, 205);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 41);
            this.panel1.TabIndex = 11012;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonCancel.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCancel.Location = new System.Drawing.Point(226, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(105, 41);
            this.buttonCancel.TabIndex = 11013;
            this.buttonCancel.Text = "取消 ";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 816);
            this.Controls.Add(this.tabControlLogs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form4";
            this.Text = "Form4";
            this.tabControlLogs.ResumeLayout(false);
            this.tabPageErrorLog.ResumeLayout(false);
            this.tabPageSystemLog.ResumeLayout(false);
            this.tabPageBarcodeLog.ResumeLayout(false);
            this.tabPageCodeTrace.ResumeLayout(false);
            this.tabPagePremission.ResumeLayout(false);
            this.tabPagePremission.PerformLayout();
            this.groupBoxUserLogin.ResumeLayout(false);
            this.groupBoxUserLogin.PerformLayout();
            this.groupBoxLockMachineSetting.ResumeLayout(false);
            this.groupBoxLockMachineSetting.PerformLayout();
            this.groupBoxXmlSetting.ResumeLayout(false);
            this.groupBoxXmlSetting.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlLogs;
        private System.Windows.Forms.TabPage tabPageErrorLog;
        private System.Windows.Forms.ListBox listBoxErrors;
        private System.Windows.Forms.TabPage tabPageSystemLog;
        private System.Windows.Forms.ListBox listBoxHistory;
        private System.Windows.Forms.TabPage tabPageBarcodeLog;
        private System.Windows.Forms.ListBox listBoxBarcodes;
        private System.Windows.Forms.TabPage tabPageCodeTrace;
        private System.Windows.Forms.ListBox listBoxCodeTrace;
        private System.Windows.Forms.TabPage tabPagePremission;
        private System.Windows.Forms.GroupBox groupBoxLockMachineSetting;
        private System.Windows.Forms.Button buttonResetErrorCount;
        private System.Windows.Forms.Label labelCumulativeErrorCount;
        private System.Windows.Forms.Label labelConsecutiveErrirCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSetT2;
        private System.Windows.Forms.TextBox textBoxCumulativeError_IntervalTime;
        private System.Windows.Forms.Button buttonSetT1;
        private System.Windows.Forms.TextBox textBoxConsecutiveError_IntervalTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBoxUserLogin;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox textBoxPw;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label labelPw;
        private System.Windows.Forms.RichTextBox richTextBoxLogedMessage;
        private System.Windows.Forms.Button buttonOperator;
        private System.Windows.Forms.Button buttonEngineer;
        private System.Windows.Forms.Button buttonVendor;
        private System.Windows.Forms.GroupBox groupBoxXmlSetting;
        private System.Windows.Forms.RadioButton radioButtonPahtTypeB;
        private System.Windows.Forms.RichTextBox richTextBoxXmlPath;
        private System.Windows.Forms.RadioButton radioButtonPahtTypeA;
        private System.Windows.Forms.Button buttonFileExplorer;
        private System.Windows.Forms.Label labelNewPw;
        private System.Windows.Forms.TextBox textBoxNewPw;
        private System.Windows.Forms.Button buttonChangePw;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCancel;
    }
}