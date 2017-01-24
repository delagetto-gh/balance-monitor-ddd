using BalanceMonitor.Database.Ef;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BalanceMonitor.Infrastructure.Core
{
  public class EventStoreMsSql : IEventStore
  {
    private readonly IDomainEvents eventPublisher;
    private readonly ISessionFactory sessionFactory;

    public EventStoreMsSql(ISessionFactory sessionFactory, IDomainEvents eventBus)
    {
      this.sessionFactory = sessionFactory;
      this.eventPublisher = eventBus;
    }

    public IEnumerable<IVersionedDomainEvent> GetEvents(Guid id)
    {
      var aggregateEvents = new List<IVersionedDomainEvent>();

      var session = this.sessionFactory.Create<BalanceMonitorEntities>();
      using (var ctx = session.Open())
      {
        var dbEvents = ctx.Events.Where(e => e.AggregateId == id).OrderBy(o => o.Version);
        foreach (var dbEvt in dbEvents)
        {
          dynamic deserialedEvent = this.DeserializeEvent(dbEvt.Type, dbEvt.Payload);
          if (deserialedEvent is IVersionedDomainEvent)
          {
            aggregateEvents.Add(deserialedEvent);
          }
        }
        session.Commit();
      }
      return aggregateEvents;
    }

    public void Save(IEventSourced ar)
    {
      var events = ar.UncommitedChanges;
      var version = ar.Version;

      ISession<BalanceMonitorEntities> session = this.sessionFactory.Create<BalanceMonitorEntities>();
      using (var ctx = session.Open())
      {
        //1.save the aggregate if not exists
        Aggregate aggr = this.GetAggregateFromStore(ar, ctx);
        if (aggr == null)
        {
          aggr = new Aggregate { AggregateId = ar.Id, AggregateType = ar.GetType().ToString() };
          ctx.Aggregates.Add(aggr);
        }

        //2.save aggregate events
        foreach (var @event in events)
        {
          version++; //local increment based off entity's starting version
          @event.Version = version; //set the event version to it

          var eventPayload = this.SerializeEvent(@event);
          ctx.Events.Add(new Event
          {
            Aggregate = aggr,
            AggregateId = ar.Id,
            Type = @event.GetType().ToString(),
            Version = @event.Version,
            Payload = eventPayload.ToString()
          });
        }

        //IMPORTANT - Ensure changes are saved to store before publishing, as we dont want to publish events before we know for sure they all have been completley saved to the event store)
        session.Commit();
      }

      //3.publish events to eventhandlers 
      foreach (var @event in events)
      {
        #region Notes on dynamic keyword
        //dynamic keywrd is a godsend. it allows us to bypass compile-time 
        //type checking so we can use the convert method to change the ievent 
        //back to its conrete implementation rather than just object (then casting)
        #endregion
        dynamic e = Convert.ChangeType(@event, @event.GetType());
        this.eventPublisher.Publish(e);
      }
    }

    private Aggregate GetAggregateFromStore<TAggregate>(TAggregate ar, BalanceMonitorEntities ctx) where TAggregate : IEventSourced
    {
      string arType = ar.GetType().ToString();
      return ctx.Aggregates.FirstOrDefault(dbAr => dbAr.AggregateId == ar.Id && dbAr.AggregateType == arType);
    }

    /// <summary>
    /// XML Serializer
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <param name="event"></param>
    /// <returns></returns>
    private string SerializeEvent<TEvent>(TEvent @event) where TEvent : IVersionedDomainEvent
    {
      var serialisedData = new StringBuilder();
      var serialiser = new XmlSerializer(@event.GetType());
      using (var writer = new StringWriter(serialisedData))
      {
        serialiser.Serialize(writer, @event);
      }
      return serialisedData.ToString();
    }

    /// <summary>
    /// XML De-serializer
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <param name="event"></param>
    /// <returns></returns>
    private dynamic DeserializeEvent(string eventClrType, string eventData)
    {
      dynamic @event;
      var eventType = Type.GetType(eventClrType);
      var deSerialiser = new XmlSerializer(eventType);
      using (var reader = new StringReader(eventData))
      {
        @event = deSerialiser.Deserialize(reader);
      }
      return @event;
    }
  }
}
