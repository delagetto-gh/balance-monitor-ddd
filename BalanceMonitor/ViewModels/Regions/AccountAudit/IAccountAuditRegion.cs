using System;
using System.Collections.ObjectModel;
using BalanceMonitor.Accounting.Application.Projections;

namespace BalanceMonitor.ViewModels.Regions
{
  public interface IAccountAuditRegion
  {
    ObservableCollection<AccountAudit> DailyBalance { get; }
    DateTime Date { get; set; }
  }
}
