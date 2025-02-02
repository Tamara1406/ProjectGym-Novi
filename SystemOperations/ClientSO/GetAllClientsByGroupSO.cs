using Domain;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.ClientSO
{
    public class GetAllClientsByGroupSO : BaseSO
    {
        Group group;
        public GetAllClientsByGroupSO(Group group)
        {
            this.group = group;
        }
        protected override void ExecuteConcreteOperation()
        {
            ResultList = repository.Search(new Client(), group.GroupID.ToString());
        }
    }
}
