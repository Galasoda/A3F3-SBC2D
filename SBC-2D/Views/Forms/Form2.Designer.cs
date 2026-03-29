using SBC_2D.Shared;
using System;

namespace SBC_2D.Views
{
    partial class Form2
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
            this.checkBoxLdsBypass = new System.Windows.Forms.CheckBox();
            this.checkBoxLowerBrBypass = new System.Windows.Forms.CheckBox();
            this.checkBoxUpperBrBypass = new System.Windows.Forms.CheckBox();
            this.checkBoxMapModeBypass = new System.Windows.Forms.CheckBox();
            this.groupBoxBypassSetting = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label32312 = new System.Windows.Forms.Label();
            this.labelPcbMaxThickness = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBoxThicknessSetting = new System.Windows.Forms.GroupBox();
            this.buttonOpenZeroingForm = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonGetPcbThickness = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBoxPcbThickness = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.textBoxThicknessTolrence = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxRecipes = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanelRecipeManageCtrls = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSwitchEditOrView = new System.Windows.Forms.Button();
            this.buttonCanSelectRecipe = new System.Windows.Forms.Button();
            this.buttonCanSaveRecipe = new System.Windows.Forms.Button();
            this.buttonCanSaveasRecipe = new System.Windows.Forms.Button();
            this.buttonCanDeleteRecipe = new System.Windows.Forms.Button();
            this.flowLayoutPanelConfirmCancel = new System.Windows.Forms.FlowLayoutPanel();
            this.labelRecipeMamageAction = new System.Windows.Forms.Label();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxRecipeManager = new System.Windows.Forms.GroupBox();
            this.panelSelectRecipe = new System.Windows.Forms.Panel();
            this.labelRecipeName = new System.Windows.Forms.Label();
            this.textBoxSubstrateBlockY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSubstrateBlockX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxRotate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSubstrateBlockNumY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSubstrateBlockNumX = new System.Windows.Forms.TextBox();
            this.groupBoxSubstrateSetting = new System.Windows.Forms.GroupBox();
            this.groupBoxBypassSetting.SuspendLayout();
            this.groupBoxThicknessSetting.SuspendLayout();
            this.panel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.flowLayoutPanelRecipeManageCtrls.SuspendLayout();
            this.flowLayoutPanelConfirmCancel.SuspendLayout();
            this.groupBoxRecipeManager.SuspendLayout();
            this.panelSelectRecipe.SuspendLayout();
            this.groupBoxSubstrateSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxLdsBypass
            // 
            this.checkBoxLdsBypass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxLdsBypass.AutoSize = true;
            this.checkBoxLdsBypass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxLdsBypass.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxLdsBypass.Location = new System.Drawing.Point(22, 107);
            this.checkBoxLdsBypass.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.checkBoxLdsBypass.Name = "checkBoxLdsBypass";
            this.checkBoxLdsBypass.Size = new System.Drawing.Size(124, 21);
            this.checkBoxLdsBypass.TabIndex = 7;
            this.checkBoxLdsBypass.Text = "Bypass 雷射測厚";
            this.checkBoxLdsBypass.UseVisualStyleBackColor = true;
            this.checkBoxLdsBypass.Click += new System.EventHandler(this.CheckBoxLdsBypass_CheckedChanged);
            // 
            // checkBoxLowerBrBypass
            // 
            this.checkBoxLowerBrBypass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxLowerBrBypass.AutoSize = true;
            this.checkBoxLowerBrBypass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxLowerBrBypass.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxLowerBrBypass.Location = new System.Drawing.Point(22, 80);
            this.checkBoxLowerBrBypass.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.checkBoxLowerBrBypass.Name = "checkBoxLowerBrBypass";
            this.checkBoxLowerBrBypass.Size = new System.Drawing.Size(124, 21);
            this.checkBoxLowerBrBypass.TabIndex = 5;
            this.checkBoxLowerBrBypass.Text = "Bypass 下讀碼器";
            this.checkBoxLowerBrBypass.UseVisualStyleBackColor = true;
            this.checkBoxLowerBrBypass.Click += new System.EventHandler(this.CheckBoxLowerBrBypass_CheckedChanged);
            // 
            // checkBoxUpperBrBypass
            // 
            this.checkBoxUpperBrBypass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxUpperBrBypass.AutoSize = true;
            this.checkBoxUpperBrBypass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxUpperBrBypass.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxUpperBrBypass.Location = new System.Drawing.Point(22, 53);
            this.checkBoxUpperBrBypass.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.checkBoxUpperBrBypass.Name = "checkBoxUpperBrBypass";
            this.checkBoxUpperBrBypass.Size = new System.Drawing.Size(124, 21);
            this.checkBoxUpperBrBypass.TabIndex = 4;
            this.checkBoxUpperBrBypass.Text = "Bypass 上讀碼器";
            this.checkBoxUpperBrBypass.UseVisualStyleBackColor = true;
            this.checkBoxUpperBrBypass.Click += new System.EventHandler(this.CheckBoxUpperBrBypass_CheckedChanged);
            // 
            // checkBoxMapModeBypass
            // 
            this.checkBoxMapModeBypass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxMapModeBypass.AutoSize = true;
            this.checkBoxMapModeBypass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxMapModeBypass.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxMapModeBypass.Location = new System.Drawing.Point(22, 26);
            this.checkBoxMapModeBypass.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.checkBoxMapModeBypass.Name = "checkBoxMapModeBypass";
            this.checkBoxMapModeBypass.Size = new System.Drawing.Size(150, 21);
            this.checkBoxMapModeBypass.TabIndex = 6;
            this.checkBoxMapModeBypass.Text = "Bypass 讀碼對應模式";
            this.checkBoxMapModeBypass.UseVisualStyleBackColor = true;
            this.checkBoxMapModeBypass.Click += new System.EventHandler(this.CheckBoxMapModeBypass_CheckedChanged);
            // 
            // groupBoxBypassSetting
            // 
            this.groupBoxBypassSetting.Controls.Add(this.checkBoxMapModeBypass);
            this.groupBoxBypassSetting.Controls.Add(this.checkBoxUpperBrBypass);
            this.groupBoxBypassSetting.Controls.Add(this.checkBoxLowerBrBypass);
            this.groupBoxBypassSetting.Controls.Add(this.checkBoxLdsBypass);
            this.groupBoxBypassSetting.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.groupBoxBypassSetting.Location = new System.Drawing.Point(2, 124);
            this.groupBoxBypassSetting.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxBypassSetting.Name = "groupBoxBypassSetting";
            this.groupBoxBypassSetting.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxBypassSetting.Size = new System.Drawing.Size(383, 136);
            this.groupBoxBypassSetting.TabIndex = 11035;
            this.groupBoxBypassSetting.TabStop = false;
            this.groupBoxBypassSetting.Text = "取消 (Bypass)";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 10.2F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(4, 4);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 18);
            this.label6.TabIndex = 11021;
            this.label6.Text = "允收板厚上限:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32312
            // 
            this.label32312.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label32312.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label32312.Location = new System.Drawing.Point(183, 42);
            this.label32312.Margin = new System.Windows.Forms.Padding(4);
            this.label32312.Name = "label32312";
            this.label32312.Size = new System.Drawing.Size(28, 28);
            this.label32312.TabIndex = 11000;
            this.label32312.Text = "um";
            this.label32312.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPcbMaxThickness
            // 
            this.labelPcbMaxThickness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPcbMaxThickness.AutoSize = true;
            this.labelPcbMaxThickness.Font = new System.Drawing.Font("微軟正黑體", 10.2F);
            this.labelPcbMaxThickness.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPcbMaxThickness.Location = new System.Drawing.Point(107, 4);
            this.labelPcbMaxThickness.Margin = new System.Windows.Forms.Padding(4);
            this.labelPcbMaxThickness.Name = "labelPcbMaxThickness";
            this.labelPcbMaxThickness.Size = new System.Drawing.Size(16, 18);
            this.labelPcbMaxThickness.TabIndex = 11020;
            this.labelPcbMaxThickness.Text = "0";
            this.labelPcbMaxThickness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(11, 12);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 17);
            this.label10.TabIndex = 11032;
            this.label10.Text = "1. 測量生產板厚";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(13, 14);
            this.label12.Margin = new System.Windows.Forms.Padding(4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 17);
            this.label12.TabIndex = 11033;
            this.label12.Text = "2. 設定正公差";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxThicknessSetting
            // 
            this.groupBoxThicknessSetting.Controls.Add(this.buttonOpenZeroingForm);
            this.groupBoxThicknessSetting.Controls.Add(this.panel4);
            this.groupBoxThicknessSetting.Controls.Add(this.panel2);
            this.groupBoxThicknessSetting.Controls.Add(this.panel3);
            this.groupBoxThicknessSetting.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBoxThicknessSetting.Location = new System.Drawing.Point(2, 268);
            this.groupBoxThicknessSetting.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxThicknessSetting.Name = "groupBoxThicknessSetting";
            this.groupBoxThicknessSetting.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxThicknessSetting.Size = new System.Drawing.Size(383, 272);
            this.groupBoxThicknessSetting.TabIndex = 11036;
            this.groupBoxThicknessSetting.TabStop = false;
            this.groupBoxThicknessSetting.Text = "測厚設定";
            // 
            // buttonOpenZeroingForm
            // 
            this.buttonOpenZeroingForm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOpenZeroingForm.Location = new System.Drawing.Point(14, 28);
            this.buttonOpenZeroingForm.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpenZeroingForm.Name = "buttonOpenZeroingForm";
            this.buttonOpenZeroingForm.Size = new System.Drawing.Size(104, 26);
            this.buttonOpenZeroingForm.TabIndex = 11046;
            this.buttonOpenZeroingForm.Text = "初始化";
            this.buttonOpenZeroingForm.UseVisualStyleBackColor = true;
            this.buttonOpenZeroingForm.Click += new System.EventHandler(this.ButtonOpenZeroingForm_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.flowLayoutPanel5);
            this.panel4.Location = new System.Drawing.Point(7, 219);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(269, 41);
            this.panel4.TabIndex = 11046;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.label6);
            this.flowLayoutPanel5.Controls.Add(this.labelPcbMaxThickness);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(7, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(200, 32);
            this.flowLayoutPanel5.TabIndex = 11046;
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.buttonGetPcbThickness);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.label32312);
            this.panel2.Location = new System.Drawing.Point(7, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 76);
            this.panel2.TabIndex = 11046;
            // 
            // buttonGetPcbThickness
            // 
            this.buttonGetPcbThickness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetPcbThickness.AutoSize = true;
            this.buttonGetPcbThickness.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonGetPcbThickness.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonGetPcbThickness.Location = new System.Drawing.Point(130, 7);
            this.buttonGetPcbThickness.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGetPcbThickness.Name = "buttonGetPcbThickness";
            this.buttonGetPcbThickness.Size = new System.Drawing.Size(44, 27);
            this.buttonGetPcbThickness.TabIndex = 10999;
            this.buttonGetPcbThickness.Text = "測量";
            this.buttonGetPcbThickness.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textBoxPcbThickness);
            this.panel5.Location = new System.Drawing.Point(14, 42);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(160, 28);
            this.panel5.TabIndex = 11048;
            // 
            // textBoxPcbThickness
            // 
            this.textBoxPcbThickness.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxPcbThickness.Location = new System.Drawing.Point(2, 2);
            this.textBoxPcbThickness.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPcbThickness.Name = "textBoxPcbThickness";
            this.textBoxPcbThickness.Size = new System.Drawing.Size(156, 23);
            this.textBoxPcbThickness.TabIndex = 11019;
            this.textBoxPcbThickness.WordWrap = false;
            this.textBoxPcbThickness.TextChanged += new System.EventHandler(this.TextBoxPcbThickness_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(7, 140);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(269, 76);
            this.panel3.TabIndex = 11046;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.textBoxThicknessTolrence);
            this.panel6.Location = new System.Drawing.Point(16, 41);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(160, 28);
            this.panel6.TabIndex = 11049;
            // 
            // textBoxThicknessTolrence
            // 
            this.textBoxThicknessTolrence.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxThicknessTolrence.Location = new System.Drawing.Point(2, 2);
            this.textBoxThicknessTolrence.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxThicknessTolrence.Name = "textBoxThicknessTolrence";
            this.textBoxThicknessTolrence.Size = new System.Drawing.Size(156, 23);
            this.textBoxThicknessTolrence.TabIndex = 11019;
            this.textBoxThicknessTolrence.WordWrap = false;
            this.textBoxThicknessTolrence.TextChanged += new System.EventHandler(this.TextBoxThicknessTolrence_TextChanged);
            this.textBoxThicknessTolrence.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPressNumber);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(185, 41);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 28);
            this.label7.TabIndex = 11001;
            this.label7.Text = "um";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxRecipes
            // 
            this.comboBoxRecipes.BackColor = System.Drawing.Color.White;
            this.comboBoxRecipes.DropDownHeight = 120;
            this.comboBoxRecipes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRecipes.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.comboBoxRecipes.FormattingEnabled = true;
            this.comboBoxRecipes.IntegralHeight = false;
            this.comboBoxRecipes.Location = new System.Drawing.Point(2, 5);
            this.comboBoxRecipes.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxRecipes.Name = "comboBoxRecipes";
            this.comboBoxRecipes.Size = new System.Drawing.Size(356, 25);
            this.comboBoxRecipes.TabIndex = 3;
            this.comboBoxRecipes.SelectedIndexChanged += new System.EventHandler(this.ComboBoxRecipes_SelectedIndexChanged);
            // 
            // flowLayoutPanelRecipeManageCtrls
            // 
            this.flowLayoutPanelRecipeManageCtrls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelRecipeManageCtrls.Controls.Add(this.buttonSwitchEditOrView);
            this.flowLayoutPanelRecipeManageCtrls.Controls.Add(this.buttonCanSelectRecipe);
            this.flowLayoutPanelRecipeManageCtrls.Controls.Add(this.buttonCanSaveRecipe);
            this.flowLayoutPanelRecipeManageCtrls.Controls.Add(this.buttonCanSaveasRecipe);
            this.flowLayoutPanelRecipeManageCtrls.Controls.Add(this.buttonCanDeleteRecipe);
            this.flowLayoutPanelRecipeManageCtrls.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.flowLayoutPanelRecipeManageCtrls.Location = new System.Drawing.Point(16, 19);
            this.flowLayoutPanelRecipeManageCtrls.Name = "flowLayoutPanelRecipeManageCtrls";
            this.flowLayoutPanelRecipeManageCtrls.Padding = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelRecipeManageCtrls.Size = new System.Drawing.Size(360, 48);
            this.flowLayoutPanelRecipeManageCtrls.TabIndex = 11039;
            this.flowLayoutPanelRecipeManageCtrls.WrapContents = false;
            // 
            // buttonSwitchEditOrView
            // 
            this.buttonSwitchEditOrView.Location = new System.Drawing.Point(5, 5);
            this.buttonSwitchEditOrView.Name = "buttonSwitchEditOrView";
            this.buttonSwitchEditOrView.Size = new System.Drawing.Size(36, 38);
            this.buttonSwitchEditOrView.TabIndex = 11047;
            this.buttonSwitchEditOrView.Text = "E";
            this.buttonSwitchEditOrView.UseVisualStyleBackColor = true;
            this.buttonSwitchEditOrView.Click += new System.EventHandler(this.ButtonToggleEditMode_Click);
            // 
            // buttonCanSelectRecipe
            // 
            this.buttonCanSelectRecipe.AutoSize = true;
            this.buttonCanSelectRecipe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCanSelectRecipe.Location = new System.Drawing.Point(47, 5);
            this.buttonCanSelectRecipe.Name = "buttonCanSelectRecipe";
            this.buttonCanSelectRecipe.Size = new System.Drawing.Size(72, 38);
            this.buttonCanSelectRecipe.TabIndex = 11033;
            this.buttonCanSelectRecipe.Text = "開啟";
            this.buttonCanSelectRecipe.UseVisualStyleBackColor = true;
            this.buttonCanSelectRecipe.Click += new System.EventHandler(this.ButtonRecipeActionRequest_Click);
            // 
            // buttonCanSaveRecipe
            // 
            this.buttonCanSaveRecipe.AutoSize = true;
            this.buttonCanSaveRecipe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCanSaveRecipe.Location = new System.Drawing.Point(125, 5);
            this.buttonCanSaveRecipe.Name = "buttonCanSaveRecipe";
            this.buttonCanSaveRecipe.Size = new System.Drawing.Size(72, 38);
            this.buttonCanSaveRecipe.TabIndex = 11035;
            this.buttonCanSaveRecipe.Text = "儲存";
            this.buttonCanSaveRecipe.UseVisualStyleBackColor = true;
            this.buttonCanSaveRecipe.Click += new System.EventHandler(this.ButtonRecipeActionRequest_Click);
            // 
            // buttonCanSaveasRecipe
            // 
            this.buttonCanSaveasRecipe.AutoSize = true;
            this.buttonCanSaveasRecipe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCanSaveasRecipe.Location = new System.Drawing.Point(203, 5);
            this.buttonCanSaveasRecipe.Name = "buttonCanSaveasRecipe";
            this.buttonCanSaveasRecipe.Size = new System.Drawing.Size(72, 38);
            this.buttonCanSaveasRecipe.TabIndex = 11034;
            this.buttonCanSaveasRecipe.Text = "另存";
            this.buttonCanSaveasRecipe.UseVisualStyleBackColor = true;
            this.buttonCanSaveasRecipe.Click += new System.EventHandler(this.ButtonRecipeActionRequest_Click);
            // 
            // buttonCanDeleteRecipe
            // 
            this.buttonCanDeleteRecipe.AutoSize = true;
            this.buttonCanDeleteRecipe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCanDeleteRecipe.Location = new System.Drawing.Point(281, 5);
            this.buttonCanDeleteRecipe.Name = "buttonCanDeleteRecipe";
            this.buttonCanDeleteRecipe.Size = new System.Drawing.Size(72, 38);
            this.buttonCanDeleteRecipe.TabIndex = 0;
            this.buttonCanDeleteRecipe.Text = "刪除";
            this.buttonCanDeleteRecipe.UseVisualStyleBackColor = true;
            this.buttonCanDeleteRecipe.Click += new System.EventHandler(this.ButtonRecipeActionRequest_Click);
            // 
            // flowLayoutPanelConfirmCancel
            // 
            this.flowLayoutPanelConfirmCancel.AutoSize = true;
            this.flowLayoutPanelConfirmCancel.Controls.Add(this.labelRecipeMamageAction);
            this.flowLayoutPanelConfirmCancel.Controls.Add(this.buttonConfirm);
            this.flowLayoutPanelConfirmCancel.Controls.Add(this.buttonCancel);
            this.flowLayoutPanelConfirmCancel.Location = new System.Drawing.Point(29, 19);
            this.flowLayoutPanelConfirmCancel.Name = "flowLayoutPanelConfirmCancel";
            this.flowLayoutPanelConfirmCancel.Padding = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelConfirmCancel.Size = new System.Drawing.Size(243, 48);
            this.flowLayoutPanelConfirmCancel.TabIndex = 11040;
            this.flowLayoutPanelConfirmCancel.WrapContents = false;
            // 
            // labelRecipeMamageAction
            // 
            this.labelRecipeMamageAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRecipeMamageAction.BackColor = System.Drawing.SystemColors.Control;
            this.labelRecipeMamageAction.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.labelRecipeMamageAction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelRecipeMamageAction.Location = new System.Drawing.Point(5, 5);
            this.labelRecipeMamageAction.Margin = new System.Windows.Forms.Padding(3);
            this.labelRecipeMamageAction.Name = "labelRecipeMamageAction";
            this.labelRecipeMamageAction.Size = new System.Drawing.Size(77, 38);
            this.labelRecipeMamageAction.TabIndex = 11035;
            this.labelRecipeMamageAction.Text = "text";
            this.labelRecipeMamageAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.AutoSize = true;
            this.buttonConfirm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonConfirm.Location = new System.Drawing.Point(88, 5);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(72, 38);
            this.buttonConfirm.TabIndex = 11036;
            this.buttonConfirm.Text = "確定";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.ButtonConfirmAction_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCancel.Location = new System.Drawing.Point(166, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 38);
            this.buttonCancel.TabIndex = 11041;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelAction_Click);
            // 
            // groupBoxRecipeManager
            // 
            this.groupBoxRecipeManager.Controls.Add(this.panelSelectRecipe);
            this.groupBoxRecipeManager.Controls.Add(this.flowLayoutPanelRecipeManageCtrls);
            this.groupBoxRecipeManager.Controls.Add(this.flowLayoutPanelConfirmCancel);
            this.groupBoxRecipeManager.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBoxRecipeManager.Location = new System.Drawing.Point(2, 2);
            this.groupBoxRecipeManager.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxRecipeManager.Name = "groupBoxRecipeManager";
            this.groupBoxRecipeManager.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxRecipeManager.Size = new System.Drawing.Size(383, 114);
            this.groupBoxRecipeManager.TabIndex = 11038;
            this.groupBoxRecipeManager.TabStop = false;
            this.groupBoxRecipeManager.Text = "配方管理";
            // 
            // panelSelectRecipe
            // 
            this.panelSelectRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelSelectRecipe.Controls.Add(this.labelRecipeName);
            this.panelSelectRecipe.Controls.Add(this.comboBoxRecipes);
            this.panelSelectRecipe.Location = new System.Drawing.Point(16, 73);
            this.panelSelectRecipe.Name = "panelSelectRecipe";
            this.panelSelectRecipe.Padding = new System.Windows.Forms.Padding(2);
            this.panelSelectRecipe.Size = new System.Drawing.Size(360, 34);
            this.panelSelectRecipe.TabIndex = 11046;
            // 
            // labelRecipeName
            // 
            this.labelRecipeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRecipeName.AutoSize = true;
            this.labelRecipeName.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelRecipeName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelRecipeName.Location = new System.Drawing.Point(6, 9);
            this.labelRecipeName.Margin = new System.Windows.Forms.Padding(4);
            this.labelRecipeName.Name = "labelRecipeName";
            this.labelRecipeName.Size = new System.Drawing.Size(0, 17);
            this.labelRecipeName.TabIndex = 11038;
            this.labelRecipeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSubstrateBlockY
            // 
            this.textBoxSubstrateBlockY.Font = new System.Drawing.Font("新細明體", 10.2F);
            this.textBoxSubstrateBlockY.Location = new System.Drawing.Point(249, 69);
            this.textBoxSubstrateBlockY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSubstrateBlockY.Name = "textBoxSubstrateBlockY";
            this.textBoxSubstrateBlockY.Size = new System.Drawing.Size(83, 24);
            this.textBoxSubstrateBlockY.TabIndex = 11008;
            this.textBoxSubstrateBlockY.TextChanged += new System.EventHandler(this.TextBoxSubstrateBlockY_TextChanged);
            this.textBoxSubstrateBlockY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPressNumber);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 10.2F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(226, 73);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 18);
            this.label5.TabIndex = 11010;
            this.label5.Text = "Y";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSubstrateBlockX
            // 
            this.textBoxSubstrateBlockX.Font = new System.Drawing.Font("新細明體", 10.2F);
            this.textBoxSubstrateBlockX.Location = new System.Drawing.Point(79, 69);
            this.textBoxSubstrateBlockX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSubstrateBlockX.Name = "textBoxSubstrateBlockX";
            this.textBoxSubstrateBlockX.Size = new System.Drawing.Size(83, 24);
            this.textBoxSubstrateBlockX.TabIndex = 11007;
            this.textBoxSubstrateBlockX.TextChanged += new System.EventHandler(this.TextBoxSubstrateBlockX_TextChanged);
            this.textBoxSubstrateBlockX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPressNumber);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 10.2F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(55, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 18);
            this.label2.TabIndex = 11009;
            this.label2.Text = "X";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxRotate
            // 
            this.checkBoxRotate.AutoSize = true;
            this.checkBoxRotate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxRotate.Location = new System.Drawing.Point(373, 51);
            this.checkBoxRotate.Name = "checkBoxRotate";
            this.checkBoxRotate.Size = new System.Drawing.Size(79, 21);
            this.checkBoxRotate.TabIndex = 11040;
            this.checkBoxRotate.Text = "置件鏡像";
            this.checkBoxRotate.UseVisualStyleBackColor = true;
            this.checkBoxRotate.CheckedChanged += new System.EventHandler(this.CheckBoxRotate_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 10.2F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(186, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 18);
            this.label4.TabIndex = 11044;
            this.label4.Text = "Block Y";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSubstrateBlockNumY
            // 
            this.textBoxSubstrateBlockNumY.Font = new System.Drawing.Font("新細明體", 10.2F);
            this.textBoxSubstrateBlockNumY.Location = new System.Drawing.Point(249, 30);
            this.textBoxSubstrateBlockNumY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSubstrateBlockNumY.Name = "textBoxSubstrateBlockNumY";
            this.textBoxSubstrateBlockNumY.Size = new System.Drawing.Size(83, 24);
            this.textBoxSubstrateBlockNumY.TabIndex = 11043;
            this.textBoxSubstrateBlockNumY.TextChanged += new System.EventHandler(this.TextBoxSubstrateBlockNumY_TextChanged);
            this.textBoxSubstrateBlockNumY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPressNumber);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 10.2F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(15, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 18);
            this.label3.TabIndex = 11042;
            this.label3.Text = "Block X";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSubstrateBlockNumX
            // 
            this.textBoxSubstrateBlockNumX.Font = new System.Drawing.Font("新細明體", 10.2F);
            this.textBoxSubstrateBlockNumX.Location = new System.Drawing.Point(79, 30);
            this.textBoxSubstrateBlockNumX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSubstrateBlockNumX.Name = "textBoxSubstrateBlockNumX";
            this.textBoxSubstrateBlockNumX.Size = new System.Drawing.Size(83, 24);
            this.textBoxSubstrateBlockNumX.TabIndex = 11041;
            this.textBoxSubstrateBlockNumX.TextChanged += new System.EventHandler(this.TextBoxSubstrateBlockNumX_TextChanged);
            this.textBoxSubstrateBlockNumX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPressNumber);
            // 
            // groupBoxSubstrateSetting
            // 
            this.groupBoxSubstrateSetting.Controls.Add(this.textBoxSubstrateBlockX);
            this.groupBoxSubstrateSetting.Controls.Add(this.checkBoxRotate);
            this.groupBoxSubstrateSetting.Controls.Add(this.label5);
            this.groupBoxSubstrateSetting.Controls.Add(this.label4);
            this.groupBoxSubstrateSetting.Controls.Add(this.textBoxSubstrateBlockY);
            this.groupBoxSubstrateSetting.Controls.Add(this.textBoxSubstrateBlockNumY);
            this.groupBoxSubstrateSetting.Controls.Add(this.label2);
            this.groupBoxSubstrateSetting.Controls.Add(this.label3);
            this.groupBoxSubstrateSetting.Controls.Add(this.textBoxSubstrateBlockNumX);
            this.groupBoxSubstrateSetting.Font = new System.Drawing.Font("微軟正黑體", 9.75F);
            this.groupBoxSubstrateSetting.Location = new System.Drawing.Point(393, 2);
            this.groupBoxSubstrateSetting.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxSubstrateSetting.Name = "groupBoxSubstrateSetting";
            this.groupBoxSubstrateSetting.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxSubstrateSetting.Size = new System.Drawing.Size(480, 114);
            this.groupBoxSubstrateSetting.TabIndex = 11045;
            this.groupBoxSubstrateSetting.TabStop = false;
            this.groupBoxSubstrateSetting.Text = "基板設定";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1034, 816);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxSubstrateSetting);
            this.Controls.Add(this.groupBoxRecipeManager);
            this.Controls.Add(this.groupBoxThicknessSetting);
            this.Controls.Add(this.groupBoxBypassSetting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.Text = "Form2";
            this.groupBoxBypassSetting.ResumeLayout(false);
            this.groupBoxBypassSetting.PerformLayout();
            this.groupBoxThicknessSetting.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.flowLayoutPanelRecipeManageCtrls.ResumeLayout(false);
            this.flowLayoutPanelRecipeManageCtrls.PerformLayout();
            this.flowLayoutPanelConfirmCancel.ResumeLayout(false);
            this.flowLayoutPanelConfirmCancel.PerformLayout();
            this.groupBoxRecipeManager.ResumeLayout(false);
            this.groupBoxRecipeManager.PerformLayout();
            this.panelSelectRecipe.ResumeLayout(false);
            this.panelSelectRecipe.PerformLayout();
            this.groupBoxSubstrateSetting.ResumeLayout(false);
            this.groupBoxSubstrateSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxLdsBypass;
        private System.Windows.Forms.CheckBox checkBoxLowerBrBypass;
        private System.Windows.Forms.CheckBox checkBoxUpperBrBypass;
        private System.Windows.Forms.CheckBox checkBoxMapModeBypass;
        private System.Windows.Forms.GroupBox groupBoxBypassSetting;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label32312;
        private System.Windows.Forms.Label labelPcbMaxThickness;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBoxThicknessSetting;
        private System.Windows.Forms.ComboBox comboBoxRecipes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRecipeManageCtrls;
        private System.Windows.Forms.Button buttonCanSelectRecipe;
        private System.Windows.Forms.Button buttonCanSaveRecipe;
        private System.Windows.Forms.Button buttonCanSaveasRecipe;
        private System.Windows.Forms.Button buttonCanDeleteRecipe;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelConfirmCancel;
        private System.Windows.Forms.Label labelRecipeMamageAction;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxRecipeManager;
        private System.Windows.Forms.TextBox textBoxSubstrateBlockY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSubstrateBlockX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxRotate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSubstrateBlockNumY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSubstrateBlockNumX;
        private System.Windows.Forms.GroupBox groupBoxSubstrateSetting;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.TextBox textBoxPcbThickness;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox textBoxThicknessTolrence;
        private System.Windows.Forms.Button buttonOpenZeroingForm;
        private System.Windows.Forms.Panel panelSelectRecipe;
        private System.Windows.Forms.Label labelRecipeName;
        private System.Windows.Forms.Button buttonGetPcbThickness;
        private System.Windows.Forms.Button buttonSwitchEditOrView;
    }
}