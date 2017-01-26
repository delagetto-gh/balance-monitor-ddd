using System;
using System.Runtime.Serialization;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.DDD
{
  public abstract class DomainEvent : IDomainEvent
  {
    protected DomainEvent(Guid aggregateId)
    {
      this.AggregateId = aggregateId;
      this.Created = DateTime.UtcNow;
    }

    protected DomainEvent()
    { }

    public Guid AggregateId { get; set; }

    public int Version { get; set; }

    public DateTime Created { get; set; }
  }
}
