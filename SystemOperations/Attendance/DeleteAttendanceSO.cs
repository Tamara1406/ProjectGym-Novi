using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.Attendance
{
    public class DeleteAttendanceSO : BaseSO
    {
        Domain.Attendance attendance;
        public DeleteAttendanceSO(Domain.Attendance attendance)
        {
            this.attendance = attendance;
        }
        protected override void ExecuteConcreteOperation()
        {
            repository.Delete(attendance, attendance.Client.ClientID);
        }
    }
}
