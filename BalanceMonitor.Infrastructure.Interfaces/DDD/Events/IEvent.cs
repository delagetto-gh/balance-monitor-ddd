using System;

namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public interface IEvent
  {
    Guid AggregateId { get; }

    DateTime Created { get; }
  }
}
