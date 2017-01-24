using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using System;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorSession : ISession<BalanceMonitorEntities>
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
