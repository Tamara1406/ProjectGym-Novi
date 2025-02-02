using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class User : AbsEntity
    {
        [Browsable(false)]
        public int UserId { get; set; }
        public string Username { get; set; }
        [Browsable(false)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string TableName => " [User] ";

        public override string CheckAttribute(AbsEntity entity)
        {
            User user = (User)entity;
            return $" [User].Username = '{user.Username}' ";
        }

        public override string CheckId(int key)
        {
            return $" [User].UserId = {key} ";
        }

        public override string JoinKeys()
        {
            return "";
        }

        public override List<AbsEntity> ReaderRead(SqlDataReader reader)
        {
            List<AbsEntity> users = new List<AbsEntity>();

            while (reader.Read())
            {
                User user = new User
                {
                    UserId = (int)reader[0],
                    Username = reader[1].ToString(),
                    Password = reader[2].ToString(),
                    FirstName = reader[3].ToString(),
                    LastName = reader[4].ToString(),
                    Email = reader[5].ToString(),
                };
                users.Add(user);
            }

            //BrokerController.Instance.CloseConnection();

            return users;
        }

        public override string Search(string criteria)
        {
            throw new NotImplementedException();
        }

        public override string ValuesToInsert(AbsEntity entity)
        {
            User user = (User)entity;

            return $" '{user.Username}', '{user.Password}', '{user.FirstName}', '{user.LastName}', '{user.Email}' ";
        }

        public override string ValuesToSet(AbsEntity entity)
        {
            User user = (User)entity;

            return $" Username = '{user.Username}', Password = '{user.Password}', Email = '{user.Email}', Firstname = '{user.FirstName}', Lastname = '{user.LastName}' ";

        }

        public override string GetKey()
        {
            return UserId + "";
        }
    }
}

