using Infrastructure.Repositories.Interfaces;
using Instagram.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IBaseRepository<T> where T : class
    {
        protected InstagramDbContext _instagramDbContext;
        public GenericRepository(InstagramDbContext instagramDbContext)
        {
            _instagramDbContext = instagramDbContext;
        }

        public T Add(T entity)
        {
            return _instagramDbContext.Add(entity).Entity;
        }

        public IEnumerable<T> All()
        {
            return _instagramDbContext.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _instagramDbContext.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _instagramDbContext.Set<T>().FirstOrDefault(predicate);
        } 

        public void SaveChanges()
        {
            _instagramDbContext.SaveChanges();
        }

        public T Update(T entity)
        {
            return _instagramDbContext.Update(entity).Entity;
        }
    }
}
