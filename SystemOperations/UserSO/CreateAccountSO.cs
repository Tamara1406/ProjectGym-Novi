using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.UserSO
{
    public class CreateAccountSO : BaseSO
    {
        User user;
        public CreateAccountSO(User user)
        {
            this.user = user;
        }
        protected override void ExecuteConcreteOperation()
        {
            repository.Add(user);
        }
    }
}
