using BalanceMonitor.Accounting.Domain.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BalanceMonitor.ViewModels
{
  public interface ICreateAccountRegion
  {
    ObservableCollection<Money> Balance { get; } //should really be a money viewmodel but.. meh
    decimal Amount { get; set; }
    string Currency { get; set; }
    ICommand CreateNewAccountCommand { get; }
    ICommand AddBalanceCommand { get; }
    string Name { get; set; }
  }
}
