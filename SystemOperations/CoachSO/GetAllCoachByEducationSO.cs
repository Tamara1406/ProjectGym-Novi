using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.CoachSO
{
    public class GetAllCoachByEducationSO : BaseSO
    {
        Education education;
        public GetAllCoachByEducationSO(Education education)
        {
            this.education = education;
        }
        protected override void ExecuteConcreteOperation()
        {
            ResultList = repository.Search(new Coach(), education.EducationID.ToString());
        }
    }
}
