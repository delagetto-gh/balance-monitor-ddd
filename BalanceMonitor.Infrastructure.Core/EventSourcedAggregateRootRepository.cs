using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceMonitor.Infrastructure.Core
{
  public class EventSourcedAggregateRootRepository<TAggregateRoot> : IAggregateRootRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot, IEventSourced, new()
  {
    private readonly IEventStore eventStore;

    public EventSourcedAggregateRootRepository(IEventStore eventStore)
    {
      this.eventStore = eventStore;
    }

    public TAggregateRoot Get(Guid id)
    {
      IEnumerable<IVersionedDomainEvent> events = this.eventStore.GetEvents(id);
      if (events.Any())
      {
        var aggregate = new TAggregateRoot();
        aggregate.LoadFromHistory(events);
        return aggregate;
      }
      else
      {
        return new TAggregateRoot();
      }
    }

    public void Add(TAggregateRoot aggregate)
    {
      ///Don't save the entity if it hasnt had any changes applied to it.
      if (aggregate.UncommitedChanges.Any())
      {
        if (aggregate.Version != -1) //its not a new aggregate
        {
          var eventStoredAggregate = this.Get(aggregate.Id);
          if (aggregate.Version != eventStoredAggregate.Version)
          {
            throw new Exception(String.Format("Optimistic Concurrency! Aggregate {0} version inconsistency. Aggregate has been modified", aggregate.GetType()));
          }
        }
        //store to eventstore (which will also publish events to handlers!)
        this.eventStore.Save(aggregate);
      }
    }
  }
}
