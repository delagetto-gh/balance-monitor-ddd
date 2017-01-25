using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorAccountingSession : ISession
  {
    private IEventStore eventStore;

    private IDomainEvents domainEventsPublisher;

    private ObservableCollection<VersionedDomainEvent> events = new ObservableCollection<VersionedDomainEvent>();

    private bool IsDirty = true;

    public BalanceMonitorAccountingSession(IEventStore eventStore, IDomainEvents domainEventsPublisher)
    {
      this.eventStore = eventStore;
      this.domainEventsPublisher = domainEventsPublisher;
      this.events.CollectionChanged += (o, evnt) => this.IsDirty = true;
    }

    public ICollection<VersionedDomainEvent> Events
    {
      get
      {
        if (this.IsDirty)
        {
          var eventsUpdated = this.eventStore.Events;
          this.events = new ObservableCollection<VersionedDomainEvent>(eventsUpdated);
          this.IsDirty = false;
        }
        return this.events;
      }
    }

    public void Commit()
    {
      try
      {
        IEnumerable<VersionedDomainEvent> events = this.events;
        foreach (var @event in events)
        {
          this.eventStore.Add(@event);
          this.domainEventsPublisher.Publish(@event);
        }
        this.IsDirty = true;
      }
      catch (Exception e)
      {
        throw;
      }
    }

    public void Dispose()
    {
      this.events.Clear();
      this.eventStore = null;
      this.domainEventsPublisher = null;
    }
  }
}
