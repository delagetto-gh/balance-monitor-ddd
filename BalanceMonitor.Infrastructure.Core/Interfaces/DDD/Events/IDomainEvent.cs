using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IDomainEvent
  {
    int Version { get; set; }

    Guid AggregateId { get; }

    DateTime Created { get; }
  }
}
