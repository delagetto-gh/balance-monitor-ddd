using BalanceMonitor.Accounting.Application.Commands;
using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Application.Projections.Interfaces;
using BalanceMonitor.Accounting.Application.Services.ApplicationServices;
using BalanceMonitor.Accounting.Domain.Services;
using BalanceMonitor.Infrastructure.Core;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using BalanceMonitor.Infrastructure.Core.Logging;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using BalanceMonitor.ViewModels;
using BalanceMonitor.ViewModels.Regions;
using BalanceMonitor.ViewModels.Shell;
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

      Application.Current.MainWindow = new ApplicationShellView();
      Application.Current.MainWindow.DataContext = container.Resolve<IShellViewModel>();
      Application.Current.MainWindow.Show();
    }

    private void RegisterApplicationInfrastructure(IContainer container)
    {
      //register framework services etc..
      container.Register<ILogger, DebugLogger>();

      //register eventstore
      container.Register<IEventStore, BalanceMonitorXmlEventStore>();

      //register eventPublisher
      container.Register<IDomainEvents, BalanceMonitorDomainEvents>();

      //register sessionFactory
      container.Register<ISessionFactory, BalanceMonitorSessionFactory>();

      //register sessions
      container.Register<BalanceMonitorAccountingSession>();
      container.Register<AccountDailyBalanceSession>();
      container.Register<AccountAuditSession>();

      //registee domain repositories
      container.Register<IAccountRepository, AccountRepository>();

      //register cmd Bus
      container.Register<ICommandBus, BalanceMonitorTransactionCommandBus>();

      //register eventHandlers / queryHandlers
      container.Register<IAccountDailyBalanceQuerier, AccountDailyBalanceDenormaliser>();
      container.Register<IAccountAuditQuerier, AccountAuditDenormaliser>();

      //registeer command handler(s)
      container.Register<ICommandHandler<CreateAccountCommand>, BalanceMonitorAccountingCommandHandler>();
      container.Register<ICommandHandler<HelloWorldCommand>, BalanceMonitorAccountingCommandHandler>();

    }

    private void RegisterApplicationServies(IContainer container)
    {
      container.Register<IAccountingService, AccountingService>();
    }

    private void RegisterApplicationShell(IContainer container)
    {
      container.Register<ICreateAccountRegion, CreateAccountRegion>();
      container.Register<IAccountAuditRegion, AccountAuditRegion>();
      container.Register<IShellViewModel, ApplicationShellViewModel>();
    }
  }
}
