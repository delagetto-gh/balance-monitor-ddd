using System;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountDailyBalance
  {
    public Guid AccountId { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
  }
}
