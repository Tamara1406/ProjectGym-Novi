using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.UserSO
{
    public class GetUserByUsernameSO : BaseSO
    {
        User user;
        public GetUserByUsernameSO(User user)
        {
            this.user = user;
        }

        protected override void ExecuteConcreteOperation()
        {
            Result = repository.Get(user, user.UserId);
        }
    }
}
