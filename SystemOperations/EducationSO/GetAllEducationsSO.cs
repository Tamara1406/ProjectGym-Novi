using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.EducationSO
{
    public class GetAllEducationsSO : BaseSO
    {
        public GetAllEducationsSO()
        {
        }
        protected override void ExecuteConcreteOperation()
        {
            ResultList = repository.GetAll(new Education());
        }
    }
}
