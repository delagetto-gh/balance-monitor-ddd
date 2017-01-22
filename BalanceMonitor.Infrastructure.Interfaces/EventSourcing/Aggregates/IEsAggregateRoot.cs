using BalanceMonitor.Infrastructure.Interfaces.DDD;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public interface IEsAggregateRoot : IAggregateRoot
  {
    int Version { get; set; }
    void LoadFromHistory(IEnumerable<IEsDomainEvent> events);
    void MarkChangesAsCommitted();
    IEnumerable<IEsDomainEvent> UncommitedChanges { get; }
  }
}
