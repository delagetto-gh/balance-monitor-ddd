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
      this.DispatcherUnhandledException += AppDispatcherUnhandledException;
      this.Startup += OnApplicationStartup;
    }

    private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      MessageBox.Show(e.Exception.Message);
      e.Handled = true;
    }

    private void OnApplicationStartup(object sender, StartupEventArgs e)
    {
      IContainer container = new UnityWrappedIoc(new Microsoft.Practices.Unity.UnityContainer());
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

      //register sessionCtx
      container.Register<ISession<BalanceMonitorAccountingContext>, BalanceMonitorAccountingContext>();

      //registee database repositories
      container.Register<IAccountRepository, AccountRepository>();

      //register cmd Bus
      container.Register<ICommandBus, BalanceMonitorTransactionCommandBus>();
      //register

    }

    private void RegisterApplicationServies(IContainer container)
    {
      container.Register<IAccountingService, AccountingService>();
    }

    private void RegisterApplicationShell(IContainer container)
    {
      container.Register<IShellViewModel, ApplicationShellViewModel>();
    }

  }
}
