using BalanceMonitor.Database.Ef;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections.Repositories
{
  public interface IAccountDailyBalanceRepository : IRepository<AccountDailyBalance>
  {
    IEnumerable<AccountDailyBalance> GetAccountBalanceOn(DateTime date);

    IEnumerable<AccountDailyBalance> GetAccountBalanceOn(Guid accId, DateTime date);
  }
}
