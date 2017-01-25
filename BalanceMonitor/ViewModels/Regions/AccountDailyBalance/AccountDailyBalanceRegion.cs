using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Application.Services.ApplicationServices;
using System.Collections.ObjectModel;

namespace BalanceMonitor.ViewModels
{
  public class AccountDailyBalanceRegion : ViewModelBase
  {
    private readonly IAccountingService accountingService;

    public AccountDailyBalanceRegion(IAccountingService accountingService)
    {
      this.DailyBalance = new ObservableCollection<AccountDailyBalance>();
      this.accountingService = accountingService;
    }

    public ObservableCollection<AccountDailyBalance> DailyBalance { get; private set; }
  }
}
