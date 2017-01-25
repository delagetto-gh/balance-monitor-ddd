using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections.Interfaces
{
  public interface IAccountDailyBalanceQuerier
  {
    IEnumerable<AccountDailyBalance> GetAccountBalanceOn(DateTime date);

    IEnumerable<AccountDailyBalance> GetAccountBalanceOn(Guid accId, DateTime date);
  }
}
