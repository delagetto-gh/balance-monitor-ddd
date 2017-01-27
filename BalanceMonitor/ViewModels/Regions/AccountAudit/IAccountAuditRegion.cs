using BalanceMonitor.Accounting.Application.Projections;
using System;
using System.Collections.ObjectModel;

namespace BalanceMonitor.ViewModels
{
  public interface IAccountAuditRegion
  {
    ObservableCollection<AccountAudit> Audits { get; }
    DateTime Date { get; set; }
  }
}
