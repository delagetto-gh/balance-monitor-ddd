using BalanceMonitor.Infrastructure.Interfaces.Ioc;

namespace BalanceMonitor.ViewModels.Shell
{
  public class ApplicationShellViewModel : ViewModelBase, IShellViewModel
  {
    private readonly ViewModelBase createNewAccountSection;
    private readonly ViewModelBase accountListingsSection;

    public ApplicationShellViewModel(IContainer iocContainer)
    {
      this.createNewAccountSection = iocContainer.Resolve<CreateAccountRegion>();
      this.accountListingsSection = iocContainer.Resolve<AccountAuditRegion>();
    }

    public ViewModelBase CreateNewAccountSection
    {
      get { return this.createNewAccountSection; }
    }

    public ViewModelBase AccountListingsSection
    {
      get { return this.accountListingsSection; }
    }
  }
}
