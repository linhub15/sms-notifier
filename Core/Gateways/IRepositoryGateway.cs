using System.Collections.Generic;

namespace Notifier.Core.Gateways
{
    public interface IRepositoryGateway<TId, TEntity>
    {
        TEntity Get(TId id);
        IList<TEntity> List();

        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TId Delete(TId id);
    }
}