using BalanceMonitor.Utility;

namespace BalanceMonitor.ViewModels
{
  public class ApplicationShellViewModel : ObservableViewModel, IShellViewModel
  {
    private readonly ICreateAccountRegion createNewAccountSection;
    private readonly IAccountAuditRegion accountAuditSection;
    private readonly IAccountDailyBalanceRegion dailyalanceSection;

    public ApplicationShellViewModel(IAccountAuditRegion accountListingsSection, ICreateAccountRegion createNewAccountSection, IAccountDailyBalanceRegion dailyBalRegion)
    {
      this.createNewAccountSection = createNewAccountSection;
      this.accountAuditSection = accountListingsSection;
      this.dailyalanceSection = dailyBalRegion;
    }

    public ICreateAccountRegion CreateNewAccountSection
    {
      get { return this.createNewAccountSection; }
    }

    public IAccountAuditRegion AccountAuditSection
    {
      get { return this.accountAuditSection; }
    }

    public IAccountDailyBalanceRegion AccountDailyBalanceSection
    {
        get { return this.dailyalanceSection ; }
    }
  }
}
