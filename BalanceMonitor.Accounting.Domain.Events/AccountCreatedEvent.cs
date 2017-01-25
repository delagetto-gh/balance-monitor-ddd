using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BalanceMonitor.Accounting.Domain.Events
{
  [DataContract]
  public class AccountCreatedEvent : VersionedDomainEvent
  {
    public string Name { get; set; }

    public DateTime Effective { get; set; }

    public IEnumerable<Money> OpeningBalance { get; set; }

    public AccountCreatedEvent(Guid id, string name, DateTime created, IEnumerable<Money> openingBalance)
      : base(id)
    {
      this.Name = name;
      this.Effective = created;
      this.OpeningBalance = openingBalance;
    }

    /// <summary>
    /// Req for serialization
    /// </summary>
    private AccountCreatedEvent() { }

    public string AccountName { get; set; }
  }
}