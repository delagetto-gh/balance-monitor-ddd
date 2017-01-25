using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Application.Services.ApplicationServices;
using System.Collections.ObjectModel;

namespace BalanceMonitor.ViewModels
{
  public class AccountAuditRegion : ViewModelBase
  {
    private readonly IAccountingService accountingService;

    public AccountAuditRegion(IAccountingService accountingService)
    {
      this.AccountAudits = new ObservableCollection<AccountAudit>();
      this.accountingService = accountingService;
    }

    public ObservableCollection<AccountAudit> AccountAudits { get; private set; }
  }
}
