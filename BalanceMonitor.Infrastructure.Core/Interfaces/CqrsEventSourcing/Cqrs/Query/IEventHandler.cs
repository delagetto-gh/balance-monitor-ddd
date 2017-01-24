using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs
{
  public interface IEventHandler<TEvent> where TEvent : IDomainEvent
  {
    void Handle(TEvent @event);
  }
}
