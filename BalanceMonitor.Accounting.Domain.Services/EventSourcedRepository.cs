using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceMonitor.Accounting.Domain.Services
{
  public abstract class EventSourcedRepository<TEventSourced> : IAggregateRootRepository<TEventSourced> where TEventSourced : class, IAggregateRoot, IEventSourced, new()
  {
    private readonly IEventStore eventStore;
    private readonly IDomainEvents domainEventsPublisher;

    public EventSourcedRepository(IEventStore eventStore, IDomainEvents domainEventsPublisher)
    {
      this.eventStore = eventStore;
      this.domainEventsPublisher = domainEventsPublisher;
    }

    public TEventSourced Get(Guid id)
    {
      TEventSourced agg = default(TEventSourced);
      IEnumerable<IDomainEvent> events = this.eventStore.Events.Where(evt => evt.AggregateId == id);
      if (events.Any())
      {
        agg = new TEventSourced();
        agg.LoadFromHistory(events);
      }
      return agg;
    }

    public void Add(TEventSourced aggregate)
    {
      ///Don't save the entity if it hasnt had any changes applied to it.
      if (aggregate.UncommitedChanges.Any())
      {
        var aggregateEvents = aggregate.UncommitedChanges.ToList();
        if (aggregate.Version != -1) //its an existing aggregate were dealing with..
        {
          var eventStoredAggregate = this.Get(aggregate.Id);
          if (aggregate.Version != eventStoredAggregate.Version)
          {
            throw new Exception(String.Format("Optimistic Concurrency! Aggregate {0} version inconsistency. Aggregate has been modified", aggregate.GetType()));
          }
        }

        int currentVersion = aggregate.Version;
        foreach (var @event in aggregateEvents)
        {
          currentVersion++; //local increment based off entity's currentVersion version
          @event.Version = aggregate.Version = currentVersion; ; //set the event version to it
        }

        this.eventStore.Store(aggregateEvents); //1. Guarantee events are stored on the "source of truth"
        foreach (var @event in aggregateEvents) //2... Then we let others know about the event.
        {
          dynamic eventDym = Convert.ChangeType(@event, @event.GetType());
          this.domainEventsPublisher.Publish(eventDym);
        }
      }
    }
  }
}
