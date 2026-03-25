namespace SBC_2D.Views.Forms
{
    partial class FormZeroing
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
            this.panelZeroingThickness = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.textBoxThicknessZeroBias = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonSetZeroBias = new System.Windows.Forms.Button();
            this.buttonZeroing = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.panelZeroingThickness.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelZeroingThickness
            // 
            this.panelZeroingThickness.Controls.Add(this.panel7);
            this.panelZeroingThickness.Controls.Add(this.label9);
            this.panelZeroingThickness.Controls.Add(this.buttonSetZeroBias);
            this.panelZeroingThickness.Controls.Add(this.buttonZeroing);
            this.panelZeroingThickness.Controls.Add(this.label11);
            this.panelZeroingThickness.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.panelZeroingThickness.Location = new System.Drawing.Point(4, 4);
            this.panelZeroingThickness.Name = "panelZeroingThickness";
            this.panelZeroingThickness.Size = new System.Drawing.Size(543, 171);
            this.panelZeroingThickness.TabIndex = 11040;
            // 
            // panel7
            // 
            this.panel7.AutoSize = true;
            this.panel7.Controls.Add(this.textBoxThicknessZeroBias);
            this.panel7.Location = new System.Drawing.Point(151, 127);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(160, 28);
            this.panel7.TabIndex = 11050;
            // 
            // textBoxThicknessZeroBias
            // 
            this.textBoxThicknessZeroBias.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxThicknessZeroBias.Location = new System.Drawing.Point(2, 3);
            this.textBoxThicknessZeroBias.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxThicknessZeroBias.Name = "textBoxThicknessZeroBias";
            this.textBoxThicknessZeroBias.Size = new System.Drawing.Size(156, 23);
            this.textBoxThicknessZeroBias.TabIndex = 11019;
            this.textBoxThicknessZeroBias.WordWrap = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("新細明體", 12F);
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(20, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(450, 96);
            this.label9.TabIndex = 11031;
            this.label9.Text = "⚠ 歸零調校將會影響測厚的計算誤差\r\n     請依照下列指示操作: \r\n     1. 請將歸零板放在量測範圍之內\r\n     2. 設定歸零板實際厚度\r\n  " +
    "   3. 按下歸零按鈕\r\n     4.如果歸零正常，才進行測厚設定";
            // 
            // buttonSetZeroBias
            // 
            this.buttonSetZeroBias.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSetZeroBias.Location = new System.Drawing.Point(23, 127);
            this.buttonSetZeroBias.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSetZeroBias.Name = "buttonSetZeroBias";
            this.buttonSetZeroBias.Size = new System.Drawing.Size(121, 28);
            this.buttonSetZeroBias.TabIndex = 11026;
            this.buttonSetZeroBias.Text = "設定歸零板厚度";
            this.buttonSetZeroBias.UseVisualStyleBackColor = true;
            // 
            // buttonZeroing
            // 
            this.buttonZeroing.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonZeroing.Location = new System.Drawing.Point(356, 127);
            this.buttonZeroing.Margin = new System.Windows.Forms.Padding(4);
            this.buttonZeroing.Name = "buttonZeroing";
            this.buttonZeroing.Size = new System.Drawing.Size(74, 28);
            this.buttonZeroing.TabIndex = 11030;
            this.buttonZeroing.Text = "歸零";
            this.buttonZeroing.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 10.2F);
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(318, 127);
            this.label11.Margin = new System.Windows.Forms.Padding(4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 28);
            this.label11.TabIndex = 11024;
            this.label11.Text = "um";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormZeroing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 179);
            this.Controls.Add(this.panelZeroingThickness);
            this.Name = "FormZeroing";
            this.Text = "FormZeroing";
            this.panelZeroingThickness.ResumeLayout(false);
            this.panelZeroingThickness.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelZeroingThickness;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox textBoxThicknessZeroBias;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonSetZeroBias;
        private System.Windows.Forms.Button buttonZeroing;
        private System.Windows.Forms.Label label11;
    }
}