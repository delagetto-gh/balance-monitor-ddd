using BalanceMonitor.Accounting.Application.Commands;
using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Domain.Events;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Services.ApplicationServices
{
  public class AccountingService : IAccountingService
  {
    public void HandleCommand(CreateAccountCommand cmd)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(DateTime date)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(Guid accId, DateTime date)
    {
      throw new System.NotImplementedException();
    }

    public void Handle(AccountCreatedEvent @event)
    {
      throw new System.NotImplementedException();
    }

    public void Handle(AmountDepositedEvent @event)
    {
      throw new System.NotImplementedException();
    }

    public void Handle(AmountWithdrawalEvent @event)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<AccountDailyBalance> GetAuditForAccountOnDate(Guid accId, DateTime date)
    {
      throw new NotImplementedException();
    }
  }
}
