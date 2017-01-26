using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing
{
  public interface IEventSourced : IAggregateRoot
  {
    int Version { get; }
    void LoadFromHistory(IEnumerable<IDomainEvent> events);
    void MarkChangesAsCommitted();
    IEnumerable<IDomainEvent> UncommitedChanges { get; }
  }
}
