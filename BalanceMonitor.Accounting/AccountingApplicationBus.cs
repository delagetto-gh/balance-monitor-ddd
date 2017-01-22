using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing.Cqrs;
using BalanceMonitor.Infrastructure.Interfaces.DDD;

namespace BalanceMonitor.Infrastructure.Core
{
  public class AccountingApplicationBus : ICommandBus, IEventBus
  {
    private readonly ICommandHandlerFactory cmdHandlerFactory;
    private readonly IEventHandlerFactory evtHandlerFactory;
    private readonly ILogger logger;

    public AccountingApplicationBus(ICommandHandlerFactory cmdHdlrFactory, IEventHandlerFactory evntHdlrFactory, ILogger logger)
    {
      this.cmdHandlerFactory = cmdHdlrFactory;
      this.evtHandlerFactory = evntHdlrFactory;
      this.logger = logger;
    }

    public void SubmitCommand<TCommand>(TCommand cmd) where TCommand : ICommand
    {
      var handler = this.cmdHandlerFactory.GetHandler<TCommand>();
      if (cmd != null && handler != null)
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
        this.logger.Log(this, ex.Message);
        throw ex;
      }
    }

    public void PublishEvent<TEvent>(TEvent @event) where TEvent : IEvent
    {
      if (@event != null)
      {
        var eventHandlers = this.evtHandlerFactory.GetHandlers<TEvent>();
        foreach (var eHdlr in eventHandlers)
        {
          //this is done syncronously right now, but perhaps we could have 
          //an ISyncronousEventHandler<T> and an IAsyncronousEventHandler
          //and we can execute accordingly
          eHdlr.Handle(@event);
        }
      }
      else
      {
        var ex = new Exception(String.Format("Invalid event publication - event [{0}] is null!", typeof(TEvent)));
        this.logger.Log(this, ex.Message);
        throw ex;
      }
    }
  }
}
