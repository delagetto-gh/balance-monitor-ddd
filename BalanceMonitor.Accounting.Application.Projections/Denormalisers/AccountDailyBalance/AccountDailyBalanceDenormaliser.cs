using BalanceMonitor.Accounting.Application.Projections.Interfaces;
using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountDailyBalanceDenormaliser : IAccountDailyBalanceQuerier,
                                                 IEventHandler<AmountDepositedEvent>,
                                                 IEventHandler<AmountWithdrawalEvent>,
                                                 IEventHandler<AccountCreatedEvent>
  {
    private readonly ILogger logger;
    private readonly ISession<AccountDailyBalanceContext> session;

    public AccountDailyBalanceDenormaliser(ISession<AccountDailyBalanceContext> session, ILogger log)
    {
      this.logger = log;
      this.session = session;
    }

    public void Handle(AccountCreatedEvent @event)
    {
      //this.logger.Log(String.Format("Account created event {0} @ {1}", @event.Name, DateTimeOffset.Now));

      //var session = this.sessionFactory.Create<BalanceMonitorEFSession>();
      //using (var ctx = session.Open())
      //{
      //  Account newAcc = ctx.AccountDailyBalance.Find(@event.AggregateId);
      //  if (newAcc == null)
      //  {
      //    newAcc = new Account()
      //    {
      //      AccountId = @event.AggregateId,
      //      Created = @event.Effective,
      //      Name = @event.Name
      //    };
      //    newAcc.AccountDailyBalances.Add(new AccountDailyBalance
      //    {
      //      AccountId = @event.AggregateId,
      //      Amount = 0M,
      //      Currency = "EUR",
      //      Date = @event.Created
      //    });
      //    ctx.Accounts.Add(newAcc);
      //    ctx.SaveChanges();
      //  }
      //  else
      //  {
      //    throw new Exception(String.Format("Account already exists with Id: {0}", @event.AggregateId));
      //  }
      //}
    }

    public void Handle(AmountDepositedEvent @event)
    {
      //this.logger.Log(String.Format("Account deposited event @ {0}", DateTimeOffset.Now));

      //var session = this.sessionFactory.Create<BalanceMonitorEFSession>();
      //using (var ctx = session.Open())
      //{
      //  Account acc = ctx.Accounts.Find(@event.AggregateId);
      //  if (acc != null)
      //  {
      //    AccountDailyBalance accDlyBal = ctx.AccountDailyBalance.FirstOrDefault(b => b.Date == @event.Created && b.Currency == @event.Amount.Currency);
      //    if (accDlyBal == null)
      //    {
      //      accDlyBal = new AccountDailyBalance { AccountId = acc.AccountId, Amount = @event.Amount.Amount, Currency = @event.Amount.Currency, Date = @event.Created };
      //      acc.AccountDailyBalances.Add(accDlyBal);
      //    }
      //    else
      //    {
      //      accDlyBal.Amount += @event.Amount.Amount;
      //    }
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

      //var session = this.sessionFactory.Create<BalanceMonitorEFSession>();
      //using (var ctx = session.Open())
      //{
      //  Account acc = ctx.AccountDailyBalance.Find(@event.AggregateId);
      //  if (acc != null)
      //  {
      //    AccountDailyBalance accDlyBal = ctx.AccountDailyBalances.FirstOrDefault(b => b.Date == @event.Created && b.Currency == @event.Amount.Currency);
      //    if (accDlyBal == null)
      //    {
      //      accDlyBal = new AccountDailyBalance { AccountId = acc.AccountId, Amount = @event.Amount.Amount, Currency = @event.Amount.Currency, Date = @event.Created };
      //      acc.AccountDailyBalances.Add(accDlyBal);
      //    }
      //    else
      //    {
      //      accDlyBal.Amount -= @event.Amount.Amount;
      //    }
      //    ctx.SaveChanges();
      //  }
      //  else
      //  {
      //    throw new Exception(String.Format("Account does not exist! Id: {0}", @event.AggregateId));
      //  }
      //}
    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(DateTime date)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(Guid accId, DateTime date)
    {
      throw new NotImplementedException();
    }
  }
}
