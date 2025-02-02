using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.UserSO
{
    public class GetAllUsersSO : BaseSO
    {
        public GetAllUsersSO()
        {
        }
        protected override void ExecuteConcreteOperation()
        {
            ResultList = repository.GetAll(new User());
        }
    }
}
