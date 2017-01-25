using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace BalanceMonitor.Infrastructure.Core
{
  public class BalanceMonitorXmlEventStore : IEventStore
  {
    private readonly FileStream xmlFileStream;

    public BalanceMonitorXmlEventStore()
    {
      this.xmlFileStream = File.Create("BalanceMonitorXmlEventStore.xml");
    }

    private void SerializeEvents(IEnumerable<IDomainEvent> eventsCollection, FileStream fs)
    {
      var xmlSerializer = new DataContractSerializer(typeof(IEnumerable<IDomainEvent>));
      xmlSerializer.WriteObject(fs, eventsCollection);
    }

    private IEnumerable<IDomainEvent> DeserializeEvents(FileStream fs)
    {
      var xmlDeserializer = new DataContractSerializer(typeof(IEnumerable<IDomainEvent>));
      var obj = xmlDeserializer.ReadObject(fs) as IEnumerable<IDomainEvent>;
      if (obj != null)
      {
        return obj;
      }
      else
      {
        throw new Exception("Failed to Deserialize Event Store you muppet");
      }
    }

    public IEnumerable<IDomainEvent> Events
    {
      get
      {
        var events = this.DeserializeEvents(this.xmlFileStream);
        return events;
      }
    }

    public void Add(IDomainEvent @event)
    {
      var events = new List<IDomainEvent>(this.Events);
      events.Add(@event);
      this.SerializeEvents(events, this.xmlFileStream);
    }
  }
}
