using BalanceMonitor.Infrastructure.Interfaces.DDD;
using System;

namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public interface IEsDomainEvent : IEvent
  {
    int Version { get; set; }
  }
}
