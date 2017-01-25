using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Application.Services.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BalanceMonitor.ViewModels.Regions
{
  public class AccountAuditRegion : ViewModelBase, IAccountAuditRegion
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
      }
    }

    public ObservableCollection<AccountAudit> DailyBalance
    {
      get
      {
        this.accountAudits = this.accountingService.GetAuditOnDate(this.Date);
        return new ObservableCollection<AccountAudit>(this.accountAudits);
      }
    }
  }
}
