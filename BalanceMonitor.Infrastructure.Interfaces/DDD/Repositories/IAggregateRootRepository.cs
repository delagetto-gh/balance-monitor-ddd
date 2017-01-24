using System;

namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public interface IAggregateRootRepository<TAggregateRoot> : IEntityRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot, new()
  { }
}
