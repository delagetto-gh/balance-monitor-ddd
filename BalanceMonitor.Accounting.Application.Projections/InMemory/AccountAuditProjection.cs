using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace BalanceMonitor.Accounting.Application.Projections.InMemory
{
  public class AccountAuditDenormaliser : IAccountAuditQuerier,
                                          IHandleEvents<AccountCreatedEvent>,
                                          IHandleEvents<AmountDepositedEvent>,
                                          IHandleEvents<AmountWithdrawalEvent>
  {
    private static ObservableCollection<AccountAudit> AccountAuditsDb = new ObservableCollection<AccountAudit>();

    private readonly ObservableCollection<AccountAudit> accountAuditCurrent;

    public AccountAuditDenormaliser()
    {
      this.accountAuditCurrent = new ObservableCollection<AccountAudit>(AccountAuditsDb);
      this.accountAuditCurrent.CollectionChanged += OnAccountAuditCollectionChanged;
    }

    private void OnAccountAuditCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          foreach (var newAudit in e.NewItems)
          {
            var audit = (AccountAudit)newAudit;
            AccountAuditsDb.Add(audit);
          }
          break;
        case NotifyCollectionChangedAction.Remove:
          AccountAuditsDb.RemoveAt(e.NewStartingIndex);
          break;
        default:
          break;
      }
    }

    public IEnumerable<AccountAudit> GetAuditOnDate(DateTime date)
    {
      return AccountAuditsDb.Where(o => o.Time.Date == date.Date).ToList();
    }

    public void Handle(AmountDepositedEvent @event)
    {
      var account = this.accountAuditCurrent.FirstOrDefault(o => o.AccountId == @event.AggregateId);
      if (account != null)
      {
        AccountAudit accAudit = new AccountAudit
        {
          AccountId = @event.AggregateId,
          AccountName = account.AccountName,
          Action = "Amount Deposited",
          Time = @event.DateOccured
        };
        this.accountAuditCurrent.Add(accAudit);
      }
      else
      {
        throw new Exception(String.Format("No account found with Id: {0}", @event.AggregateId));
      }
    }

    public void Handle(AmountWithdrawalEvent @event)
    {
      var account = this.accountAuditCurrent.FirstOrDefault(o => o.AccountId == @event.AggregateId);
      if (account != null)
      {
        AccountAudit accAudit = new AccountAudit
        {
          AccountId = @event.AggregateId,
          AccountName = account.AccountName,
          Action = "Amount Withdrawn",
          Time = @event.DateOccured
        };
        this.accountAuditCurrent.Add(accAudit);
      }
      else
      {
        throw new Exception(String.Format("No account found with Id: {0}", @event.AggregateId));
      }
    }

    public void Handle(AccountCreatedEvent @event)
    {
      var account = this.accountAuditCurrent.FirstOrDefault(o => o.AccountId == @event.AggregateId);
      if (account == null)
      {
        AccountAudit accAudit = new AccountAudit
        {
          AccountId = @event.AggregateId,
          AccountName = @event.Name,
          Action = "Account Created",
          Time = @event.DateOccured
        };
        this.accountAuditCurrent.Add(accAudit);
      }
      else
      {
        throw new Exception(String.Format("Account does not exist! Id: {0}", @event.AggregateId));
      }
    }
  }
}
