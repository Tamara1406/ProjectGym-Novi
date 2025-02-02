using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.UserSO
{
    public class LoginSO : BaseSO
    {
        private User user;
        public LoginSO(User user)
        {
            this.user = user;
        }

        protected override void ExecuteConcreteOperation()
        {
            Result = (User)repository.Get(user, user.UserId);
        }
    }
}
