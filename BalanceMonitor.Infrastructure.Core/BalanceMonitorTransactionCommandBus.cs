using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorTransactionCommandBus : ICommandBus
  {
    private readonly IContainer container;

    public BalanceMonitorTransactionCommandBus(IContainer container)
    {
      this.container = container;
    }

    public void Submit<TCommand>(TCommand cmd) where TCommand : ICommand
    {
      using (var tx = new System.Transactions.TransactionScope())
      {
        var cmdHlr = this.container.Resolve<ICommandHandler<TCommand>>();
        if (cmdHlr != null)
          cmdHlr.HandleCommand(cmd);

        tx.Complete();
      }
    }
  }
}
