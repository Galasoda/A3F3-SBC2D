using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.User
{
    public class User
    {
        public string Id { get; set; }
        public int Role { get; set; }
        public string Pw { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   Role == user.Role &&
                   Pw == user.Pw;
        }
    }
}
