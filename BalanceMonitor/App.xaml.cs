using BalanceMonitor.Accounting.Application;
using BalanceMonitor.Accounting.Application.CommonHandlers;
using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Application.Projections.InMemory;
using BalanceMonitor.Accounting.Application.Services;
using BalanceMonitor.Accounting.Domain.Commands;
using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Accounting.Domain.Model.Repositories;
using BalanceMonitor.Accounting.Domain.Services;
using BalanceMonitor.Infrastructure.Core;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using BalanceMonitor.Infrastructure.Core.Logging;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using BalanceMonitor.ViewModels;
using System.Windows;
using System.Windows.Threading;

namespace BalanceMonitor
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public App()
    {
      this.DispatcherUnhandledException += OnAppicationUnhandledException;
      this.Startup += OnApplicationStartup;
    }

    private void OnAppicationUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      MessageBox.Show(e.Exception.Message);
      e.Handled = true;
    }

    private void OnApplicationStartup(object sender, StartupEventArgs e)
    {
      IContainer container = new BalanceMonitorIoc(new Microsoft.Practices.Unity.UnityContainer());
      container.RegisterInstance<IContainer>(container);

      this.RegisterApplicationInfrastructure(container);
      this.RegisterApplicationServies(container);
      this.RegisterApplicationShell(container);

      Application.Current.MainWindow = new BalanceMonitorShellView();
      Application.Current.MainWindow.DataContext = container.Resolve<IShellViewModel>();
      Application.Current.MainWindow.Show();
    }

    private void RegisterApplicationInfrastructure(IContainer container)
    {
      //register framework services etc..
      container.Register<ILogger, BalanceMonitorDebugLogger>();

      //register eventstore
      container.Register<IEventStore, BalanceMonitorXmlEventStore>();

      container.Register<IHandleEvents<AccountCreatedEvent>, AccountAuditDenormaliser>();
      container.Register<IHandleEvents<AmountWithdrawalEvent>, AccountAuditDenormaliser>();

      //register eventPublisher
      container.Register<IDomainEvents, BalanceMonitorDomainEvents>();

      //registee domain repositories
      container.Register<IAccountRepository, AccountRepository>();

      //register cmd Bus
      container.Register<ICommandBus, BalanceMonitorTransactionScopeCommandBus>();

      //register eventHandlers / queryHandlers
      container.Register<IAccountDailyBalanceQuerier, AccountDailyBalanceDenormaliser>();
      container.Register<IAccountAuditQuerier, AccountAuditDenormaliser>();

      //registeer command handler(s)
      container.Register<ICommandHandler<CreateAccountCommand>, BalanceMonitorAccountingCommandHandler>();
      container.Register<ICommandHandler<WithdrawAmountCommand>, BalanceMonitorAccountingCommandHandler>();
      container.Register<ICommandHandler<DepositAmountCommand>, BalanceMonitorAccountingCommandHandler>();
    }

    private void RegisterApplicationServies(IContainer container)
    {
      container.Register<IAccountingService, AccountingService>();
    }

    private void RegisterApplicationShell(IContainer container)
    {
      container.Register<ICreateAccountRegion, CreateAccountRegion>();
      container.Register<IAccountAuditRegion, AccountAuditRegion>();
      container.Register<IAccountDailyBalanceRegion, AccountDailyBalanceRegion>();
      container.Register<IShellViewModel, BalanceMonitorShellViewModel>();
    }
  }
}
