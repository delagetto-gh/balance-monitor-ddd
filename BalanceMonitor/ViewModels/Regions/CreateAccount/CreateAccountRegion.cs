using BalanceMonitor.Accounting.Application.Services.ApplicationServices;
using BalanceMonitor.Accounting.Domain.Commands;
using BalanceMonitor.Accounting.Domain.Common;
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

    Guid accountId = new Guid("84492de2-0c4e-4096-974c-0607b568beb1");
    private decimal balanceAmount;
    private string balanceCcy;
    private string accountName;

    public CreateAccountRegion(IAccountingService accSvc)
    {
      this.accSvc = accSvc;
      this.Balance = new ObservableCollection<BalanceViewModel>();
      this.WithdrawAmountCommand = new DelegateCommand((_) => this.accSvc.Submit(new WithdrawMoneyCommand(this.accountId, new Money("GBP", 0))), (_) => true);
      this.CreateNewAccountCommand = new DelegateCommand((_) => this.accSvc.Submit(new CreateAccountCommand(accountId, this.Name)), (_) => !String.IsNullOrWhiteSpace(this.Name));
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

    public WpfCommands.ICommand WithdrawAmountCommand { get; private set; }
  }
}
