namespace BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs
{
  public interface ICommandBus
  {
    void Submit<TCommand>(TCommand cmd) where TCommand : ICommand;
  }
}
