using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountAuditSession : ISession
  {
    private Dictionary<Type, object> iDatabase = new Dictionary<Type, object>();

    private ObservableCollection<AccountAudit> accountAudits;

    private bool IsDirty = true;

    public AccountAuditSession()
    {
      //this.iDatabase = somePossibleEfDataBaseContext;
      this.accountAudits = new ObservableCollection<AccountAudit>();
      this.accountAudits.CollectionChanged += (sender, @eventArgs) => this.IsDirty = true;
    }

    public ICollection<AccountAudit> AccountAudits
    {
      get
      {
        if (this.IsDirty)
        {
          if (this.iDatabase.ContainsKey(typeof(ICollection<AccountAudit>)))
          {
            var audits = (ICollection<AccountAudit>)this.iDatabase[typeof(ICollection<AccountAudit>)];
            this.accountAudits = new ObservableCollection<AccountAudit>(audits);
          }
          else
          {
            this.accountAudits = new ObservableCollection<AccountAudit>();
          }
          this.IsDirty = false;
        }
        return this.accountAudits;
      }
    }

    public void Commit()
    {
      if (this.iDatabase.ContainsKey(typeof(ICollection<AccountAudit>)))
      {
        this.iDatabase[typeof(ICollection<AccountAudit>)] = this.AccountAudits;
      }
      else
      {
        this.iDatabase.Add(typeof(ICollection<AccountAudit>), this.AccountAudits);
      }
    }

    public void Dispose()
    {
      this.iDatabase = null;
      this.accountAudits = null;
    }
  }
}
