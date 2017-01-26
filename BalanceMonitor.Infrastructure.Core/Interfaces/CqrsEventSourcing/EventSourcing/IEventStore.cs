using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing
{
  /// <summary>
  /// Event stores should be append only in EventSourcing
  /// </summary>
  public interface IEventStore
  {
    IEnumerable<IDomainEvent> Events { get; }

    void Store<TDomainEvent>(IEnumerable<TDomainEvent> @events) where TDomainEvent : IDomainEvent;
  }
}
