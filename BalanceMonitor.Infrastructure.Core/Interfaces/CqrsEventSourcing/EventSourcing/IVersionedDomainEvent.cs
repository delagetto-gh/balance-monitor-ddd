using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing
{
  public interface IVersionedDomainEvent : IDomainEvent
  {
    int Version { get; set; }
  }
}
