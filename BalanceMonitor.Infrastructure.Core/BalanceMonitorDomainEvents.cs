using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using System.Linq;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using System.Reflection;

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

        //appdomainselfdiscovery
        Type eventHlrType = typeof(IHandleEvents<>).MakeGenericType(typeof(TEvent));
        var eventHandlers = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(o => Assembly.Load(o.FullName)).SelectMany(o => o.GetTypes()).Where(o => o.IsAssignableFrom(eventHlrType)).ToList();
        foreach (var evtHlr in eventHandlers)
        {
          if (evtHlr.IsAssignableFrom(typeof(IHandleEvents<TEvent>)))
          {
            var hdlr = evtHlr as IHandleEvents<TEvent>;
            hdlr.Handle(@event);
          }
        }

        //var eHdlr = this.container.Resolve<IHandleEvents<TEvent>>();
        ////This is done syncronously right now, but perhaps we could have  an ISyncronousEventHandler<T>...
        //eHdlr.Handle(@event);

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
