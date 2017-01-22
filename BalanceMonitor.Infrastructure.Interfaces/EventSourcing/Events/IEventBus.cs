
namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public interface IEventBus
  {
    void PublishEvent<TEvent>(TEvent @event) where TEvent : IEsDomainEvent;
  }
}
