using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.CoachSO
{
    public class CreateCoachSO : BaseSO
    {
        Coach coach;
        public CreateCoachSO(Coach coach)
        {
            this.coach = coach;
        }
        protected override void ExecuteConcreteOperation()
        {
            repository.Add(coach);
        }
    }
}
