using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork
{
  public interface ISession<TContext> : IDisposable
  {
    TContext Open();

    void Commit();
  }
}
