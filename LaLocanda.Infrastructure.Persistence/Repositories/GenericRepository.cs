using LaLocanda.Infrastructure.Persistence.Contexts;
using LaLocanda.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LaLocandaContext _dbContext;

        public GenericRepository(LaLocandaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T t)
        {
            await _dbContext.Set<T>().AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return t;
        }

        public async Task UpdateAsync(T t, int id)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T t)
        {
            _dbContext.Set<T>().Remove(t);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> GetAllWithIncludesAsync(List<string> properties)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdWithIncludeAsync(int id, List<string> props, List<string> colls)
        {
            var query = await _dbContext.Set<T>().FindAsync(id);

            foreach (string prop in props)
            {
                _dbContext.Entry(query).Reference(prop).Load();
            }

            foreach (string coll in colls)
            {
                _dbContext.Entry(query).Collection(coll).Load();
            }

            return query;
        }
    }
}
