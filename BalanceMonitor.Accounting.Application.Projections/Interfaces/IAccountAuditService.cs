using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Interfaces.DDD;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections.Interfaces
{
  public interface IAccountAuditService : IEventHandler<AmountDepositedEvent>,
                                          IEventHandler<AmountWithdrawalEvent>,
                                          IEventHandler<AccountCreatedEvent>
  {
    IEnumerable<AccountDailyBalance> GetAuditForAccountOnDate(Guid accId, DateTime date);
  }
}
