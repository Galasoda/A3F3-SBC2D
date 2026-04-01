using SBC_2D.Infrastructures.User;
using SBC_2D.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Domain.Servicies
{
    public class UserService
    {
        private UserDao _userDao;
        public UserService(UserDao userDao)
        {
            _userDao = userDao;
        }

        public bool Login(User user, out string message)
        {
            message = "";
            try
            {
                if (user == null)
                {
                    message = $"Login failed by null issuse.";
                    return false;
                }
                if (user.Role == (int)Role.Operater)
                {
                    message = $"{(Role)user.Role} {user.Id} has successfully logged in.";
                    return true;
                }
                if (string.IsNullOrWhiteSpace(user.Id))
                {
                    message = $"Login failed because id is empty.";
                    return false;
                }
                User u = _userDao.Get(user.Id);
                if (u == null || user.Id != u.Id)
                {
                    message = $"{(Role)user.Role} {user.Id} login failed, because user not exist..";
                    return false;
                }
                bool isPwMatched = user.Pw == u.Pw;
                if (!isPwMatched)
                {
                    message = $"{(Role)user.Role} {user.Id} login failed because password is not correct";
                    return false;
                }
                message = $"{(Role)user.Role} {user.Id} has successfully logged in.";
                return true;
            }
            catch (Exception ex)
            {
                message = $"User {user.Id} login failed by issuse.";
                return false;
            }
        }

        public bool ChangePw(User user, string newPw, out string message)
        {
            message = "";
            bool isUpdated = false;
            try
            {
                if(!Login(user, out message))
                    return false;
                isUpdated = _userDao.UpdatePw(user.Id, newPw);
                string status = isUpdated ? "completed" : "failed";
                message = $"{(Role)user.Role} {user.Id} change password {status}.";
            }
            catch (Exception ex)
            {
                message = $"User {user.Id} changePw failed by issuse.";
            }
            return isUpdated;
        }
    }
}
