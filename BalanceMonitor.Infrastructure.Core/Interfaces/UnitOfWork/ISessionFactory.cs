using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork
{
  public interface ISessionFactory
  {
    TSession Open<TSession>() where TSession : ISession;
  }
}
