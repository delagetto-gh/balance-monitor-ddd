using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorXmlEventStore : IEventStore
  {
    private readonly string xmlFile;

    public BalanceMonitorXmlEventStore()
    {
      this.xmlFile = "BalanceMonitorXmlEventStore.xml";
    }

    public IEnumerable<IDomainEvent> Events
    {
      get
      {
        List<IDomainEvent> domainEvents = new List<IDomainEvent>();
        using (var fs = new FileStream(this.xmlFile, FileMode.OpenOrCreate))
        {
          if (fs.Length > 0)
          {
            var xmlDeserializer = new DataContractSerializer(typeof(List<SerializableXmlEvent>));
            var storedEvents = (List<SerializableXmlEvent>)xmlDeserializer.ReadObject(fs);
            foreach (var xmlEvent in storedEvents)
            {
              var xmlEventDeserialzer = new DataContractSerializer(Type.GetType(string.Format("{0}, {1}", xmlEvent.EventType, xmlEvent.Assemblyname)));
              using (var sr = new StringReader(xmlEvent.Data))
              using (var xmlReader = new XmlTextReader(sr))
              {
                var evt = (IDomainEvent)xmlEventDeserialzer.ReadObject(xmlReader);
                domainEvents.Add(evt);
              }
            }
          }
          return domainEvents.OrderBy(o => o.Created);
        }
      }
    }

    public void Store<TDomainEvent>(IEnumerable<TDomainEvent> events) where TDomainEvent : IDomainEvent
    {
      var domainEvents = new List<IDomainEvent>(this.Events);
      foreach (var @event in events)
      {
        domainEvents.Add(@event);
      }

      var xmlEvents = new List<SerializableXmlEvent>(domainEvents.Count);
      foreach (var @event in domainEvents)
      {
        var sb = new StringBuilder();
        using (var xmlWriter = XmlWriter.Create(sb))
        {
          var xmlSerializer = new DataContractSerializer(@event.GetType());
          xmlSerializer.WriteObject(xmlWriter, @event);
        }

        var xmlEvent = new SerializableXmlEvent
        {
          AggregateId = @event.AggregateId,
          Version = @event.Version,
          Data = sb.ToString(),
          EventType = @event.GetType().FullName.ToString(),
          Assemblyname = @event.GetType().Assembly.FullName,
        };
        xmlEvents.Add(xmlEvent);
      }

      using (var fs = new FileStream(this.xmlFile, FileMode.Create))
      {
        var xmlEventSerializer = new DataContractSerializer(typeof(List<SerializableXmlEvent>));
        xmlEventSerializer.WriteObject(fs, xmlEvents);
      }
    }
  }

  public class SerializableXmlEvent
  {
    public Guid AggregateId { get; set; }

    public int Version { get; set; }

    public string EventType { get; set; }

    public string Assemblyname { get; set; }

    public string Data { get; set; }
  }
}
