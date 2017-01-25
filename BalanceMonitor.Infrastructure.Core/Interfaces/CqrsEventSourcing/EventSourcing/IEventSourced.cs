using System;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing
{
  public interface IEventSourced
  {
    int Version { get; set; }
    void LoadFromHistory(IEnumerable<VersionedDomainEvent> events);
    void MarkChangesAsCommitted();
    IEnumerable<VersionedDomainEvent> UncommitedChanges { get; }
  }
}
