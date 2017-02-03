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
    private readonly Timer dataPoller;
    private readonly IAccountingService accountingService;

    private DateTime date;
    private ICommand depositAmountCommand;
    private ICommand withdrawAmountCommand;
    private IEnumerable<AccountDailyBalance> dailyBalances;

    public AccountDailyBalanceRegion(IAccountingService accountingService)
    {
      this.accountingService = accountingService;
      this.date = DateTime.Today;
      this.dailyBalances = new List<AccountDailyBalance>();
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

    public ICommand DepositAmountCommand
    {
      get
      {
        if (this.depositAmountCommand == null)
        {
          this.depositAmountCommand = new DelegateCommand(o => this.accountingService.Submit(new DepositAmountCommand(dailyBalances.First().AccountId, new Accounting.Domain.Common.Money("GBP", 10M))), (o) => this.dailyBalances.Any());
        }
        return this.depositAmountCommand;
      }
    }

    public ICommand WithdrawAmountCommand
    {
      get
      {
        if (this.withdrawAmountCommand == null)
        {
          this.withdrawAmountCommand = new DelegateCommand(o => this.accountingService.Submit(new WithdrawAmountCommand(dailyBalances.First().AccountId, new Accounting.Domain.Common.Money("GBP", 10M))), (o) => this.dailyBalances.Any());
        }
        return this.withdrawAmountCommand;
      }
    }
  }
}
