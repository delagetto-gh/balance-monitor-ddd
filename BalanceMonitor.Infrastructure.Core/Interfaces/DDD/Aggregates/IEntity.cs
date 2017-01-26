using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IEntity<TAggregateRoot> where TAggregateRoot : IAggregateRoot
  {
    Guid Id { get; }
  }
}
