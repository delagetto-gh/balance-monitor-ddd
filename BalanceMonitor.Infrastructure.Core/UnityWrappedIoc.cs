using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Core
{
  public class UnityWrappedIoc : IContainer
  {
    private readonly IUnityContainer ulUnityContainer;

    public UnityWrappedIoc(IUnityContainer unityContainer)
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

    public void Register<TInterface, TClass>() where TClass : TInterface
    {
      this.ulUnityContainer.RegisterType<TInterface, TClass>(new ContainerControlledLifetimeManager()); ///ContainerControlledLifetimeManager => Singleton
    }

    public void Register<TInterface, TClass>(string keyName) where TClass : TInterface
    {
      this.ulUnityContainer.RegisterType<TInterface, TClass>(keyName, new ContainerControlledLifetimeManager());
    }

    public void RegisterInstance<TInterface>(TInterface inst)
    {
      this.ulUnityContainer.RegisterInstance<TInterface>(inst);
    }
  }
}
