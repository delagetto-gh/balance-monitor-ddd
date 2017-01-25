using System;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing
{
  public interface IEventSourced
  {
    int Version { get; set; }
    void LoadFromHistory(IEnumerable<IVersionedDomainEvent> events);
    void MarkChangesAsCommitted();
    IEnumerable<IVersionedDomainEvent> UncommitedChanges { get; }
  }
}
