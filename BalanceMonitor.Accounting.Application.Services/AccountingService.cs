using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Services
{
  public class AccountingService : IAccountingService
  {
    private readonly ICommandBus cmdBus;
    private readonly IAccountDailyBalanceQuerier accDailyBalanceService;
    private readonly IAccountAuditQuerier accAuditService;

    public AccountingService(ICommandBus cmdBus, IAccountDailyBalanceQuerier accDailyBalanceService, IAccountAuditQuerier accAuditService)
    {
      this.cmdBus = cmdBus;
      this.accDailyBalanceService = accDailyBalanceService;
      this.accAuditService = accAuditService;
    }

    public void Submit<TCommand>(TCommand cmd) where TCommand : ICommand
    {
      if (cmd == null)
        return;

      this.cmdBus.Submit(cmd);
    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(DateTime date)
    {
      return this.accDailyBalanceService.GetAccountBalanceOn(date);
    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(Guid accId, DateTime date)
    {
      return this.accDailyBalanceService.GetAccountBalanceOn(accId, date);
    }

    public IEnumerable<AccountAudit> GetAuditOnDate(DateTime date)
    {
      return this.accAuditService.GetAuditOnDate(date);
    }
  }
}
