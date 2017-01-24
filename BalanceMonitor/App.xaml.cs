using BalanceMonitor.Accounting;
using BalanceMonitor.Accounting.Application.Commands;
using BalanceMonitor.Accounting.Application.Services.ApplicationServices;
using BalanceMonitor.Accounting.Domain.Model;
using BalanceMonitor.Infrastructure.Core;
using BalanceMonitor.Infrastructure.Core.Logging;
using BalanceMonitor.Infrastructure.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing.Cqrs;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using BalanceMonitor.ViewModels;
using BalanceMonitor.ViewModels.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace BalanceMonitor
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private readonly IContainer iocContainer;

    public App()
    {
      this.iocContainer = new UnityWrappedIoc(new Microsoft.Practices.Unity.UnityContainer());
      this.iocContainer.Register<IContainer, UnityWrappedIoc>();
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
      this.RegisterApplicationShell();
      this.RegisterApplicationInfrastructure();
      this.StartApplication();
    }

    private void RegisterApplicationShell()
    {
      Debug.WriteLine(String.Format("{0}: Setting up Application Shell [{1}].", DateTime.Now, typeof(ApplicationShellView)));

      this.iocContainer.Register<IShellViewModel, ApplicationShellViewModel>();

      Debug.WriteLine(String.Format("{0}: Setting up Application Shell [{1}] Complete!", DateTime.Now, typeof(ApplicationShellView)));
    }

    private void RegisterApplicationInfrastructure()
    {
      //register framework services etc..
      this.iocContainer.Register<ILogger, ConsoleLogger>();
      this.iocContainer.Register<ISessionFactory, BalanceMonitorSessionFactory>();
      this.iocContainer.Register<IAccountingService, AccountingService>();
      this.RegisterCqrsInfrastructure();
    }

    private void RegisterCqrsInfrastructure()
    {
      //registee database (Entifiy Framwork) repositories
      this.iocContainer.Register<IAggregateRootRepository<Account>, EventSourcedAggregateRootRepository<Account>>();

      //register eventstore
      this.iocContainer.Register<IEventStore, EventStoreMsSql>();

      //register cmd/query/event handler factories
      this.iocContainer.Register<ICommandHandlerFactory, CommandHandlerFactory>();
      this.iocContainer.Register<IEventHandlerFactory, EventHandlerFactory>();

      //register application Bus
      this.iocContainer.Register<ICommandBus, DomainEvents>();
      this.iocContainer.Register<IDomainEvent, DomainEvents>();
    }

    private void StartApplication()
    {
      var appWindow = this.iocContainer.Resolve<ApplicationShellView>();
      appWindow.DataContext = this.iocContainer.Resolve<IShellViewModel>();
      appWindow.Show();
    }
  }
}
