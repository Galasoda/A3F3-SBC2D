using SBC_2D.Infrastructures;
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

namespace SBC_2D.Views.UserControls
{
    public partial class MachineDiagramControl : UserControl, IMachineDiagramView
    {
        public MachineDiagramControl()
        {
            InitializeComponent();
        }

        public void ShowThicknessValue(double thickness)
        {
            SafeInvoke(() => labelThickness.Text = thickness.ToString());
        }

        public void SystemDisUpdated(IReadOnlyDictionary<int, bool> dis)
        {
            if (dis.ContainsKey(0))
                labelNxtLink.BackColor = dis[0] ? Color.Lime : AppTheme.Surface;
            if (dis.ContainsKey(1))
                labelCvReady.BackColor = dis[1] ? Color.Lime : AppTheme.Surface;
            if (dis.ContainsKey(2))
                panelEntrySensor.BackColor = dis[2] ? Color.Lime : AppTheme.Surface;
            if (dis.ContainsKey(3))
                panelExitSensor.BackColor = dis[3] ? Color.Lime : AppTheme.Surface;
            if (dis.ContainsKey(4))
                labelLowerStpSensor.BackColor = dis[4] ? Color.Lime : AppTheme.Surface;
            if (dis.ContainsKey(5))
                labelUpperStpSensor.BackColor = dis[5] ? Color.Lime : AppTheme.Surface;
            if (dis.ContainsKey(6))
                panelInPlaceSensor.BackColor = dis[6] ? Color.Lime : AppTheme.Surface;
            if (dis.ContainsKey(7))
                labelEmo.BackColor = dis[7] ? Color.Lime : AppTheme.Surface;
            if (dis.ContainsKey(8))
                labelSafetyDoorSensor.BackColor = dis[8] ? Color.Lime : AppTheme.Surface;
            if (dis.ContainsKey(9))
                panelBoardLengthSensor.BackColor = dis[9] ? Color.Lime : AppTheme.Surface;

        }

        public void SystemDosUpdated(IReadOnlyDictionary<int, bool> dos)
        {
            if (dos.ContainsKey(0))
                labelCvLink.BackColor = dos[0] ? Color.Lime : AppTheme.Surface;
            if (dos.ContainsKey(1))
                labelNxtReady.BackColor = dos[1] ? Color.Lime : AppTheme.Surface;
            if (dos.ContainsKey(2))
                labelNxtReady.BackColor = dos[2] ? Color.Lime : AppTheme.Surface;
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
    }
}
