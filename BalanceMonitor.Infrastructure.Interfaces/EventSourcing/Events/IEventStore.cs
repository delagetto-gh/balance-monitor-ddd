using BalanceMonitor.Infrastructure.Interfaces.DDD;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public interface IEventStore
  {
    IEnumerable<IEsDomainEvent> GetEvents(Guid id);

    void Save(IEsAggregateRoot entity);
  }
}
