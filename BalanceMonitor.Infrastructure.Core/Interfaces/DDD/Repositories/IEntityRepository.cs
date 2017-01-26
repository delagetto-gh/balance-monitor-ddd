using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
  {
    TEntity Get(Guid id);
    void Add(TEntity entity);
  }
}
