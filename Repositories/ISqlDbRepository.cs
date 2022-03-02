using System.Linq.Expressions;
using MDDPlatform.DataStorage.Core;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DataStorage.SQLDB.Repositories
{
    public interface ISqlDbRepository<TEntity, TId> : IRepository<TEntity,TId> where TEntity : BaseEntity<TId>
    {
        
    }
}