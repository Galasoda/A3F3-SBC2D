using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.User
{
    public class UserMap : EntityMap<User>
    {
        public UserMap()
        {
            Map(p => p.Id).ToColumn("ID");
            Map(p => p.Role).ToColumn("ROLE");
            Map(p => p.Pw).ToColumn("PASSWORD");
        }
    }
}
