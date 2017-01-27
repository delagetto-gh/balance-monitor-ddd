using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorDomainEvents : IDomainEvents
  {
    private readonly ILogger logger;
    private readonly IContainer container;

    public BalanceMonitorDomainEvents(IContainer container, ILogger logger)
    {
      this.container = container;
      this.logger = logger;
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
    {
      if (@event != null)
      {
        //var evtHandlers = this.container.ResolveAll<IHandleEvents<TEvent>>();
        //foreach (var eHdlr in evtHandlers)
        //{
        //  //This is done syncronously right now, but perhaps we could have  an ISyncronousEventHandler<T>...
        //  eHdlr.Handle(@event);
        //}

        var eHdlr = this.container.Resolve<IHandleEvents<TEvent>>();
        //This is done syncronously right now, but perhaps we could have  an ISyncronousEventHandler<T>...
        eHdlr.Handle(@event);

      }
      else
      {
        var ex = new Exception(String.Format("Invalid event publication - event [{0}] is null!", typeof(TEvent)));
        this.logger.Log(ex.Message);
        throw ex;
      }
    }
  }
}
