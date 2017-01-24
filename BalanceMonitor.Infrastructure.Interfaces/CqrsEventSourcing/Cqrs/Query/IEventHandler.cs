using BalanceMonitor.Infrastructure.Interfaces.DDD;

namespace BalanceMonitor.Infrastructure.Interfaces.Cqrs
{
  public interface IEventHandler<TEvent> where TEvent : IDomainEvent
  {
    void Handle(TEvent @event);
  }
}
