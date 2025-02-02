using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.CoachSO
{
    public class GetAllCoachesSO : BaseSO
    {
        public GetAllCoachesSO()
        {
        }
        protected override void ExecuteConcreteOperation()
        {
            ResultList = repository.GetAll(new Coach());
        }
    }
}
