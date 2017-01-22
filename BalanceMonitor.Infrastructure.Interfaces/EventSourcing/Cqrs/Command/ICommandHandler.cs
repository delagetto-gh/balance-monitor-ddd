namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing.Cqrs
{
  public interface ICommandHandler<TCommand> where TCommand : ICommand
  {
    void HandleCommand(TCommand cmd);
  }
}
