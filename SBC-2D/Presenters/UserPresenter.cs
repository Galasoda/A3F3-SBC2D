using SBC_2D.Domain.Servicies;
using SBC_2D.Events;
using SBC_2D.Infrastructures.User;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Presenters
{
    public class UserPresenter : IDisposable
    {
        private readonly IUserLoginView _userLoginView;
        private readonly UserService _userService;
        private User _user;
        private string _newPw;
        private bool _isOnChangePw;
        private IEventBus _eventBus;


        public UserPresenter(IUserLoginView userLoginView, UserService userService, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _userLoginView = userLoginView;
            _userService = userService;
            _user = new User();
            _userLoginView.RoleChanged += UserLoginView_RoleChanged;
            _userLoginView.IdChanged += UserLoginView_IdChanged;
            _userLoginView.PwChanged += UserLoginView_PwChanged;
            _userLoginView.NewPwChanged += UserLoginView_NewPwChanged;
            _userLoginView.UserLoginRequested += UserLoginView_UserLoginRequested;
            _userLoginView.ChangePwRequested += UserLoginView_ChangePwRequested;
            _userLoginView.CancelChangePwRequested += UserLoginView_CancelChangePwRequested;
        }

        public void Dispose()
        {
            _userLoginView.RoleChanged -= UserLoginView_RoleChanged;
            _userLoginView.IdChanged -= UserLoginView_IdChanged;
            _userLoginView.PwChanged -= UserLoginView_PwChanged;
            _userLoginView.NewPwChanged -= UserLoginView_NewPwChanged;
            _userLoginView.UserLoginRequested -= UserLoginView_UserLoginRequested;
            _userLoginView.ChangePwRequested -= UserLoginView_ChangePwRequested;
            _userLoginView.CancelChangePwRequested -= UserLoginView_CancelChangePwRequested;
        }

        public void Initialize()
        {
            _user.Id = string.Empty;
            _user.Pw = string.Empty;
            _isOnChangePw = false;
            _userLoginView.SetChangePwMode(false);
            _userLoginView.HighlightRoleView(Role.Operater);
            Login(out _);
        }

        private void UserLoginView_CancelChangePwRequested(object sender, EventArgs e)
        {
            _isOnChangePw = false;
            _userLoginView.SetChangePwMode(false);
        }

        private void UserLoginView_UserLoginRequested(object sender, EventArgs e)
        {
            string message = string.Empty;
            if (_isOnChangePw)
            {
                if (_userService.ChangePw(_user, _newPw, out message))
                {
                    _isOnChangePw = false;
                    _userLoginView.SetChangePwMode(false);
                }
                _userLoginView.ShowLogedMessage(message);
            }
            else
            {
                if (Login(out message))
                {
                    _eventBus.Publish<(Role, string)>(((Role)_user.Role, _user.Id));
                    _userLoginView.ClearEnterInfos();
                }
                _userLoginView.ShowLogedMessage(message);
            }
        }

        private void UserLoginView_ChangePwRequested(object sender, EventArgs e)
        {
            if (_user.Role == 0)
                return;
            _isOnChangePw = true;
            _userLoginView.SetChangePwMode(true);
        }

        private void UserLoginView_NewPwChanged(object sender, string e)
            => _newPw = e;

        private void UserLoginView_PwChanged(object sender, string e)
            => _user.Pw = e;

        private void UserLoginView_IdChanged(object sender, string e)
            => _user.Id = e;

        private void UserLoginView_RoleChanged(object sender, Role e)
        {
            _user.Role = (int)e;
            _userLoginView.HighlightRoleView(e);
        }

        private bool Login(out string message)
        {
            bool isLoged = _userService.Login(_user, out message);
            if (isLoged)
            {
                _user.Pw = string.Empty;
                _userLoginView.ClearEnterInfos();
                _eventBus.Publish<(Role, string)>(((Role)_user.Role, _user.Id));
            }
            return isLoged;
        }
    }
}
