using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BalanceMonitor.ViewModels.Regions
{
  public interface ICreateAccountRegion
  {
    ObservableCollection<BalanceViewModel> Balance { get; }
    decimal BalanceAmount { get; set; }
    string BalanceCurrency { get; set; }
    ICommand CreateNewAccountCommand { get; }
    ICommand WithdrawAmountCommand { get; }
    string Name { get; set; }
  }
}
