using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IDomainEvent : IEvent
  {
    Guid AggregateId { get; }
  }
}
