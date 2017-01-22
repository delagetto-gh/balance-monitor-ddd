using BalanceMonitor.Database.Ef;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections.Repositories
{
  public interface IAccountAuditRepository : IRepository<AccountAudit>
  {
    IEnumerable<AccountDailyBalance> GetAuditForAccountOnDate(Guid accId, DateTime date);
  }
}
