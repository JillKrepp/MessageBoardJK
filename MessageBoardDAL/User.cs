using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MessageBoardDAL
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static User GetUserByUsernameAndPassword(string username, string password)
        {

            string hash = Hasher.Hash(password);
            DataTable table = ForumDB.QuerySproc("GetUserByUsernameAndPassword", new SqlParameter("username", username), new SqlParameter("password", hash));
            if (table.Rows.Count == 1)
            {
                return
                    new User
                    {
                        Password = Convert.ToString(table.Rows[0]["password"]),
                        UserId = Convert.ToInt32(table.Rows[0]["user_id"]),
                        Username = Convert.ToString(table.Rows[0]["username"])
                    };
            }

            return null;
        }


        public void Save()
        {
            if (this.UserId == 0)
            {
                string hash = Hasher.Hash(this.Password);
                int userId = (int)ForumDB.ExecuteScalar("AddUser", new SqlParameter("username", this.Username), new SqlParameter("password", hash));
                this.UserId = userId;
                this.Password = hash;
            }
            else
            {
                string hash = Hasher.Hash(this.Password);
                ForumDB.NonQuery("UpdateUser", new SqlParameter("Password", this.Password), new SqlParameter("user_id", this.UserId));
            }
        }
    }
}
