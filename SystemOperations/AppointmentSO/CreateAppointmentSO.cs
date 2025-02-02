using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.AppointmentSO
{
    public class CreateAppointmentSO : BaseSO
    {
        Appointment appointment;
        public CreateAppointmentSO(Appointment appointment)
        {
            this.appointment = appointment;
        }
        protected override void ExecuteConcreteOperation()
        {
            repository.Add(appointment);
        }
    }
}
