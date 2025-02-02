using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.UserSO
{
    public class UpdateAccountSO : BaseSO
    {
        User user;
        public UpdateAccountSO(User user)
        {
            this.user = user;
        }
        protected override void ExecuteConcreteOperation()
        {
            repository.Update(user, user.UserId);
        }
    }
}
