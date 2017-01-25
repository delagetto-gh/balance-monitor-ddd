using System.Collections.Generic;

namespace BalanceMonitor.Infrastructure.Interfaces.Ioc
{
  public interface IContainer
  {
    TInterface Resolve<TInterface>();

    IEnumerable<TInterface> ResolveAll<TInterface>();

    void Register<TClass>();

    void Register<TInterface, TClass>() where TClass : TInterface;

    void Register<TInterface, TClass>(string keyName) where TClass : TInterface;

    void RegisterInstance<TInterface>(TInterface inst);
  }
}
