using System;
using System.Linq;
using System.Linq.Expressions;
using dot_net_api.Context;
using Microsoft.EntityFrameworkCore;

namespace dot_net_api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        public Repository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> FromSql(string query)
        {
            return _context.Set<T>().FromSqlRaw(query).AsNoTracking();
        }
    }
}