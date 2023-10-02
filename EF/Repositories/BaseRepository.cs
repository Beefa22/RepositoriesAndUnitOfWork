using Core.Interfaces;
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

        public T Find(Expression<Func<T, bool>> expression)
          => _dbContext.Set<T>().SingleOrDefault(expression);

        public IEnumerable<T> GetAll()
       => _dbContext.Set<T>().ToList();

        public T GetById(int id)
        => _dbContext.Set<T>().Find(id);


        public async Task<T> GetByIdAsync(int id)
        => await _dbContext.Set<T>().FindAsync(id);
    }
}
