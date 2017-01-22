using BalanceMonitor.Accounting;
using BalanceMonitor.Accounting.Application.Commands;
using BalanceMonitor.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfCommands = System.Windows.Input;

namespace BalanceMonitor.ViewModels
{
  public class CreateAccountRegion : ViewModelBase
  {
    private readonly IAccountingService accSvc;

    public CreateAccountRegion(IAccountingService accSvc)
    {
      this.accSvc = accSvc;
      this.Balance = new ObservableCollection<BalanceViewModel>();
      this.AddNewBalanceCommand = new DelegateCommand((_) => this.Balance.Add(new BalanceViewModel(this.NewBalanceCurrency, this.NewBalanceAmount)), (_) => !String.IsNullOrWhiteSpace(this.NewBalanceCurrency) && !this.Balance.Any(b => b.Currency == this.NewBalanceCurrency));
      this.CreateNewAccountCommand = new DelegateCommand((_) => this.accSvc.Command.SendCommand(new CreateAccountCommand(Guid.NewGuid(), this.Name)), (_) => !String.IsNullOrWhiteSpace(this.Name));
    }

    private string name;
    public string Name
    {
      get { return this.name; }
      set
      {
        this.name = value;
        this.RaisePropertyChangedEvent("Name");
      }
    }

    public ObservableCollection<BalanceViewModel> Balance { get; private set; }

    private string newBalanceCcy;
    public string NewBalanceCurrency
    {
      get { return newBalanceCcy; }
      set
      {
        newBalanceCcy = value;
        this.RaisePropertyChangedEvent("NewBalanceCurrency");
      }
    }

    private decimal newBalanceAmount;
    public decimal NewBalanceAmount
    {
      get { return newBalanceAmount; }
      set
      {
        newBalanceAmount = value;
        this.RaisePropertyChangedEvent("NewBalanceAmount");
      }
    }

    public WpfCommands.ICommand CreateNewAccountCommand { get; private set; }

    public WpfCommands.ICommand AddNewBalanceCommand { get; private set; }
  }
}
