using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        AbsEntity Add(TEntity entity);
        AbsEntity Update(TEntity entity, int key);
        TEntity Get(TEntity entity, int key);
        List<TEntity> Search(TEntity entity, string criteria);
        List<TEntity> GetAll(TEntity entity);
        TEntity Delete(TEntity entity, int key);
    }
}
