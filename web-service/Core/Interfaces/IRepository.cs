using System.Collections.Generic;

namespace Notifier.Core.Interfaces
{
    public interface IRepository<TId, TEntity>
    {
        TEntity Get(TId id);
        IList<TEntity> List();

        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TId Delete(TId id);
    }
}