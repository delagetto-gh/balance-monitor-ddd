using BalanceMonitor.Accounting.Application;
using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;

namespace BalanceMonitor.ViewModels
{
  public class AccountAuditRegion : ObservableViewModel, IAccountAuditRegion
  {
    private readonly IAccountingService accountingService;

    private IEnumerable<AccountAudit> accountAudits;

    private DateTime date;

    private Timer dataPoller;

    public AccountAuditRegion(IAccountingService accountingService)
    {
      this.date = DateTime.Today;
      this.accountingService = accountingService;
      this.accountAudits = new List<AccountAudit>();
      this.dataPoller = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
      this.dataPoller.Elapsed += dataPoller_Elapsed;
      this.dataPoller.Start();
    }

    private void dataPoller_Elapsed(object sender, ElapsedEventArgs e)
    {
        this.RaisePropertyChangedEvent("Audits"); //force refresh of data to reflect the new date changed
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
