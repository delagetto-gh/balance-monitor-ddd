using System;
using System.Runtime.Serialization;

namespace BalanceMonitor.Accounting.Domain.Common
{
  public class Money
  {
    public Money(string currency, decimal value)
    {
      this.Currency = currency;
      this.Value = value;
    }

    public string Currency { get; set; }

    public decimal Value { get; set; }

    private Money() { }
  }
}
