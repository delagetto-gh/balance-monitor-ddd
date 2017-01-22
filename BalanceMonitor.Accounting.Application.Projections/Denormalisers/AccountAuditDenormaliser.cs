using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Database.Ef;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountAuditDenormaliser : IEventHandler<AmountDepositedEvent>,
                                          IEventHandler<AmountWithdrawalEvent>
  {
    private readonly ILogger logger;

    public AccountAuditDenormaliser(ILogger log)
    {
      this.logger = log;
    }

    public void Handle(AmountDepositedEvent @event)
    {
      this.logger.Log(String.Format("Account deposited event @ {0}", DateTimeOffset.Now));

      using (var ctx = this.CreateSession())
      {
        Account acc = ctx.Accounts.Find(@event.AggregateId);
        if (acc != null)
        {
          AccountAudit accAudit = new AccountAudit
          {
            AccountId = acc.AccountId,
            AccountName = acc.Name,
            ActivityTypeId = (int)ActivityType.AmountDebited,
            Amount = @event.Cash.Amount,
            Currency = @event.Cash.Currency,
            Time = @event.Created
          };
          ctx.AccountAudits.Add(accAudit);
          ctx.SaveChanges();
        }
        else
        {
          throw new Exception(String.Format("Account does not exist! Id: {0}", @event.AggregateId));
        }
      }
    }

    public void Handle(AmountWithdrawalEvent @event)
    {
      this.logger.Log(String.Format("Account withdrawl event @ {0}", DateTimeOffset.Now));

      using (var ctx = this.CreateSession())
      {
        Account acc = ctx.Accounts.Find(@event.AggregateId);
        if (acc != null)
        {
          AccountAudit accAudit = new AccountAudit
          {
            AccountId = acc.AccountId,
            AccountName = acc.Name,
            ActivityTypeId = (int)ActivityType.AmountDebited,
            Amount = @event.Cash.Amount,
            Currency = @event.Cash.Currency,
            Time = @event.Created
          };
          ctx.AccountAudits.Add(accAudit);
          ctx.SaveChanges();
        }
        else
        {
          throw new Exception(String.Format("Account does not exist! Id: {0}", @event.AggregateId));
        }
      }
    }

    private BalanceMonitorEntities CreateSession()
    {
      return new BalanceMonitorEntities();
    }
  }
}
