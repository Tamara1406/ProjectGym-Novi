using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ValidatorClient
    {
        internal bool CheckUserData(User user)
        {
            if (!user.Email.Contains("@"))
            {
                return false;
            }

            if (user.Username.Length < 4)
            {
                return false;
            }

            if (user.Password.Length < 4)
            {
                return false;
            }

            return true;
        }

        internal bool CheckLoginData(User user)
        {
            if (user.Username.Length < 4 || user.Password.Length < 4)
            {
                return false;
            }
            return true;
        }
    }
}
