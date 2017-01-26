using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceMonitor.Accounting.Domain.Services
{
  public abstract class EventSourcedRepository<TEventSourcedAr> : IAggregateRootRepository<TEventSourcedAr> where TEventSourcedAr : class, IEventSourced, IAggregateRoot, new()
  {
    private readonly IEventStore eventStore;
    private readonly IDomainEvents domainEventsPublisher;

    public EventSourcedRepository(IEventStore eventStore, IDomainEvents domainEventsPublisher)
    {
      this.eventStore = eventStore;
      this.domainEventsPublisher = domainEventsPublisher;
    }

    public TEventSourcedAr Get(Guid id)
    {
      TEventSourcedAr agg = default(TEventSourcedAr);
      var events = this.eventStore.Events.OfType<IDomainEvent>().Where(evt => evt.AggregateId == id);
      if (events.Any())
      {
        agg = new TEventSourcedAr();
        agg.LoadFromHistory(events);
      }
      return agg;
    }

    public void Save(TEventSourcedAr aggregate)
    {
      ///Don't save the entity if it hasnt had any changes applied to it.
      if (aggregate.UncommitedChanges.Any())
      {
        var newEvents = aggregate.UncommitedChanges.ToList();
        if (aggregate.Version != -1) //its an existing aggregate were dealing with..
        {
          var eventStoredAggregate = this.Get(aggregate.Id);
          if (aggregate.Version != eventStoredAggregate.Version)
          {
            throw new Exception(String.Format("Optimistic Concurrency! Aggregate {0} version inconsistency. Aggregate has been modified", aggregate.GetType()));
          }
        }
        this.eventStore.(newEvents); //1. Guarantee aggregate events are stored on the "source of truth"
        foreach (var @event in aggregate.UncommitedChanges) //2... Then we let others know about the event.
        {
          dynamic eventDym = Convert.ChangeType(@event, @event.GetType());
          this.domainEventsPublisher.Publish(eventDym);
        }
      }
    }
  }
}
