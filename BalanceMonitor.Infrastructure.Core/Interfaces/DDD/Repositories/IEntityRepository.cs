using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IEntityRepository<TEntity> where TEntity : IEntity, new()
  {
    TEntity Get(Guid id);
    void Add(TEntity entity);
  }
}
