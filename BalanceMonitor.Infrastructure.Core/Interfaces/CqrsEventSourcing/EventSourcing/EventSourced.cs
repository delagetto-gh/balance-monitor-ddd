using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing
{
  public abstract class EventSourced : IEventSourced
  {
    private readonly List<IDomainEvent> changes = new List<IDomainEvent>();
    private readonly Dictionary<Type, Action<IDomainEvent>> eventHandlers = new Dictionary<Type, Action<IDomainEvent>>();

    protected EventSourced()
    {
      this.Version = -1;
    }

    public Guid Id { get; protected set; }

    public int Version { get; set; }

    public IEnumerable<IDomainEvent> UncommitedChanges
    {
      get
      {
        return this.changes.AsReadOnly();
      }
    }

    public void MarkChangesAsCommitted()
    {
      this.changes.Clear();
    }

    public void LoadFromHistory(IEnumerable<IDomainEvent> events)
    {
      foreach (var @event in events.OrderBy(e => e.Version))//ensure events are ordered in asc (from beginning)
      {
        this.Apply(@event, false);
        this.Version = @event.Version; //Version of aggregate will be the same as last event
      }
    }

    protected void Handles<TDomainEvent>(Action<TDomainEvent> handler) where TDomainEvent : IDomainEvent
    {
      this.eventHandlers.Add(typeof(TDomainEvent), @event => handler((TDomainEvent)@event));
    }

    protected void Apply(IDomainEvent @event)
    {
      this.Apply(@event, true);
    }

    private void Apply(IDomainEvent @event, bool isNew)
    {
      if (this.eventHandlers.ContainsKey(@event.GetType()))
      {
        this.eventHandlers[@event.GetType()](@event);
        if (isNew)
          this.changes.Add(@event);
      }
    }
  }
}
