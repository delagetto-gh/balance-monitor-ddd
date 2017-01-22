using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Database.Ef;
using System.Collections.ObjectModel;

namespace BalanceMonitor.ViewModels
{
  public class AccountDailyBalanceRegion : ViewModelBase
  {
    public AccountDailyBalanceRegion()
    {
      this.DailyBalance = new ObservableCollection<AccountDailyBalance>();
    }

    public ObservableCollection<AccountDailyBalance> DailyBalance { get; private set; }
  }
}
