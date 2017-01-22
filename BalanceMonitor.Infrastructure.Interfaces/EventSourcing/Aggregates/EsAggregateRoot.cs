using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceMonitor.Infrastructure.Interfaces.EventSourcing
{
  public abstract class EsAggregateRoot : IEsAggregateRoot
  {
    private readonly List<IEsDomainEvent> changes = new List<IEsDomainEvent>();
    private readonly Dictionary<Type, Action<IEsDomainEvent>> eventHandlers = new Dictionary<Type, Action<IEsDomainEvent>>();

    protected EsAggregateRoot()
    {
      this.Version = -1;
    }

    public Guid Id { get; protected set; }

    public int Version { get; set; }

    public IEnumerable<IEsDomainEvent> UncommitedChanges
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

    public void LoadFromHistory(IEnumerable<IEsDomainEvent> events)
    {
      foreach (var @event in events.OrderBy(e => e.Version))//ensure events are ordered in asc (from beginning)
      {
        this.Apply(@event, false);
        this.Version = @event.Version; //Version of aggregate will be the same as last event
      }
    }

    protected void Handles<TDomainEvent>(Action<TDomainEvent> handler) where TDomainEvent : IEsDomainEvent
    {
      this.eventHandlers.Add(typeof(TDomainEvent), @event => handler((TDomainEvent)@event));
    }

    protected void Apply(IEsDomainEvent @event)
    {
      this.Apply(@event, true);
    }

    private void Apply(IEsDomainEvent @event, bool isNew)
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
