using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing
{
  public interface IEventSourced
  {
    int Version { get; set; }
    void LoadFromHistory(IEnumerable<IDomainEvent> events);
    void MarkChangesAsCommitted();
    IEnumerable<IDomainEvent> UncommitedChanges { get; }
  }
}
