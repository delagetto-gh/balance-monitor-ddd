namespace BalanceMonitor.ViewModels
{
  public class BalanceViewModel : ViewModelBase
  {
    private string currency;
    private decimal amount;

    public BalanceViewModel(string currency, decimal amount)
    {
      this.currency = currency;
      this.amount = amount;
    }

    public string Currency
    {
      get { return this.currency; }
      set
      {
        if (this.currency != value)
        {
          this.currency = value;
          this.RaisePropertyChangedEvent("Currency");
        }
      }
    }

    public decimal Amount
    {
      get { return this.amount; }
      set
      {
        if (this.amount != value)
        {
          this.amount = value;
          this.RaisePropertyChangedEvent("Amount");
        }
      }
    }
  }
}
