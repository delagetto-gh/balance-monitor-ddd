using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using System;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorSessionFactory : ISessionFactory
  {
    private readonly IContainer container;

    public BalanceMonitorSessionFactory(IContainer container)
    {
      this.container = container;
    }

    public ISession<TContext> Create<TContext>() where TContext : IDisposable
    {
      var session = this.container.Resolve<ISession<TContext>>();
      if (session != null)
      {
        return session;
      }
      else
      {
        throw new Exception(String.Format("No ISession implementation for type {0}", typeof(TContext)));
      }
    }
  }
}
