using System;

namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public interface IDomainEvent
  {
    Guid AggregateId { get; }

    DateTime Created { get; }
  }
}
