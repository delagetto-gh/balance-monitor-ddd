using System;

namespace BalanceMonitor.Accounting.Domain.Common
{
  [Serializable]
  public class Money
  {
    public Money(string currency, decimal value)
    {
      this.Currency = currency;
      this.Amount = value;
    }

    public string Currency { get; set; }

    public decimal Amount { get; set; }

    private Money() { }
  }
}
