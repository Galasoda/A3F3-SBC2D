namespace SBC_2D.Views.Forms
{
    partial class FormZeroing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lbl1Index;
        private System.Windows.Forms.Label lbl1Text;
        private System.Windows.Forms.Label lbl2Index;
        private System.Windows.Forms.TextBox tbThicknessBias;
        private System.Windows.Forms.Label lbl3Index;
        private System.Windows.Forms.Button btnEnableZeroing;
        private System.Windows.Forms.Label lbl4Index;

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

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbl4Text = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnEnableZeroing = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbThicknessBias = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl1Text = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.lbl1Index = new System.Windows.Forms.Label();
            this.lbl2Index = new System.Windows.Forms.Label();
            this.lbl3Index = new System.Windows.Forms.Label();
            this.lbl4Index = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblWarning, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl1Index, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl2Index, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl3Index, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl4Index, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(575, 188);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbl4Text);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(32, 148);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(535, 32);
            this.panel4.TabIndex = 11;
            // 
            // lbl4Text
            // 
            this.lbl4Text.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl4Text.AutoSize = true;
            this.lbl4Text.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl4Text.Location = new System.Drawing.Point(3, 7);
            this.lbl4Text.Margin = new System.Windows.Forms.Padding(3);
            this.lbl4Text.Name = "lbl4Text";
            this.lbl4Text.Size = new System.Drawing.Size(231, 16);
            this.lbl4Text.TabIndex = 12;
            this.lbl4Text.Text = "如果歸零正常，才進行測厚設定";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnEnableZeroing);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(32, 113);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(535, 28);
            this.panel3.TabIndex = 10;
            // 
            // btnEnableZeroing
            // 
            this.btnEnableZeroing.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnEnableZeroing.AutoSize = true;
            this.btnEnableZeroing.Location = new System.Drawing.Point(130, 5);
            this.btnEnableZeroing.Name = "btnEnableZeroing";
            this.btnEnableZeroing.Size = new System.Drawing.Size(39, 22);
            this.btnEnableZeroing.TabIndex = 8;
            this.btnEnableZeroing.Text = "歸零";
            this.btnEnableZeroing.Click += new System.EventHandler(this.BtnEnableZeroing_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "按下歸零按鈕";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbThicknessBias);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(32, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(535, 28);
            this.panel2.TabIndex = 10;
            // 
            // tbThicknessBias
            // 
            this.tbThicknessBias.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbThicknessBias.Location = new System.Drawing.Point(184, 4);
            this.tbThicknessBias.Name = "tbThicknessBias";
            this.tbThicknessBias.Size = new System.Drawing.Size(100, 22);
            this.tbThicknessBias.TabIndex = 5;
            this.tbThicknessBias.TextChanged += new System.EventHandler(this.TbThickness_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "設定歸零板實際厚度 um";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl1Text);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(32, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 28);
            this.panel1.TabIndex = 9;
            // 
            // lbl1Text
            // 
            this.lbl1Text.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl1Text.AutoSize = true;
            this.lbl1Text.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl1Text.Location = new System.Drawing.Point(3, 5);
            this.lbl1Text.Margin = new System.Windows.Forms.Padding(3);
            this.lbl1Text.Name = "lbl1Text";
            this.lbl1Text.Size = new System.Drawing.Size(215, 16);
            this.lbl1Text.TabIndex = 2;
            this.lbl1Text.Text = "請將歸零板放在量測範圍之內";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "⚠";
            // 
            // lblWarning
            // 
            this.lblWarning.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblWarning.AutoSize = true;
            this.lblWarning.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblWarning.ForeColor = System.Drawing.Color.DarkRed;
            this.lblWarning.Location = new System.Drawing.Point(32, 14);
            this.lblWarning.Margin = new System.Windows.Forms.Padding(3);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(411, 16);
            this.lblWarning.TabIndex = 0;
            this.lblWarning.Text = "歸零調校將會影響測厚的計算誤差，請依照下列指示操作:";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl1Index
            // 
            this.lbl1Index.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl1Index.AutoSize = true;
            this.lbl1Index.Location = new System.Drawing.Point(11, 51);
            this.lbl1Index.Name = "lbl1Index";
            this.lbl1Index.Size = new System.Drawing.Size(14, 12);
            this.lbl1Index.TabIndex = 1;
            this.lbl1Index.Text = "1.";
            // 
            // lbl2Index
            // 
            this.lbl2Index.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl2Index.AutoSize = true;
            this.lbl2Index.Location = new System.Drawing.Point(11, 86);
            this.lbl2Index.Name = "lbl2Index";
            this.lbl2Index.Size = new System.Drawing.Size(14, 12);
            this.lbl2Index.TabIndex = 3;
            this.lbl2Index.Text = "2.";
            // 
            // lbl3Index
            // 
            this.lbl3Index.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl3Index.AutoSize = true;
            this.lbl3Index.Location = new System.Drawing.Point(11, 121);
            this.lbl3Index.Name = "lbl3Index";
            this.lbl3Index.Size = new System.Drawing.Size(14, 12);
            this.lbl3Index.TabIndex = 6;
            this.lbl3Index.Text = "3.";
            // 
            // lbl4Index
            // 
            this.lbl4Index.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl4Index.AutoSize = true;
            this.lbl4Index.Location = new System.Drawing.Point(11, 158);
            this.lbl4Index.Name = "lbl4Index";
            this.lbl4Index.Size = new System.Drawing.Size(14, 12);
            this.lbl4Index.TabIndex = 9;
            this.lbl4Index.Text = "4.";
            // 
            // FormZeroing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 323);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormZeroing";
            this.Text = "歸零設定";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormZeroing_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbl4Text;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
    }
}