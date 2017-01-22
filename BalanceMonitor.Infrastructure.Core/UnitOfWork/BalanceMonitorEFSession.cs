using BalanceMonitor.Database.Ef;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using System;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorEFSession : ISession<BalanceMonitorEntities>
  {
    private BalanceMonitorEntities currentCtx;

    public void Commit()
    {
      this.currentCtx.SaveChanges();
    }

    public BalanceMonitorEntities Open()
    {
      this.currentCtx = new BalanceMonitorEntities();
      return this.currentCtx;
    }
  }
}
