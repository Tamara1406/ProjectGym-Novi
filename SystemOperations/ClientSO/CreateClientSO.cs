using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.ClientSO
{
    public class CreateClientSO : BaseSO
    {
        Client client;
        public CreateClientSO(Client client)
        {
            this.client = client;
        }
        protected override void ExecuteConcreteOperation()
        {
            repository.Add(client);
        }
    }
}
