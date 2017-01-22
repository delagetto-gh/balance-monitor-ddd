using BalanceMonitor.Infrastructure.Interfaces.EventSourcing.Cqrs;
using BalanceMonitor.Infrastructure.Interfaces.Ioc;

namespace BalanceMonitor.Infrastructure.Core
{
  public class CommandHandlerFactory : ICommandHandlerFactory
  {
    private readonly IContainer iocContainer;

    public CommandHandlerFactory(IContainer ioc)
    {
      this.iocContainer = ioc;
    }

    public ICommandHandler<TCommand> GetHandler<TCommand>() where TCommand : ICommand
    {
      var cmdHlr = this.iocContainer.Resolve<ICommandHandler<TCommand>>();
      return cmdHlr;
    }
  }
}
