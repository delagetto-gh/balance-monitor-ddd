namespace BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs
{
  public interface ICommandHandler<TCommand> where TCommand : ICommand
  {
    void HandleCommand(TCommand cmd);
  }
}
