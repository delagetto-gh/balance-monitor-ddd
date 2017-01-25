using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Application.Services.ApplicationServices;
using BalanceMonitor.ViewModels.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BalanceMonitor.ViewModels
{
  public class AccountDailyBalanceRegion : ViewModelBase, IAccountDailyBalanceRegion
  {
    private readonly IAccountingService accountingService;

    private IEnumerable<AccountDailyBalance> dailyBalances;

    private DateTime date;

    public AccountDailyBalanceRegion(IAccountingService accountingService)
    {
      this.date = DateTime.Today;
      this.accountingService = accountingService;
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
