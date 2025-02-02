using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.AppointmentSO
{
    public class UpdateAppointmentSO : BaseSO
    {
        Appointment appointment;
        public UpdateAppointmentSO(Appointment appointment)
        {
            this.appointment = appointment;
        }

        protected override void ExecuteConcreteOperation()
        {
            repository.Update(appointment, appointment.AppointmentID);
        }
    }
}
