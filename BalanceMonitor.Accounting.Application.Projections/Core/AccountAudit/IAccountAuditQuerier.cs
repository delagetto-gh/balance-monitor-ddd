using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public interface IAccountAuditQuerier
  {
    IEnumerable<AccountAudit> GetAuditOnDate(DateTime date);
  }
}
