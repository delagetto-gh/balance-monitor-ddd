using System;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public abstract class DomainEvent : IDomainEvent
  {
    protected DomainEvent(Guid aggregateId)
      : this()
    {
      this.AggregateId = aggregateId;
    }

    protected DomainEvent()
    {
      this.DateOccured = DateTime.UtcNow;
    }

    public Guid AggregateId { get; set; }

    public DateTime DateOccured { get; set; }
  }
}
