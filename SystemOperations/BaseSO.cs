using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repository.Implementation;
using Repository.Interfaces;

namespace SystemOperations
{
    public abstract class BaseSO
    {
        protected IDbRepository<AbsEntity> repository;
        public virtual AbsEntity Result { get; set; }
        public virtual List<AbsEntity> ResultList { get; set; }
        public BaseSO()
        {
            repository = new GenericDbRepository();
        }
        public virtual void ExecuteOperation()
        {
            try
            {
                ExecuteConcreteOperation();
                repository.Commit();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                repository.Rollback();
                throw;
            }
            catch (Exception)
            {
                repository.Rollback();
                throw;
            }
            finally
            {
                repository.Close();
            }
        }

        protected abstract void ExecuteConcreteOperation();
    }
}
