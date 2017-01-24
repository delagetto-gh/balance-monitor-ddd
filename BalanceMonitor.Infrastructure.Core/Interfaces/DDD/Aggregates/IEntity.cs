using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IEntity
  {
    Guid Id { get; }
  }
}
