using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    private void SerializeEvents(IEnumerable<VersionedDomainEvent> eventsCollection, FileStream fs)
    {
      //var dataContractSerializer = new DataContractSerializer(typeof(IEnumerable<VersionedDomainEvent>));
      //dataContractSerializer.WriteObject(fs, eventsCollection);


      dynamic evt = eventsCollection.First();
      var dataContractSerializer = new DataContractSerializer(evt.GetType());
      dataContractSerializer.WriteObject(fs, evt);
    }

    private IEnumerable<VersionedDomainEvent> DeserializeEvents(FileStream fs)
    {
      if (fs.Length > 0)
      {
        var xmlDeserializer = new DataContractSerializer(typeof(IEnumerable<VersionedDomainEvent>));
        var obj = xmlDeserializer.ReadObject(fs) as IEnumerable<VersionedDomainEvent>;
        if (obj != null)
        {
          return obj;
        }
        else
        {
          throw new Exception("Failed to Deserialize Event Store you muppet");
        }
      }
      else
      {
        return new List<VersionedDomainEvent>();
      }
    }

    public IEnumerable<VersionedDomainEvent> Events
    {
      get
      {
        var events = this.DeserializeEvents(this.xmlFileStream);
        return events;
      }
    }

    public void Add(VersionedDomainEvent @event)
    {
      var events = new List<VersionedDomainEvent>(this.Events);
      events.Add(@event);
      this.SerializeEvents(events, this.xmlFileStream);
    }
  }
}
