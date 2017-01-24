namespace BalanceMonitor.Infrastructure.Interfaces.Cqrs
{
  public interface ICommandHandler<TCommand> where TCommand : ICommand
  {
    void HandleCommand(TCommand cmd);
  }
}
