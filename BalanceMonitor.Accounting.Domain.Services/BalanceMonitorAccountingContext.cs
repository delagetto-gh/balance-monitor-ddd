using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorAccountingContext : ISession<BalanceMonitorAccountingContext>
  {
    private IEventStore eventStore;

    private IDomainEvents domainEventsPublisher;

    private ObservableCollection<IDomainEvent> events = new ObservableCollection<IDomainEvent>();

    private bool IsDirty = true;

    public BalanceMonitorAccountingContext(IEventStore eventStore, IDomainEvents domainEventsPublisher)
    {
      this.eventStore = eventStore;
      this.domainEventsPublisher = domainEventsPublisher;
    }

    private void OnEventsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.IsDirty = true;
    }

    public BalanceMonitorAccountingContext Open()
    {
      return this;
    }

    public ICollection<IDomainEvent> Events
    {
      get
      {
        if (this.IsDirty)
        {
          var eventsUpdated = this.eventStore.Events;
          this.events = new ObservableCollection<IDomainEvent>();
          this.IsDirty = false;
        }
        return this.events;
      }
    }

    public void Commit()
    {
      try
      {
        IEnumerable<IDomainEvent> events = this.events;
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
