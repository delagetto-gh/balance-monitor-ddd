using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;
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

    public TSession Open<TSession>() where TSession : ISession
    {
      var sesh = this.container.Resolve<TSession>();
      if (sesh != null)
      {
        return sesh;
      }
      else
      {
        throw new Exception(string.Format("Failed to resolve session implementation of type {0}.", typeof(TSession).Name));
      }
    }
  }
}
