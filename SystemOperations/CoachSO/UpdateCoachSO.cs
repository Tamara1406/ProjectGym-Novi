using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.CoachSO
{
    public class UpdateCoachSO : BaseSO
    {
        Coach coach;
        public UpdateCoachSO(Coach coach)
        {
            this.coach = coach;
        }

        protected override void ExecuteConcreteOperation()
        {
            repository.Update(coach, coach.CoachID);
        }

    }
}
