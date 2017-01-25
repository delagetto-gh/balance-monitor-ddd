using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections.Interfaces
{
  public interface IAccountAuditQuerier
  {
    IEnumerable<AccountDailyBalance> GetAuditForAccountOnDate(Guid accId, DateTime date);
  }
}
