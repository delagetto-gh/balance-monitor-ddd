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
  /// <summary>
  /// For conveience, the DomainEvents publisher is also the command bus
  /// </summary>
  public class DomainEvents : ICommandBus, IDomainEvents
  {
    private readonly ILogger logger;
    private readonly IContainer container;

    public DomainEvents(IContainer container, ILogger logger)
    {
      this.container = container;
      this.logger = logger;
    }

    public void Submit<TCommand>(TCommand cmd) where TCommand : ICommand
    {
      if (cmd == null)
        return;

      var handler = this.container.Resolve<ICommandHandler<TCommand>>();
      if (handler != null)
      {
        using (var tx = new System.Transactions.TransactionScope())
        {
          handler.HandleCommand(cmd);
          tx.Complete();
        }
      }
      else
      {
        var ex = new Exception(String.Format("Invalid Command Request - either supplied command is null or no handler for [{0}] command exists!", typeof(TCommand)));
        this.logger.Log(ex.Message);
        throw ex;
      }
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
