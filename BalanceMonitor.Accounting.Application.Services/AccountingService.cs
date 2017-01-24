using BalanceMonitor.Accounting.Application.Commands;
using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Interfaces.Cqrs;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Services.ApplicationServices
{
  public class AccountingService : IAccountingService
  {
    private readonly ICommandBus cmdBus;
    public AccountingService(ICommandBus cmdBus)
    {
      this.cmdBus = cmdBus;
    }

    public void HandleCommand(CreateAccountCommand cmd)
    {
      this.cmdBus.Submit(cmd);
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
