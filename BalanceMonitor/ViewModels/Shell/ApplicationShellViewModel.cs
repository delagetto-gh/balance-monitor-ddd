using BalanceMonitor.Utility;

namespace BalanceMonitor.ViewModels
{
  public class ApplicationShellViewModel : ObservableViewModel, IShellViewModel
  {
    private readonly ICreateAccountRegion createNewAccountSection;
    private readonly IAccountAuditRegion accountAuditSection;

    public ApplicationShellViewModel(IAccountAuditRegion accountListingsSection, ICreateAccountRegion createNewAccountSection)
    {
      this.createNewAccountSection = createNewAccountSection;
      this.accountAuditSection = accountListingsSection;
    }

    public ICreateAccountRegion CreateNewAccountSection
    {
      get { return this.createNewAccountSection; }
    }

    public IAccountAuditRegion AccountAuditSection
    {
      get { return this.accountAuditSection; }
    }

    public IAccountAuditRegion AccountDailyBalanceSection
    {
      get { return this.accountAuditSection; }
    }
  }
}
