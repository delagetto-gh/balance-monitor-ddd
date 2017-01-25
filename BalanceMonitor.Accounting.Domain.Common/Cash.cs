using System;
using System.Runtime.Serialization;

namespace BalanceMonitor.Accounting.Domain.Common
{
  [DataContract]
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
