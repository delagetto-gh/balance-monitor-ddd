using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public interface IEventHandler<TEvent> where TEvent : IEvent
  {
    void Handle(TEvent @event);
  }
}
