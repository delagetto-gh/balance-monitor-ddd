using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork
{
  public interface ISessionFactory
  {
    ISession<TContext> Create<TContext>() where TContext : IDisposable;
  }
}
