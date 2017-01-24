using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Database.Ef;
using System.Collections.ObjectModel;

namespace BalanceMonitor.ViewModels
{
  public class AccountAuditRegion : ViewModelBase
  {
    private readonly IAccountingQueryService accountingService;

    public AccountAuditRegion(IAccountingQueryService accountingService)
    {
      this.AccountAudits = new ObservableCollection<AccountAudit>();
      this.accountingService = accountingService;
    }

    public ObservableCollection<AccountAudit> AccountAudits { get; private set; }
  }
}
