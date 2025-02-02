using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.Attendance
{
    public class CreateAttendancesSO : BaseSO
    {
        List<Domain.Attendance> attendances;
        public CreateAttendancesSO(List<Domain.Attendance> attendances)
        {
            this.attendances = attendances;
        }
        protected override void ExecuteConcreteOperation()
        {
            foreach(Domain.Attendance attendance in attendances)
            {
            repository.Add(attendance);
            }
        }
    }
}
