using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.ClientSO
{
    public class GetAllClientsSO : BaseSO
    {
        public GetAllClientsSO()
        {
        }
        protected override void ExecuteConcreteOperation()
        {
            ResultList = repository.GetAll(new Client());
        }
    }
}
