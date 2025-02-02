using Domain;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.ClientSO
{
    public class GetClientSO : BaseSO
    {
        Client client;
        public GetClientSO(Client client)
        {
            this.client = client;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = repository.Get(client, client.ClientID);
        }
    }
}
