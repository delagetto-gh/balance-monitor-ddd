using BalanceMonitor.Accounting.Application.Projections.Interfaces;
using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountAuditDenormaliser : IAccountAuditQuerier,
                                          IEventHandler<AccountCreatedEvent>,
                                          IEventHandler<AmountDepositedEvent>,
                                          IEventHandler<AmountWithdrawalEvent>
  {
    private readonly ILogger logger;
    private readonly ISessionFactory sessionFactory;

    public AccountAuditDenormaliser(ISessionFactory sessionFactory, ILogger log)
    {
      this.logger = log;
      this.sessionFactory = sessionFactory;
    }

    public IEnumerable<AccountAudit> GetAuditOnDate(DateTime date)
    {
      IEnumerable<AccountAudit> result;
      using (var ctx = this.sessionFactory.Open<AccountAuditSession>())
      {
        result = ctx.AccountAudits.Where(o => o.Time.Date == date.Date).ToList();
      }
      return result;
    }

    public void Handle(AmountDepositedEvent @event)
    {
      this.logger.Log(String.Format("Account deposited event @ {0}", DateTimeOffset.Now));

      using (var ctx = this.sessionFactory.Open<AccountAuditSession>())
      {
        var account = ctx.AccountAudits.FirstOrDefault(o => o.AccountId == @event.AggregateId);
        if (account != null)
        {
          AccountAudit accAudit = new AccountAudit
          {
            AccountId = @event.AggregateId,
            AccountName = @event.AccountName,
            Action = "Amount Deposited",
            Time = @event.DateOccured
          };
          ctx.AccountAudits.Add(accAudit);
          ctx.Commit();
        }
        else
        {
          throw new Exception(String.Format("Account does not exist! Id: {0}", @event.AggregateId));
        }
      }
    }

    public void Handle(AmountWithdrawalEvent @event)
    {
      this.logger.Log(String.Format("Account deposited event @ {0}", DateTimeOffset.Now));

      using (var ctx = this.sessionFactory.Open<AccountAuditSession>())
      {
        var account = ctx.AccountAudits.FirstOrDefault(o => o.AccountId == @event.AggregateId);
        if (account != null)
        {
          AccountAudit accAudit = new AccountAudit
          {
            AccountId = @event.AggregateId,
            AccountName = @event.AccountName,
            Action = "Amount Withdrawn",
            Time = @event.DateOccured
          };
          ctx.AccountAudits.Add(accAudit);
          ctx.Commit();
        }
        else
        {
          throw new Exception(String.Format("Account does not exist! Id: {0}", @event.AggregateId));
        }
      }
    }

    public void Handle(AccountCreatedEvent @event)
    {
      this.logger.Log(String.Format("Account deposited event @ {0}", DateTimeOffset.Now));

      using (var ctx = this.sessionFactory.Open<AccountAuditSession>())
      {
        var account = ctx.AccountAudits.FirstOrDefault(o => o.AccountId == @event.AggregateId);
        if (account == null)
        {
          AccountAudit accAudit = new AccountAudit
          {
            AccountId = @event.AggregateId,
            AccountName = @event.Name,
            Action = "Account Created",
            Time = @event.DateOccured
          };
          ctx.AccountAudits.Add(accAudit);
          ctx.Commit();
        }
        else
        {
          throw new Exception(String.Format("Account does not exist! Id: {0}", @event.AggregateId));
        }
      }
    }

  }
}
