using System;

namespace BalanceMonitor.Infrastructure.Interfaces.UnitOfWork
{
  public interface ISessionFactory
  {
    ISession<TContext> Create<TContext>() where TContext : IDisposable;
  }

  public interface ISession<TContext> where TContext : IDisposable
  {
    TContext Open();

    void Commit();
  }
}
