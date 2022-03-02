using System.Linq.Expressions;
using MDDPlatform.DataStorage.SQLDB.Repositories;
using MDDPlatform.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace MDDPlatform.DataStorage.SQLDB
{
    public class SqlDbRepository<T, TId> : ISqlDbRepository<T, TId> where T : BaseEntity<TId>
    {
        private DbContext _dbContext;

        public SqlDbRepository(DbContext dbContext)
        {
             _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();            
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
        {
            var pred = predicate.Compile();
            var n =_dbContext.Set<T>().AsEnumerable().Where<T>(pred).ToList().Count();
            return await Task.FromResult(n);
            //return (await _dbContext.Set<T>().Where(predicate).ToListAsync()).Count();
            
            //return await _dbContext.Set<T>().CountAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAync(TId Id)
        {
            object? entity = await _dbContext.Set<T>().FindAsync(Id);
            if(entity != null)
                await DeleteAsync((T) entity);            
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {            
            var pred = predicate.Compile();
            var n =  _dbContext.Set<T>().AsEnumerable().Count(pred);            

            //var n = (await _dbContext.Set<T>().Where(predicate).ToListAsync()).Count();
            
            // int n = await _dbContext.Set<T>().CountAsync(predicate);
            return Task.FromResult(n>0);
        }

        public async Task<T> GetAsync(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

        }
    }

}