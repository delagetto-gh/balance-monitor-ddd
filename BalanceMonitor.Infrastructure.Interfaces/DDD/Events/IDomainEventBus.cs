namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public interface IDomainEventBus
  {
    void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
  }
}
