using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorTransactionScopeCommandBus : ICommandBus
  {
    private readonly IContainer container;

    public BalanceMonitorTransactionScopeCommandBus(IContainer container)
    {
      this.container = container;
    }

    public void Submit<TCommand>(TCommand cmd) where TCommand : ICommand
    {
      var cmdHlr = this.container.Resolve<ICommandHandler<TCommand>>();
      if (cmdHlr != null)
      {
        using (var tx = new System.Transactions.TransactionScope())
        {
          cmdHlr.HandleCommand(cmd);
        }
      }
    }
  }
}
