using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.GroupSO
{
    public class GetAllGroupsSO : BaseSO
    {
        public GetAllGroupsSO()
        {
        }
        protected override void ExecuteConcreteOperation()
        {
            ResultList = repository.GetAll(new Group());
        }
    }
}
