using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections.Interfaces
{
  public interface IAccountAuditQuerier
  {
    IEnumerable<AccountAudit> GetAuditOnDate(DateTime date);
  }
}
