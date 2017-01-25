using BalanceMonitor.Accounting.Domain.Common;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountAudit
  {
    public Guid AccountId { get; set; }

    public string AccountName { get; set; }

    public DateTime Time { get; set; }

    public string Action { get; set; }
  }
}
