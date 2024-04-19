using CQRS.Application.Interfaces.Repositories;
using CQRS.Data;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQRS.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext _context;
        DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;   
            dbSet = _context.Set<T>();
        }
        public T Add(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public IEnumerable<T> AddMany(IEnumerable<T> entities)
        {
            _context.BulkInsert(entities, cfg =>
            {
                cfg.SetOutputIdentity = true;
            });

            return entities;
        }

        public T FindById(object id)
        {
            var result = dbSet.Find(id);
            return result;
        }

        public T FindOne(Expression<Func<T,bool>> predicate)
        {
            return dbSet.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> GetByQuery(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public int UpdateByQuery(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> expression)
        {
            return dbSet.Where(predicate).BatchUpdate(expression);
        }

        public void UpdateOne(T entity)
        {
            var result = dbSet.Update(entity);         
        }
    }
}
