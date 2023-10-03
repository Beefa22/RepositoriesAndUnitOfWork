using Core.Consts;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
                return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }

        public T Find(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();
          
            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

           return query.SingleOrDefault(expression);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(criteria);
            
            if(includes is not null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return query.ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip)
        {
            var query = _dbContext.Set<T>().Where(criteria);

            if (take.HasValue)
                query.Take(take.Value);
           
            if (skip.HasValue)
                query.Skip(skip.Value);
            return query.ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
            string[] includes, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            var query = _dbContext.Set<T>().Where(criteria);

            if (includes is not null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            if (take.HasValue)
                query.Take(take.Value);

            if (skip.HasValue)
                query.Skip(skip.Value);

            if (orderBy is not null)
            {  
                if(orderByDirection==OrderBy.Ascending)
                query = query.OrderBy(orderBy);
                 else
                query = query.OrderByDescending(orderBy);
            }

                return query.ToList();
        }

        public IEnumerable<T> GetAll()
       => _dbContext.Set<T>().ToList();

        public T GetById(int id)
        => _dbContext.Set<T>().Find(id);


        public async Task<T> GetByIdAsync(int id)
        => await _dbContext.Set<T>().FindAsync(id);
    }
}
