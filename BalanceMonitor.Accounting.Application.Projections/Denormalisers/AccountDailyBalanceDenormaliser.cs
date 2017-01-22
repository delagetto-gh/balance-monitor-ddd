using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Database.Ef;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using System.Linq;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountDailyBalanceDenormaliser : IEventHandler<AccountCreatedEvent>,
                                                  IEventHandler<AmountDepositedEvent>,
                                                  IEventHandler<AmountWithdrawalEvent>
  {
    private readonly ILogger logger;

    public AccountDailyBalanceDenormaliser(ILogger log)
    {
      this.logger = log;
    }

    public void Handle(AccountCreatedEvent @event)
    {
      this.logger.Log(String.Format("Account created event {0} @ {1}", @event.Name, DateTimeOffset.Now));

      using (var ctx = this.CreateSession())
      {
        Account newAcc = ctx.Accounts.Find(@event.AggregateId);
        if (newAcc == null)
        {
          newAcc = new Account()
          {
            AccountId = @event.AggregateId,
            Created = @event.Effective,
            Name = @event.Name
          };
          newAcc.AccountDailyBalances.Add(new AccountDailyBalance
          {
            AccountId = @event.AggregateId,
            Amount = 0M,
            Currency = "EUR",
            Date = @event.Created
          });
          ctx.Accounts.Add(newAcc);
          ctx.SaveChanges();
        }
        else
        {
          throw new Exception(String.Format("Account already exists with Id: {0}", @event.AggregateId));
        }
      }
    }

    public void Handle(AmountDepositedEvent @event)
    {
      this.logger.Log(String.Format("Account deposited event @ {0}", DateTimeOffset.Now));

      using (var ctx = this.CreateSession())
      {
        Account acc = ctx.Accounts.Find(@event.AggregateId);
        if (acc != null)
        {
          AccountDailyBalance accDlyBal = ctx.AccountDailyBalances.FirstOrDefault(b => b.Date == @event.Created && b.Currency == @event.Cash.Currency);
          if (accDlyBal == null)
          {
            accDlyBal = new AccountDailyBalance { AccountId = acc.AccountId, Amount = @event.Cash.Amount, Currency = @event.Cash.Currency, Date = @event.Created };
            acc.AccountDailyBalances.Add(accDlyBal);
          }
          else
          {
            accDlyBal.Amount += @event.Cash.Amount;
          }
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
          AccountDailyBalance accDlyBal = ctx.AccountDailyBalances.FirstOrDefault(b => b.Date == @event.Created && b.Currency == @event.Cash.Currency);
          if (accDlyBal == null)
          {
            accDlyBal = new AccountDailyBalance { AccountId = acc.AccountId, Amount = @event.Cash.Amount, Currency = @event.Cash.Currency, Date = @event.Created };
            acc.AccountDailyBalances.Add(accDlyBal);
          }
          else
          {
            accDlyBal.Amount -= @event.Cash.Amount;
          }
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
