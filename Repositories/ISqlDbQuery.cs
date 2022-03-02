using MDDPlatform.DataStorage.Core;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DataStorage.SQLDB.Repositories{
    public interface ISqlDbQuery<TEntity,TId> : IDbQuery<TEntity,TId> where TEntity : BaseEntity<TId>
    {

    }
}