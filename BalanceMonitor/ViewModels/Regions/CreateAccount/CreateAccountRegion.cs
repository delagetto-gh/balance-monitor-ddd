using BalanceMonitor.Accounting.Application;
using BalanceMonitor.Accounting.Domain.Commands;
using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Utility;
using System;
using System.Collections.ObjectModel;
using WpfCommands = System.Windows.Input;

namespace BalanceMonitor.ViewModels
{
  public class CreateAccountRegion : ObservableViewModel, ICreateAccountRegion
  {
    private readonly IAccountingService accountingService;
    private Guid accountId = Guid.Empty;
    private string accountName;
    private decimal balanceAmount;
    private string balanceCcy;

    public CreateAccountRegion(IAccountingService accountingService)
    {
      this.accountingService = accountingService;
      this.Balance = new ObservableCollection<Money>();
      this.CreateNewAccountCommand = new DelegateCommand(o => this.accountingService.Submit(new CreateAccountCommand((this.accountId = Guid.NewGuid()), this.Name)), o => !String.IsNullOrWhiteSpace(this.Name) && this.accountId == Guid.Empty);
      this.AddBalanceCommand = new DelegateCommand(o => this.Balance.Add(new Money(this.Currency, this.Amount)), o => this.accountId == Guid.Empty);
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

    public ObservableCollection<Money> Balance { get; private set; }

    public string Currency
    {
      get
      {
        return balanceCcy;
      }
      set
      {
        balanceCcy = value;
        this.RaisePropertyChangedEvent("Currency");
      }
    }

    public decimal Amount
    {
      get
      {
        return balanceAmount;
      }
      set
      {
        balanceAmount = value;
        this.RaisePropertyChangedEvent("Amount");
      }
    }

    public WpfCommands.ICommand CreateNewAccountCommand { get; private set; }

    public WpfCommands.ICommand AddBalanceCommand { get; private set; }
  }
}
