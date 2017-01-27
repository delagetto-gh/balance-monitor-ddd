namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public interface IHandleEvents<TDomainEvent> where TDomainEvent : IDomainEvent
  {
    void Handle(TDomainEvent @event);
  }
}
