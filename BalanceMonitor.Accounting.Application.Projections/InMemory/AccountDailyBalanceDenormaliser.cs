using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace BalanceMonitor.Accounting.Application.Projections.InMemory
{
  public class AccountDailyBalanceDenormaliser : IAccountDailyBalanceQuerier,
                                                 IHandleEvents<AmountDepositedEvent>,
                                                 IHandleEvents<AmountWithdrawalEvent>,
                                                 IHandleEvents<AccountCreatedEvent>
  {

    private static ObservableCollection<AccountDailyBalance> AccountDailyBalanceDb = new ObservableCollection<AccountDailyBalance>();

    private readonly ObservableCollection<AccountDailyBalance> accountDailyBalances;


    public AccountDailyBalanceDenormaliser()
    {
      this.accountDailyBalances = new ObservableCollection<AccountDailyBalance>(AccountDailyBalanceDb);
      this.accountDailyBalances.CollectionChanged += OnAccountAuditCollectionChanged;
    }

    private void OnAccountAuditCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          foreach (var newAudit in e.NewItems)
          {
            var audit = (AccountDailyBalance)newAudit;
            AccountDailyBalanceDb.Add(audit);
          }
          break;
        case NotifyCollectionChangedAction.Remove:
          AccountDailyBalanceDb.RemoveAt(e.NewStartingIndex);
          break;
        default:
          break;
      }
    }

    public void Handle(AccountCreatedEvent @event)
    {
      var newAcc = this.accountDailyBalances.FirstOrDefault(o => o.AccountId == @event.AggregateId);
      if (newAcc == null)
      {
        newAcc = new AccountDailyBalance()
        {
          AccountId = @event.AggregateId,
          AccountName = @event.Name,
          Date = @event.DateOccured,
          Balance = new List<Money>(@event.OpeningBalance)
        };
        this.accountDailyBalances.Add(newAcc);
      }
      else
      {
        throw new Exception(String.Format("Account already exists with Id: {0}", @event.AggregateId));
      }
    }

    public void Handle(AmountDepositedEvent @event)
    {
      var account = this.accountDailyBalances.FirstOrDefault(o => o.AccountId == @event.AggregateId);
      if (account != null)
      {
        account.Date = @event.DateOccured;
        account.AccountName = account.AccountName;

        var accBalance = account.Balance.SingleOrDefault(o => o.Currency == @event.Amount.Currency);
        if (accBalance != null)
        {
          int idx = account.Balance.IndexOf(accBalance);
          account.Balance[idx] = new Money(accBalance.Currency, (accBalance.Value + @event.Amount.Value));
        }
        else
        {
          accBalance = new Money(@event.Amount.Currency, @event.Amount.Value);
          account.Balance.Add(accBalance);
        }
      }
      else
      {
        throw new Exception(String.Format("Account does not exists with Id: {0}", @event.AggregateId));
      }
    }

    public void Handle(AmountWithdrawalEvent @event)
    {
      var account = this.accountDailyBalances.FirstOrDefault(o => o.AccountId == @event.AggregateId);
      if (account != null)
      {
        account.Date = @event.DateOccured;
        account.AccountName = account.AccountName;

        var accBalance = account.Balance.SingleOrDefault(o => o.Currency == @event.Amount.Currency);
        if (accBalance != null)
        {
          int idx = account.Balance.IndexOf(accBalance);
          account.Balance[idx] = new Money(accBalance.Currency, (accBalance.Value - @event.Amount.Value));
        }
        else
        {
          accBalance = new Money(@event.Amount.Currency, @event.Amount.Value);
          account.Balance.Add(accBalance);
        }
      }
      else
      {
        throw new Exception(String.Format("Account does not exists with Id: {0}", @event.AggregateId));
      }
    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(DateTime date)
    {
      return AccountDailyBalanceDb.Where(o => o.Date.Date == date.Date);
    }

    public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(Guid accId, DateTime date)
    {
      return AccountDailyBalanceDb.Where(o => o.Date.Date == date.Date && o.AccountId == accId);
    }
  }
}
