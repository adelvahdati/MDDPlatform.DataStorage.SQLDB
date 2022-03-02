using System.Linq.Expressions;
using MDDPlatform.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace MDDPlatform.DataStorage.SQLDB.Repositories{
    public class SqlDbQuery<TEntity, TId> : ISqlDbQuery<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private DbContext _dbContext;

        public SqlDbQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().CountAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            int n = await _dbContext.Set<TEntity>().CountAsync(predicate);
            return (n>0);

        }

        public async Task<TEntity> GetAsync(TId id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);

        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }
    }
}