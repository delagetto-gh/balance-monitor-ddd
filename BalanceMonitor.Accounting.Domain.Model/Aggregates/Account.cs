using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;


namespace BalanceMonitor.Accounting.Domain.Model
{
  public class Account : EventSourced, IAggregateRoot
  {
    private List<Money> balance = new List<Money>();
    private string name;
    private DateTime created;

    private Account(Guid id, string name, DateTime created, IEnumerable<Money> openingBalance)
      : this()
    {
      this.Apply(new AccountCreatedEvent(id, name, created, openingBalance));
    }

    public Account()
      : base()
    {
      base.Handles<AccountCreatedEvent>(this.OnAccountCreated);
      base.Handles<AmountDepositedEvent>(this.OnAmountDeposited);
      base.Handles<AmountWithdrawalEvent>(this.OnAmountWithdrawn);
    }

    /// <summary>
    /// Create account with no opening balance(s)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="created"></param>
    /// <returns></returns>
    public static Account Create(Guid id, string name, DateTime created)
    {
      return Account.Create(id, name, created, new List<Money>());
    }

    /// <summary>
    /// Create account with an opening balance(s)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="created"></param>
    /// <param name="openingBalance"></param>
    /// <returns></returns>
    public static Account Create(Guid id, string name, DateTime created, IEnumerable<Money> openingBalance)
    {
      return new Account(id, name, created, openingBalance);
    }

    public void Withdraw(string currency, decimal amount)
    {
      if (amount >= 0M)
      {
        this.Apply(new AmountWithdrawalEvent(this.Id, new Money(currency, amount)));
      }
      else
      {
        throw new Exception(String.Format("Cannot withraw a negative number: {0}", amount));
      }
    }

    public void Deposit(string currency, decimal amount)
    {
      if (amount >= 0M)
      {
        this.Apply(new AmountDepositedEvent(this.Id, new Money(currency, amount)));
      }
      else
      {
        throw new Exception(String.Format("Cannot deposit a negative number: {0}", amount));
      }
    }

    private void OnAmountWithdrawn(AmountWithdrawalEvent @event)
    {
      var amount = @event.Amount;
      int idx = this.balance.FindIndex(b => b.Currency == amount.Currency);
      if (idx != -1)
      {
        this.balance[idx].Value -= amount.Value;
      }
      else
      {
        this.balance.Add(new Money(amount.Currency, amount.Value));
      }
    }

    private void OnAmountDeposited(AmountDepositedEvent @event)
    {
      var amount = @event.Amount;
      int idx = this.balance.FindIndex(b => b.Currency == amount.Currency);
      if (idx != -1)
      {
        this.balance[idx].Value += amount.Value;
      }
      else
      {
        this.balance.Add(new Money(amount.Currency, amount.Value));
      }
    }

    private void OnAccountCreated(AccountCreatedEvent @event)
    {
      this.Id = @event.AggregateId;
      this.name = @event.Name;
      this.created = @event.Effective;
      this.balance = new List<Money>(@event.OpeningBalance);
    }
  }
}
