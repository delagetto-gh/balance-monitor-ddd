using BalanceMonitor.Infrastructure.Interfaces.DDD;
using System;

namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public interface IVersionedDomainEvent : IDomainEvent
  {
    int Version { get; set; }
  }
}
