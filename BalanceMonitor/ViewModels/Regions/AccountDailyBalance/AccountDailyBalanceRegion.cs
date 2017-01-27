using BalanceMonitor.Accounting.Application;
using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BalanceMonitor.ViewModels
{
  public class AccountDailyBalanceRegion : ObservableViewModel, IAccountDailyBalanceRegion
  {
    private readonly IAccountingService accountingService;

    private IEnumerable<AccountDailyBalance> dailyBalances;

    private DateTime date;

    public AccountDailyBalanceRegion(IAccountingService accountingService)
    {
      this.accountingService = accountingService;
      this.date = DateTime.Today;
      this.dailyBalances = new List<AccountDailyBalance>();
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
        this.RaisePropertyChangedEvent("DailyBalance"); //force refresh of data to reflect the new date changed
      }
    }

    public ObservableCollection<AccountDailyBalance> DailyBalance
    {
      get
      {
        this.dailyBalances = this.accountingService.GetAccountBalanceOn(this.Date);
        return new ObservableCollection<AccountDailyBalance>(this.dailyBalances);
      }
    }
  }
}
