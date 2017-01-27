using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork
{
  public interface ISessionFactory 
  {
    TContext Open<TContext>();

    void Commit();
  }
}
