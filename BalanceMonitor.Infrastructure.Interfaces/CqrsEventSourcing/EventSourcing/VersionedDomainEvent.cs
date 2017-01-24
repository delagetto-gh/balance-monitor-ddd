using System;

namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public abstract class VersionedDomainEvent : IVersionedDomainEvent
  {
    protected VersionedDomainEvent(Guid aggregateId)
    {
      this.AggregateId = aggregateId;
      this.Created = DateTime.UtcNow;
    }

    protected VersionedDomainEvent()
    { }

    public Guid AggregateId { get; set; }

    public int Version { get; set; }

    public DateTime Created { get; set; }
  }
}
