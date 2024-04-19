using System.Linq.Expressions;

namespace CQRS.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        T FindOne(Expression<Func<T, bool>> predicate);
        T FindById(object id);
        IEnumerable<T> GetByQuery(Expression<Func<T, bool>> predicate);
        void UpdateOne(T entity);
        int UpdateByQuery(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> expression);
        T Add(T entity);
        IEnumerable<T> AddMany(IEnumerable<T> entities);
    }
}
