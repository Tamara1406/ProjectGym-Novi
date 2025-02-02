using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.AppointmentSO
{
    public class DeleteAppointmentSO : BaseSO
    {
        Appointment appointment;
        public DeleteAppointmentSO(Appointment appointment)
        {
            this.appointment = appointment;
        }
        protected override void ExecuteConcreteOperation()
        {
            repository.Delete(appointment, appointment.AppointmentID);
        }
    }
}
