using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IEvent
  {
    DateTime DateOccured { get; }
  }
}
