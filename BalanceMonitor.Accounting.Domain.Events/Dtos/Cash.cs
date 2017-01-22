using System;

namespace BalanceMonitor.Accounting.Domain.Events
{
  [Serializable]
  public class Cash
  {
    public Cash(string currency, decimal value)
    {
      this.Currency = currency;
      this.Amount = value;
    }

    public string Currency { get; set; }

    public decimal Amount { get; set; }

    private Cash() { }
  }
}
