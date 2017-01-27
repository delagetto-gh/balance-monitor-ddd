using BalanceMonitor.Accounting.Application;
using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BalanceMonitor.ViewModels
{
  public class AccountAuditRegion : ObservableViewModel, IAccountAuditRegion
  {
    private readonly IAccountingService accountingService;

    private IEnumerable<AccountAudit> accountAudits;

    private DateTime date;

    public AccountAuditRegion(IAccountingService accountingService)
    {
      this.date = DateTime.Today;
      this.accountingService = accountingService;
      this.accountAudits = new List<AccountAudit>();
    }

    public DateTime Date
    {
      get
      {
        return this.date;
      }
      set
      {
        this.date = value;
        this.RaisePropertyChangedEvent("Date");
        this.RaisePropertyChangedEvent("DailyBalance");
      }
    }

    public ObservableCollection<AccountAudit> Audits
    {
      get
      {
        this.accountAudits = this.accountingService.GetAuditOnDate(this.Date);
        return new ObservableCollection<AccountAudit>(this.accountAudits);
      }
    }
  }
}
