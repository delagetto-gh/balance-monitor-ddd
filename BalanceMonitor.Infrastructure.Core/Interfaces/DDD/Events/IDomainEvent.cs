using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IDomainEvent
  {
    Guid AggregateId { get; }

    DateTime Created { get; }
  }
}
