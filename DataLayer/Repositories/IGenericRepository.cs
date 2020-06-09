using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DataLayer
{
   public interface IGenericRepository<TEntity>
    {
         IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null);
        TEntity GetById(object id);
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool Delete(object id);



    }
}
