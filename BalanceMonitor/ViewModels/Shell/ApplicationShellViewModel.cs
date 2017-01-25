using BalanceMonitor.ViewModels.Regions;
namespace BalanceMonitor.ViewModels.Shell
{
  public class ApplicationShellViewModel : ViewModelBase, IShellViewModel
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

    public IAccountAuditRegion AccountListingsSection
    {
      get { return this.accountAuditSection; }
    }
  }
}
