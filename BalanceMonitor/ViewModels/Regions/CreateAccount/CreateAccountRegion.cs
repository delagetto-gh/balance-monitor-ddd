using BalanceMonitor.Accounting.Application.Commands;
using BalanceMonitor.Accounting.Application.Services.ApplicationServices;
using BalanceMonitor.Utility;
using BalanceMonitor.ViewModels.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfCommands = System.Windows.Input;

namespace BalanceMonitor.ViewModels
{
  public class CreateAccountRegion : ViewModelBase, ICreateAccountRegion
  {
    private readonly IAccountingService accSvc;

    private decimal balanceAmount;
    private string balanceCcy;
    private string accountName;

    public CreateAccountRegion(IAccountingService accSvc)
    {
      this.accSvc = accSvc;
      this.Balance = new ObservableCollection<BalanceViewModel>();
      this.AddNewBalanceCommand = new DelegateCommand((_) => this.Balance.Add(new BalanceViewModel(this.BalanceCurrency, this.BalanceAmount)), (_) => !String.IsNullOrWhiteSpace(this.BalanceCurrency) && !this.Balance.Any(b => b.Currency == this.BalanceCurrency));
      this.CreateNewAccountCommand = new DelegateCommand((_) => this.accSvc.Submit(new CreateAccountCommand(Guid.NewGuid(), this.Name)), (_) => !String.IsNullOrWhiteSpace(this.Name));
    }

    public string Name
    {
      get
      {
        return this.accountName;
      }
      set
      {
        this.accountName = value;
        this.RaisePropertyChangedEvent("Name");
      }
    }

    public ObservableCollection<BalanceViewModel> Balance { get; private set; }

    public string BalanceCurrency
    {
      get
      {
        return balanceCcy;
      }
      set
      {
        balanceCcy = value;
        this.RaisePropertyChangedEvent("BalanceCurrency");
      }
    }

    public decimal BalanceAmount
    {
      get
      {
        return balanceAmount;
      }
      set
      {
        balanceAmount = value;
        this.RaisePropertyChangedEvent("BalanceAmount");
      }
    }

    public WpfCommands.ICommand CreateNewAccountCommand { get; private set; }

    public WpfCommands.ICommand AddNewBalanceCommand { get; private set; }
  }
}
