using System;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public interface IEventStore
  {
    IEnumerable<IVersionedDomainEvent> GetEvents(Guid id);

    void Save(IEventSourced entity);
  }
}
