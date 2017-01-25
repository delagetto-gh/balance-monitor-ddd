using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public async void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
    {
      if (@event != null)
      {
        var evtHandlers = this.container.ResolveAll<IEventHandler<TEvent>>();
        var evtHandlerExecutions = new List<Task>(evtHandlers.Count());
        foreach (var eHdlr in evtHandlers)
        {
          //This is done syncronously right now, but perhaps we could have  an ISyncronousEventHandler<T>...
          //scratch that - we are doing it syncronously AS A WHOLE, but parallelizing the handler executions
          //evtHandlerExecutions.Add(Task.Run(() => eHdlr.Handle(@event)));

          //sync for now
          eHdlr.Handle(@event);
        }

        //sync for now
        //await Task.WhenAll(evtHandlerExecutions);
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
