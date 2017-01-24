using System;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public interface IEventSourced
  {
    int Version { get; set; }
    void LoadFromHistory(IEnumerable<IVersionedDomainEvent> events);
    void MarkChangesAsCommitted();
    IEnumerable<IVersionedDomainEvent> UncommitedChanges { get; }
  }
}
