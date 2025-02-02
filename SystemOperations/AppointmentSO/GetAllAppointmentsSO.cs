using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.AppointmentSO
{
    public class GetAllAppointmentsSO : BaseSO
    {
        public GetAllAppointmentsSO()
        {
        }
        protected override void ExecuteConcreteOperation()
        {
            ResultList = repository.GetAll(new Appointment());
        }
    }
}
