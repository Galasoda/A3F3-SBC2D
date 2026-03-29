using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Views.Interfaces
{
    public interface IUserLoginView
    {
        event EventHandler UserLoginRequested;
        event EventHandler ChangePwRequested;
        event EventHandler CancelChangePwRequested;
        event EventHandler<Role> RoleChanged;
        event EventHandler<string> IdChanged;
        event EventHandler<string> PwChanged;
        event EventHandler<string> NewPwChanged;

        void SetChangePwMode(bool isChangePw);
        void HighlightRoleView(Role role);
        void ClearEnterInfos();
        void ShowLogedMessage(string message);
    }
}
