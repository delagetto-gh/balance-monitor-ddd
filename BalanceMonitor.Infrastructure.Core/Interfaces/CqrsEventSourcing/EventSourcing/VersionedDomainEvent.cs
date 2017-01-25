using System;
using System.Runtime.Serialization;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing
{
  [DataContract]
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
