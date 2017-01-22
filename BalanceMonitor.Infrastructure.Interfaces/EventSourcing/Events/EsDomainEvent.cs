using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using System;

namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public abstract class EsDomainEvent : IEsDomainEvent
  {
    protected EsDomainEvent(Guid aggregateId)
    {
      this.AggregateId = aggregateId;
      this.Created = DateTime.UtcNow;
    }

    protected EsDomainEvent()
    { }

    public Guid AggregateId { get; set; }

    public int Version { get; set; }

    public DateTime Created { get; set; }
  }
}
