using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountAuditSession
  {
    private static ObservableCollection<AccountAudit> AccountAuditsDb = new ObservableCollection<AccountAudit>();

    private readonly ObservableCollection<AccountAudit> accountAuditCurrent;

    public AccountAuditSession()
    {
      this.accountAuditCurrent = new ObservableCollection<AccountAudit>();
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

    public ICollection<AccountAudit> AccountAudits
    {
      get { return this.accountAuditCurrent; }
    }
  }
}
