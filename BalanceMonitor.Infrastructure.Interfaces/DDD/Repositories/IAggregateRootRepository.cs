using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public interface IAggregateRootRepository<T> : IRepository<T> where T : IAggregateRoot, new()
  {
    T Get(Guid id);
    void Save(T entity);
  }
}
