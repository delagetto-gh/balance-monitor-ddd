using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System;
using System.Linq;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing
{
  public abstract class EventSourcedRepository<TEventSourcedAr> : IAggregateRootRepository<TEventSourcedAr> where TEventSourcedAr : class, IAggregateRoot, IEventSourced, new()
  {
    private readonly IEventStore eventStore;
    private readonly IDomainEvents domainEventsPublisher;

    public EventSourcedRepository(IEventStore eventStore, IDomainEvents domainEventsPublisher)
    {
      this.eventStore = eventStore;
      this.domainEventsPublisher = domainEventsPublisher;
    }

    public TEventSourcedAr Get(Guid aggregateId)
    {
      TEventSourcedAr agg = default(TEventSourcedAr);
      var events = this.eventStore.Events.OfType<IDomainEvent>().Where(evt => evt.AggregateId == aggregateId);
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
        TEventSourcedAr existingAgg = this.Get(aggregate.Id);
        if (existingAgg != null)
        {
          var expectedAggregateVersion = aggregate.Version - newEvents.Count;
          var existingAggregateVersion = existingAgg.Version;
          if (expectedAggregateVersion != existingAggregateVersion)
          {
            throw new Exception(String.Format("Optimistic Concurrency! Aggregate {0} version inconsistency. Aggregate has been modified", aggregate.GetType()));
          }
        }

        this.eventStore.Store(newEvents); //1. Guarantee aggregate events are stored on the "source of truth"
        foreach (var @event in newEvents) //2... Then we let others know about the event.
        {
          dynamic eventDym = Convert.ChangeType(@event, @event.GetType());
          this.domainEventsPublisher.Publish(eventDym);
        }
      }
    }
  }
}
