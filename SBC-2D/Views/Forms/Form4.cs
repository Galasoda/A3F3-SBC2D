using SBC_2D.Shared;
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
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Views.Forms
{
    public partial class Form4 : Form, IUserLoginView, IXmlDirSelectorView
    {
        public event EventHandler UserLoginRequested;
        public event EventHandler ChangePwRequested;
        public event EventHandler CancelChangePwRequested;
        public event EventHandler<Role> RoleChanged;
        public event EventHandler<string> IdChanged;
        public event EventHandler<string> PwChanged;
        public event EventHandler<string> NewPwChanged;
        public event EventHandler InitializeRequested;
        public event EventHandler ChangeDirRequested;
        public event EventHandler<string> InsertTypeChanged;

        public Form4()
        {
            InitializeComponent();
            buttonOperator.Tag = Role.Operater;
            buttonEngineer.Tag = Role.Engineer;
            buttonVendor.Tag = Role.Vendor;
        }

        private void ButtonRole_Click(object sender, EventArgs args)
        {
            if (!(sender is Button btn))
                return;
            if (!(btn.Tag is Role role))
                return;
            RoleChanged?.Invoke(this, role);
        }

        public void HighlightRoleView(Role role)
        {
            foreach (var btn in groupBoxUserLogin.Controls.OfType<Button>())
            {
                if (btn.Tag is Role r && r == role)
                    btn.BackColor = Color.LimeGreen;
                else
                    btn.BackColor = SystemColors.Control;
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            InitializeRequested?.Invoke(this, e);
        }

        public void ShowLogedMessage(string message)
            => richTextBoxLogedMessage.Text = message;

        private void TextBoxId_TextChanged(object sender, EventArgs e)
            => IdChanged?.Invoke(this, textBoxId.Text);

        private void TextBoxPw_TextChanged(object sender, EventArgs e)
            => PwChanged?.Invoke(this, textBoxPw.Text);

        private void TextBoxNewPw_TextChanged(object sender, EventArgs e)
            => NewPwChanged?.Invoke(this, textBoxNewPw.Text);

        private void ButtonLogin_Click(object sender, EventArgs e)
            => UserLoginRequested?.Invoke(this, e);

        private void ButtonChangePw_Click(object sender, EventArgs e)
            => ChangePwRequested?.Invoke(this, e);

        private void ButtonCancel_Click(object sender, EventArgs e)
            => CancelChangePwRequested?.Invoke(this, e);

        private void ButtonFileExplorer_Click(object sender, EventArgs e)
            => ChangeDirRequested?.Invoke(this, e);

        private void RadioButtonPahtTypeA_CheckedChanged(object sender, EventArgs e)
            => InsertTypeChanged?.Invoke(this, "A");
        private void RadioButtonPahtTypeB_CheckedChanged(object sender, EventArgs e)
            => InsertTypeChanged?.Invoke(this, "B");

        public void ClearEnterInfos()
        {
            textBoxId.Text = string.Empty;
            textBoxPw.Text = string.Empty;
            textBoxNewPw.Text = string.Empty;
        }

        public void SetChangePwMode(bool isChangePw)
        {
            ClearEnterInfos();
            if (isChangePw)
            {
                textBoxNewPw.Visible = true;
                textBoxNewPw.BringToFront();
                labelNewPw.Visible = true;
                labelNewPw.BringToFront();
                buttonChangePw.Visible = false;
                buttonCancel.Visible = true;
            }
            else
            {
                textBoxNewPw.Visible = false;
                labelNewPw.Visible = false;
                buttonChangePw.Visible = true;
                buttonCancel.Visible = false;
                buttonChangePw.BringToFront();
            }
        }

        public void ShowDirPath(string path)
            => SafeInvoke(() => richTextBoxXmlPath.Text = path);

        public void SetInsertType(string type)
        {
            SafeInvoke(() =>
            {
                switch (type)
                {
                    case "A":
                        radioButtonPahtTypeA.Checked = true;
                        break;
                    case "B":
                        radioButtonPahtTypeB.Checked = true;
                        break;
                    default:
                        radioButtonPahtTypeA.Checked = true;
                        break;
                }
            });
        }

        public string SelectXmlFile()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                return dialog.ShowDialog() == DialogResult.OK
                    ? dialog.FileName
                    : null;
            }
        }

        private void SafeInvoke(Action action)
        {
            if (IsDisposed || Disposing) return;
            if (InvokeRequired) Invoke(action);
            else action();
        }
    }
}
