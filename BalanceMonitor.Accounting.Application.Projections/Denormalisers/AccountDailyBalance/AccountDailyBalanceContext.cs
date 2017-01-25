using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountDailyBalanceContext : ISession<AccountDailyBalanceContext>
  {
    private readonly ISession<AccountDailyBalanceContext> session;

    public AccountDailyBalanceContext(ISession<AccountDailyBalanceContext> session)
    {
      this.session = session;
    }

    public AccountDailyBalanceContext Open()
    {
      throw new NotImplementedException();
    }

    public void Commit()
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}
