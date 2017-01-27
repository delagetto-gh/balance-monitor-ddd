using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountDailyBalanceSession
  {
     private static ObservableCollection<AccountDailyBalance> AccountAuditsDb = new ObservableCollection<AccountDailyBalance>();

    private readonly ObservableCollection<AccountDailyBalance> accountDailyBalances;


    public AccountDailyBalanceSession()
    {
      //this.iDatabase = somePossibleEfDataBaseContext;
      this.accountDailyBalances = new ObservableCollection<AccountDailyBalance>(AccountAuditsDb);
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

    public ObservableCollection<AccountDailyBalance> AccountDailyBalance
    {
      get
      {
          return this.accountDailyBalances;
      }
    }
  }
}
