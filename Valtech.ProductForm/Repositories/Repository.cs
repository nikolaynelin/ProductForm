using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Valtech.ProductForm.Repositories
{
    public class Repository : IRepository
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public T Get<T>(object entityId) where T : class
        {
            return _context.Set<T>().Find(entityId);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return predicate == null ? _context.Set<T>() : _context.Set<T>().Where(predicate);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> entityInclude) where T : class
        {
            var result = Get(predicate);

            return entityInclude == null
                ? result
                : entityInclude(result);
        }

        public void Update<T>(T model) where T : class
        {
            var entity = _context.Set<T>().Attach(model);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}