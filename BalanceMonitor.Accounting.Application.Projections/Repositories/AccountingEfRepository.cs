using BalanceMonitor.Database.Ef;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceMonitor.Accounting.Application.Projections.Repositories
{
  public class AccountingEfRepository : IAccountingQueryService
  {
    public IEnumerable<AccountDailyBalance> GetAuditForAccountOnDate(Guid accId, DateTime date)
    {
      throw new NotImplementedException();
    }

    public AccountAudit Get(Guid id)
    {
      using (var ctx = new BalanceMonitorEntities())
      {
        var acc = ctx.Accounts.FirstOrDefault(o => o.AccountId == id);
        if (acc != null)
        {
          throw new NotImplementedException("Woo got here, but need to remodel the database!");
        }
        return null;
      }
    }

    public void Save(AccountAudit entity)
    {
      throw new NotImplementedException("Woo got here, but need to remodel the database!");

    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(DateTime date)
    {
      throw new NotImplementedException("Woo got here, but need to remodel the database!");
    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(Guid accId, DateTime date)
    {
      throw new NotImplementedException("Woo got here, but need to remodel the database!");
    }



    IEnumerable<AccountDailyBalance> IAccountDailyBalanceRepository.GetAccountBalanceOn(DateTime date)
    {
      throw new NotImplementedException();
    }

    IEnumerable<AccountDailyBalance> IAccountDailyBalanceRepository.GetAccountBalanceOn(Guid accId, DateTime date)
    {
      throw new NotImplementedException();
    }

    AccountDailyBalance Infrastructure.Interfaces.UnitOfWork.IRepository<AccountDailyBalance>.Get(Guid id)
    {
      throw new NotImplementedException();
    }

    void Infrastructure.Interfaces.UnitOfWork.IRepository<AccountDailyBalance>.Save(AccountDailyBalance entity)
    {
      throw new NotImplementedException();
    }
  }
}
