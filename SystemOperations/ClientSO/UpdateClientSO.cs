using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.ClientSO
{
    public class UpdateClientSO : BaseSO
    {
        Client client;
        public UpdateClientSO(Client client)
        {
            this.client = client;
        }

        protected override void ExecuteConcreteOperation()
        {
            repository.Update(client, client.ClientID);
        }
    }
}
