using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork
{
  public interface ISession<TContext> where TContext : IDisposable
  {
    TContext Open();

    void Commit();
  }
}
