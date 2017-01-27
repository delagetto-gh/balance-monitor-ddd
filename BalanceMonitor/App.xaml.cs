using BalanceMonitor.Accounting.Application.CommonHandlers;
using BalanceMonitor.Accounting.Application.Projections;
using BalanceMonitor.Accounting.Application.Projections.Interfaces;
using BalanceMonitor.Accounting.Application.Services.ApplicationServices;
using BalanceMonitor.Accounting.Domain.Commands;
using BalanceMonitor.Accounting.Domain.Events;
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
using System;
using System.Linq;
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
      container.Register<ILogger, BalanceMonitorDebugLogger>();

      //register eventstore
      container.Register<IEventStore, BalanceMonitorXmlEventStore>();

      container.Register<IHandleEvents<AccountCreatedEvent>, AccountAuditDenormaliser>();
      container.Register<IHandleEvents<AmountWithdrawalEvent>, AccountAuditDenormaliser>();
  
      //register eventPublisher
      container.Register<IDomainEvents, BalanceMonitorDomainEvents>();

      //register sessions
      container.Register<AccountDailyBalanceSession>();
      container.Register<AccountAuditSession>();

      //registee domain repositories
      container.Register<IAccountRepository, AccountRepository>();

      //register cmd Bus
      container.Register<ICommandBus, BalanceMonitorTransactionScopeCommandBus>();

      //register eventHandlers / queryHandlers
      container.Register<IAccountDailyBalanceQuerier, AccountDailyBalanceDenormaliser>();
      container.Register<IAccountAuditQuerier, AccountAuditDenormaliser>();

      //registeer command handler(s)
      container.Register<ICommandHandler<CreateAccountCommand>, BalanceMonitorAccountingCommandHandler>();
      container.Register<ICommandHandler<WithdrawMoneyCommand>, BalanceMonitorAccountingCommandHandler>();


      //var eventHandlerAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.GetTypes().Any(t => typeof(IEventHandler<>).IsAssignableFrom(t)));
      //foreach (var assembly in eventHandlerAssemblies)
      //{
      //  var eventHandlerTypes = assembly.GetTypes().Where(o => typeof(IEventHandler<>).IsAssignableFrom(o));
      //  foreach (var evtHlr in eventHandlerTypes)
      //  {
      //     container.RegisterInstance<I
      //  }
      //}
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
