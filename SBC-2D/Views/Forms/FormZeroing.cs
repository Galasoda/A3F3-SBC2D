using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SBC_2D.Views.Forms
{
    public partial class FormZeroing : Form, IZeroingView
    {
        public event EventHandler<string> ThicknessZeroBiasChanged;
        public event EventHandler ZeroingRequested;
        public event EventHandler ViewClosed;

        public FormZeroing()
        {
            InitializeComponent();
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            Padding = new Padding(6);
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.Dock = DockStyle.None;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                ctrl.Margin = new Padding(1);

                if (ctrl is Panel panel)
                {
                    panel.Dock = DockStyle.Fill;
                    var innerControls = panel.Controls.Cast<Control>()
                    .OrderBy(c => c.Left)
                    .ToArray();
                    int startX = panel.Padding.Left;
                    foreach (Control c in innerControls)
                    {
                        int startY = (panel.ClientSize.Height - c.Height) / 2;
                        c.Top = startY;
                        c.Left = startX + c.Margin.Left;
                        startX += c.Left + c.Width + c.Margin.Right;
                    }
                }
            }

            var size = tableLayoutPanel1.PreferredSize;

            ClientSize = new Size(
                size.Width + Padding.Left + Padding.Right,
                size.Height + Padding.Top + Padding.Bottom
            );

            var p = tableLayoutPanel1.Parent;
            tableLayoutPanel1.Left = (p.ClientSize.Width - tableLayoutPanel1.Width) / 2;
            tableLayoutPanel1.Top = (p.ClientSize.Height - tableLayoutPanel1.Height) / 2;
        }

        private void BtnEnableZeroing_Click(object sender, EventArgs e)
            => ZeroingRequested?.Invoke(this, e);

        private void TbThickness_TextChanged(object sender, EventArgs e)
            => ThicknessZeroBiasChanged?.Invoke(this, tbThicknessBias.Text);
       

        public void SetThicknessZeroBias(string bias)
            => tbThicknessBias.Text = bias;

        private void FormZeroing_FormClosed(object sender, FormClosedEventArgs e)
            => ViewClosed?.Invoke(this, e);
    }
}
