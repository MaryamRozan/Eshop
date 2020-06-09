using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace DataLayer
{
    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private MyEshop_DBEntities _db;
        private DbSet<TEntity> _entity;
        public GenericRepository(MyEshop_DBEntities db)
        {
            _db = db;
            _entity = _db.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null)
        {
            IQueryable<TEntity> query=_entity;
            if (where != null) {
                query = query.Where(where);
            }
            return query.ToList();
        }

        public TEntity GetById(object id)
        {
            return _entity.Find(id);
        }

        public virtual bool Insert(TEntity entity)
        {
            try
            {
                _entity.Add(entity);
                return true;
            }
            catch {
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            try {
                _db.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch {
                return false;
            }
        }
        public bool Delete(TEntity entity)
        {
            try
            {
                _db.Entry(entity).State = EntityState.Deleted;
                return true;
            }
            catch { return false; }

        }

        public bool Delete(object id)
        {
            try {
                object entity = GetById(id);
                Delete(entity);
                return true;
            }
            catch { return false; }
        }

      
    }
}
