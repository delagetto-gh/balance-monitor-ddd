namespace BalanceMonitor.ViewModels.Shell
{
  public class ApplicationShellViewModel : ViewModelBase, IShellViewModel
  {
    private readonly CreateAccountRegion createNewAccountSection;
    private readonly AccountAuditRegion accountAuditSection;

    public ApplicationShellViewModel(AccountAuditRegion accountListingsSection, CreateAccountRegion createNewAccountSection)
    {
      this.createNewAccountSection = createNewAccountSection;
      this.accountAuditSection = accountListingsSection;
    }

    public CreateAccountRegion CreateNewAccountSection
    {
      get { return this.createNewAccountSection; }
    }

    public AccountAuditRegion AccountListingsSection
    {
      get { return this.accountAuditSection; }
    }
  }
}
