namespace SBC_2D.Views.Forms
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxErrorMessage = new System.Windows.Forms.ListBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonAutoRun = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBoxMessage = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.machineDiagramControl1 = new SBC_2D.Views.UserControls.MachineDiagramControl();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBoxErrorMessage);
            this.groupBox3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBox3.Location = new System.Drawing.Point(16, 337);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(462, 208);
            this.groupBox3.TabIndex = 11027;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "錯誤訊息";
            // 
            // listBoxErrorMessage
            // 
            this.listBoxErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxErrorMessage.FormattingEnabled = true;
            this.listBoxErrorMessage.HorizontalScrollbar = true;
            this.listBoxErrorMessage.ItemHeight = 12;
            this.listBoxErrorMessage.Location = new System.Drawing.Point(3, 18);
            this.listBoxErrorMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxErrorMessage.Name = "listBoxErrorMessage";
            this.listBoxErrorMessage.ScrollAlwaysVisible = true;
            this.listBoxErrorMessage.Size = new System.Drawing.Size(456, 187);
            this.listBoxErrorMessage.TabIndex = 10967;
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("新細明體", 12F);
            this.buttonStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonStop.Location = new System.Drawing.Point(283, 13);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(195, 100);
            this.buttonStop.TabIndex = 11022;
            this.buttonStop.Text = "結束自動";
            this.buttonStop.UseVisualStyleBackColor = true;
            // 
            // buttonAutoRun
            // 
            this.buttonAutoRun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonAutoRun.Font = new System.Drawing.Font("新細明體", 12F);
            this.buttonAutoRun.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAutoRun.Location = new System.Drawing.Point(13, 13);
            this.buttonAutoRun.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAutoRun.Name = "buttonAutoRun";
            this.buttonAutoRun.Size = new System.Drawing.Size(195, 100);
            this.buttonAutoRun.TabIndex = 11023;
            this.buttonAutoRun.Text = "開始自動";
            this.buttonAutoRun.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBoxMessage);
            this.groupBox4.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(16, 121);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(462, 208);
            this.groupBox4.TabIndex = 11028;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "當前訊息";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // listBoxMessage
            // 
            this.listBoxMessage.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.listBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMessage.FormattingEnabled = true;
            this.listBoxMessage.HorizontalScrollbar = true;
            this.listBoxMessage.ItemHeight = 12;
            this.listBoxMessage.Location = new System.Drawing.Point(3, 18);
            this.listBoxMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxMessage.Name = "listBoxMessage";
            this.listBoxMessage.ScrollAlwaysVisible = true;
            this.listBoxMessage.Size = new System.Drawing.Size(456, 187);
            this.listBoxMessage.TabIndex = 10968;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(496, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 11040;
            this.label5.Text = "已傳送 ⟳";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(576, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 16);
            this.label6.TabIndex = 11039;
            this.label6.Text = "0";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // machineDiagramControl1
            // 
            this.machineDiagramControl1.AutoSize = true;
            this.machineDiagramControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.machineDiagramControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.machineDiagramControl1.Location = new System.Drawing.Point(499, 36);
            this.machineDiagramControl1.Name = "machineDiagramControl1";
            this.machineDiagramControl1.Size = new System.Drawing.Size(523, 506);
            this.machineDiagramControl1.TabIndex = 11029;
            this.machineDiagramControl1.Load += new System.EventHandler(this.machineDiagramControl1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 816);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonAutoRun);
            this.Controls.Add(this.machineDiagramControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxErrorMessage;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonAutoRun;
        private UserControls.MachineDiagramControl machineDiagramControl1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listBoxMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}