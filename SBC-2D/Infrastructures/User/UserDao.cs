using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.User
{
    public class UserDao
    {
        private readonly string _sqLiteConnectionString;

        public UserDao(string sqLiteFilePath)
        {
            if (string.IsNullOrWhiteSpace(sqLiteFilePath))
                throw new ArgumentException("SQLite file path is invalid.", nameof(sqLiteFilePath));
            _sqLiteConnectionString = $@"Data Source={sqLiteFilePath}";
        }

        public User Get(string id)
        {
            User result = null;
            using (SqliteConnection conn = new SqliteConnection(_sqLiteConnectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM USER WHERE ID = @ID";
                result = conn.QuerySingle<User>(sql, new { ID = id });
            }
            return result;
        }

        public bool UpdatePw(string id, string pw)
        {
            bool isUpdated = false;
            using (SqliteConnection conn = new SqliteConnection(_sqLiteConnectionString))
            {
                conn.Open();
                string sql = "UPDATE USER SET PASSWORD = @PW WHERE ID = @ID";
                isUpdated = conn.Execute(sql, new { ID = id, PW = pw }) > 0;
            }
            return isUpdated;
        }
    }
}
