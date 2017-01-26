using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IAggregateRootRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot, new()
  {
    TAggregateRoot Get(Guid id);
    void Save(TAggregateRoot entity);
  }
}
