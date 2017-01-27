using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountDailyBalanceSession
  {
    private Dictionary<Type, object> iDatabase = new Dictionary<Type, object>();

    private ObservableCollection<AccountDailyBalance> accountDailyBalances;

    private bool IsDirty = true;

    public AccountDailyBalanceSession()
    {
      //this.iDatabase = somePossibleEfDataBaseContext;
      this.accountDailyBalances = new ObservableCollection<AccountDailyBalance>();
      this.accountDailyBalances.CollectionChanged += (sender, @eventArgs) => this.IsDirty = true;
    }

    public ICollection<AccountDailyBalance> AccountDailyBalance
    {
      get
      {
        if (this.IsDirty)
        {
          if (this.iDatabase.ContainsKey(typeof(ICollection<AccountDailyBalance>)))
          {
            var audits = (ICollection<AccountDailyBalance>)this.iDatabase[typeof(ICollection<AccountDailyBalance>)];
            this.accountDailyBalances = new ObservableCollection<AccountDailyBalance>(audits);
          }
          else
          {
            this.accountDailyBalances = new ObservableCollection<AccountDailyBalance>();
          }
          this.IsDirty = false;
        }
        return this.accountDailyBalances;
      }
    }

    public void Commit()
    {
      if (this.iDatabase.ContainsKey(typeof(ICollection<AccountDailyBalance>)))
      {
        this.iDatabase[typeof(ICollection<AccountDailyBalance>)] = this.AccountDailyBalance;
      }
      else
      {
        this.iDatabase.Add(typeof(ICollection<AccountDailyBalance>), this.AccountDailyBalance);
      }
    }

    public void Dispose()
    {
      this.iDatabase = null;
      this.accountDailyBalances = null;
    }
  }
}
