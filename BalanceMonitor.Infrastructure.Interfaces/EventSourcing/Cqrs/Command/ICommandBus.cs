namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing.Cqrs
{
  public interface ICommandBus
  {
    void SubmitCommand<TCommand>(TCommand cmd) where TCommand : ICommand;
  }
}
