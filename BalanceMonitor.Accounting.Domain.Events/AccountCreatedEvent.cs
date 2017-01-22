using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Domain.Events
{
  [Serializable]
  public class AccountCreatedEvent : EsDomainEvent
  {
    public string Name { get; set; }
    public DateTime Effective { get; set; }

    public AccountCreatedEvent(Guid id, string name, DateTime created)
      : base(id)
    {
      this.Name = name;
      this.Effective = created;
    }

    /// <summary>
    /// Req for serialization
    /// </summary>
    private AccountCreatedEvent() { }
  }
}