using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorIoc : IContainer
  {
    private readonly IUnityContainer ulUnityContainer;

    public BalanceMonitorIoc(IUnityContainer unityContainer)
    {
      this.ulUnityContainer = unityContainer;
    }

    public TInterface Resolve<TInterface>()
    {
      return this.ulUnityContainer.Resolve<TInterface>();
    }

    public IEnumerable<TInterface> ResolveAll<TInterface>()
    {
      return this.ulUnityContainer.ResolveAll<TInterface>();
    }

    public void Register<TClass>()
    {
      this.ulUnityContainer.RegisterType<TClass>();
    }

    public void Register<TInterface, TClass>() where TClass : TInterface
    {
      this.ulUnityContainer.RegisterType<TInterface, TClass>(new ContainerControlledLifetimeManager()); ///ContainerControlledLifetimeManager => Singleton
    }

    public void RegisterInstance<TInterface>(TInterface inst)
    {
      this.ulUnityContainer.RegisterInstance<TInterface>(inst);
    }
  }
}
