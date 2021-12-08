using System.Linq.Expressions;
using System.Linq;
using System;

namespace dot_net_api.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        T GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}