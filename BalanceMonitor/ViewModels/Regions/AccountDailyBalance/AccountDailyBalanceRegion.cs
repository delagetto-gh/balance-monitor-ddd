using BalanceMonitor.Accounting.Application;
using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Domain.Commands;
using BalanceMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Input;

namespace BalanceMonitor.ViewModels
{
  public class AccountDailyBalanceRegion : ObservableViewModel, IAccountDailyBalanceRegion
  {
    private readonly IAccountingService accountingService;

    private IEnumerable<AccountDailyBalance> dailyBalances;

    private DateTime date;
    private Timer dataPoller;

    public AccountDailyBalanceRegion(IAccountingService accountingService)
    {
      this.accountingService = accountingService;
      this.date = DateTime.Today;
      this.dailyBalances = new List<AccountDailyBalance>();
      this.WithdrawAmountCommand = new DelegateCommand(o => this.accountingService.Submit(new WithdrawMoneyCommand(dailyBalances.First().AccountId, new Accounting.Domain.Common.Money("GBP", (decimal)o))), (o) => this.dailyBalances.Any());
      this.dataPoller = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
      this.dataPoller.Elapsed += dataPoller_Elapsed;
      this.dataPoller.Start();
    }

    private void dataPoller_Elapsed(object sender, ElapsedEventArgs e)
    {
      this.RaisePropertyChangedEvent("DailyBalance"); //force refresh of data to reflect the new date changed
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

    public ICommand WithdrawAmountCommand { get; set; }
  }
}
