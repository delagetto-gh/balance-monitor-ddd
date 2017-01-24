using BalanceMonitor.Accounting.Application.Projections.Interfaces;
using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountAuditEfDenormaliser : IAccountAuditService
  {
    private readonly ILogger logger;
    private readonly ISessionFactory sessionFactory;

    public AccountAuditEfDenormaliser(ISessionFactory sessionFactory, ILogger log)
    {
      this.logger = log;
      this.sessionFactory = sessionFactory;
    }

    public void Handle(AmountDepositedEvent @event)
    {
      this.logger.Log(String.Format("Account deposited event @ {0}", DateTimeOffset.Now));

      //var session = this.sessionFactory.Create<BalanceMonitorEFSession>();
      //using (var ctx = session.Open())
      //{
      //  AccountAudit acc = ctx.Find(@event.AggregateId);
      //  if (acc != null)
      //  {
      //    AccountAudit accAudit = new AccountAudit
      //    {
      //      AccountId = acc.AccountId,
      //      AccountName = acc.Name,
      //      ActivityType = "Amount Deposited",
      //      Amount = @event.Amount.Amount,
      //      Currency = @event.Amount.Currency,
      //      Time = @event.Created
      //    };
      //    ctx.AccountAudits.Add(accAudit);
      //    ctx.SaveChanges();
      //  }
      //  else
      //  {
      //    throw new Exception(String.Format("Account does not exist! Id: {0}", @event.AggregateId));
      //  }
      //}
    }

    public void Handle(AmountWithdrawalEvent @event)
    {
      //this.logger.Log(String.Format("Account withdrawl event @ {0}", DateTimeOffset.Now));

      //using (var ctx = this.CreateSession())
      //{
      //  Account acc = ctx.AccountDailyBalance.Find(@event.AggregateId);
      //  if (acc != null)
      //  {
      //    AccountAudit accAudit = new AccountAudit
      //    {
      //      AccountId = acc.AccountId,
      //      AccountName = acc.Name,
      //      ActivityType = "Amount Withdrawn",
      //      Amount = @event.Amount.Amount,
      //      Currency = @event.Amount.Currency,
      //      Time = @event.Created
      //    };
      //    ctx.AccountAudits.Add(accAudit);
      //    ctx.SaveChanges();
      //  }
      //  else
      //  {
      //    throw new Exception(String.Format("Account does not exist! Id: {0}", @event.AggregateId));
      //  }
      //}
    }

    public void Handle(AccountCreatedEvent @event)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<AccountDailyBalance> GetAuditForAccountOnDate(Guid accId, DateTime date)
    {
      throw new NotImplementedException();
    }
  }
}
