using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Infrastructure.Interfaces.DDD
{
  public interface IEventHandlerFactory
  {
    IEnumerable<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent;
  }
}
