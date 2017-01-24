using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Interfaces.DDD;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections.Interfaces
{
  public interface IAccountDailyBalanceService : IEventHandler<AccountCreatedEvent>,
                                                 IEventHandler<AmountDepositedEvent>,
                                                 IEventHandler<AmountWithdrawalEvent>
  {
    IEnumerable<AccountDailyBalance> GetAccountBalanceOn(DateTime date);

    IEnumerable<AccountDailyBalance> GetAccountBalanceOn(Guid accId, DateTime date);
  }
}
