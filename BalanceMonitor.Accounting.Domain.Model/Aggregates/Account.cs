using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;


namespace BalanceMonitor.Accounting.Domain.Model
{
  public class Account : EsAggregateRoot
  {
    private List<Cash> balance;
    private string name;
    private DateTime created;

    private Account(Guid id, string name, DateTime created)
      : this()
    {
      this.Apply(new AccountCreatedEvent(id, name, created));
    }

    public Account()
      : base()
    {
      this.balance = new List<Cash>();
      base.Handles<AccountCreatedEvent>(this.OnAccountCreated);
      base.Handles<AmountDepositedEvent>(this.OnAmountDeposited);
      base.Handles<AmountWithdrawalEvent>(this.OnAmountWithdrawn);
    }

    public static Account Create(Guid id, string name, DateTime created)
    {
      return new Account(id, name, created);
    }

    public void Withdraw(string currency, decimal amount)
    {
      if (amount < 0M)
      {
        this.Apply(new AmountWithdrawalEvent(this.Id, new Cash(currency, amount)));
      }
      else
      {
        throw new Exception(String.Format("Cannot withraw a negative number: {0}", amount));
      }
    }

    public void Deposit(string currency, decimal amount)
    {
      if (amount < 0M)
      {
        this.Apply(new AmountDepositedEvent(this.Id, new Cash(currency, amount)));
      }
      else
      {
        throw new Exception(String.Format("Cannot withraw a negative number: {0}", amount));
      }
    }

    private void OnAmountWithdrawn(AmountWithdrawalEvent @event)
    {
      var amount = @event.Cash;
      int idx = this.balance.FindIndex(b => b.Currency == amount.Currency);
      if (idx != -1)
      {
        this.balance[idx].Amount -= amount.Amount;
      }
      else
      {
        this.balance.Add(new Cash(amount.Currency, amount.Amount));
      }
    }

    private void OnAmountDeposited(AmountDepositedEvent @event)
    {
      var amount = @event.Cash;
      int idx = this.balance.FindIndex(b => b.Currency == amount.Currency);
      if (idx != -1)
      {
        this.balance[idx].Amount += amount.Amount;
      }
      else
      {
        this.balance.Add(new Cash(amount.Currency, amount.Amount));
      }
    }

    private void OnAccountCreated(AccountCreatedEvent @event)
    {
      this.Id = @event.AggregateId;
      this.name = @event.Name;
      this.created = @event.Effective;
    }
  }
}
