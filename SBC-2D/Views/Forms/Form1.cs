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

namespace SBC_2D.Views.Forms
{
    public partial class Form1 : Form, IHomePageView
    {
        public IMachineDiagramView MachineDiagramView { get; }
        public event EventHandler AutoRunClicked;
        public event EventHandler StopClicked;

        public Form1()
        {
            InitializeComponent();
            MachineDiagramView = machineDiagramControl1;
        }

        public void AddMessage(string message)
        {
            SafeInvoke(() =>
            {
                listBoxMessage.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                listBoxMessage.TopIndex = listBoxMessage.Items.Count - 1;
            });
        }

        public void ClearMessages()
        {

            SafeInvoke(() =>
            {
                listBoxMessage.Items.Clear();
            });
        }

        public void AddErrorMessage(string errorMessage)
        {
            SafeInvoke(() =>
            {
                listBoxErrorMessage.Items.Add($"[{DateTime.Now:HH:mm:ss}] {errorMessage}");
                listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
            });
        }

        public void ClearErrorMessages()
        {
            SafeInvoke(() =>
            {
                listBoxErrorMessage.Items.Clear();
            });
        }

        public void SetSentCount(int count)
        {
            SafeInvoke(() =>
            {
                label6.Text = count.ToString();
            });
        }

        public void SetAutoRunEnabled(bool isEnabled)
        {
            SafeInvoke(() =>
            {
                buttonAutoRun.Enabled = isEnabled;
            });
        }

        public void SetStopEnabled(bool isEnabled)
        {
            SafeInvoke(() =>
            {
                buttonStop.Enabled = isEnabled;
            });
        }

        private void SafeInvoke(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
                return;
            }
            else
            {
                action();
            }
        }

        private void buttonAutoRun_Click(object sender, EventArgs e)
            => AutoRunClicked?.Invoke(this, EventArgs.Empty);

        private void buttonStop_Click(object sender, EventArgs e)
            => StopClicked?.Invoke(this, EventArgs.Empty);
    }
}
