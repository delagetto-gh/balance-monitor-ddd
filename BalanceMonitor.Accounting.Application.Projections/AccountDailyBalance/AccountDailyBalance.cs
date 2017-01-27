using BalanceMonitor.Accounting.Domain.Common;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountDailyBalance
  {
    public Guid AccountId { get; set; }

    public string AccountName { get; set; }

    public DateTime Date { get; set; }

    public IEnumerable<Money> Balance { get; set; }
  }
}
