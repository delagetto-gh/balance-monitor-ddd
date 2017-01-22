namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing.Cqrs
{
  public interface ICommandHandlerFactory
  {
    ICommandHandler<TCommand> GetHandler<TCommand>() where TCommand : ICommand;
  }
}
