using System;

namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public interface IEntity
  {
    Guid Id { get; }
  }
}
