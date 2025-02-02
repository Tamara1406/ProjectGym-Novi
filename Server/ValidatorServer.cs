using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemOperations.UserSO;

namespace Server
{
    public class ValidatorServer
    {
        public static List<User> loggedUsers = new List<User>();

        public static bool CheckUser(User checkUser)
        {
            foreach (User user in loggedUsers)
            {
                if (user.Username.Equals(checkUser.Username))
                {
                    return false;
                }
            }
            loggedUsers.Add(checkUser);
            return true;
        }

        public static bool CheckUniqueData(User user)
        {
            List<User> users = ServerController.Instance.GetAllUsers(new GetAllUsersSO());

            foreach (User user1 in users)
            {
                if (user.Username == user1.Username)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
