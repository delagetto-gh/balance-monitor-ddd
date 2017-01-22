using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using System.Collections.Generic;
using BalanceMonitor.Infrastructure.Interfaces.DDD;

namespace BalanceMonitor.Infrastructure.Core
{
  public class EventHandlerFactory : IEventHandlerFactory
  {
    private readonly IContainer iocContainer;

    public EventHandlerFactory(IContainer iocContainer)
    {
      this.iocContainer = iocContainer;
    }

    public IEnumerable<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent
    {
      var eventHdlrs = this.iocContainer.ResolveAll<IEventHandler<TEvent>>();
      return eventHdlrs;
    }
  }
}
