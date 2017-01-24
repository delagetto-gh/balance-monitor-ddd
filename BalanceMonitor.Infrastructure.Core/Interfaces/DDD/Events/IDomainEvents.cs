namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IDomainEvents
  {
    void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
  }
}
