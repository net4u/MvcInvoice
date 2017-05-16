using Invoice.Database.Context;
using Invoice.Database.Interfaces;
using Invoice.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Service.Base
{
    public abstract class EntityService<T, U> : IEntityService<T, U>
        where T: class
    {
        protected IContext _dbContext;
        protected DbSet<T> _dbSet;

        public EntityService(IContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IEnumerable<T> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual T Single(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Single(predicate);
        }

        public virtual T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public virtual T First(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbSet.First(predicate);
        }


        public virtual T GetById(U id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
        }

        public void Commit()
        {
            _dbContext.Commit();
        }
    }
}
