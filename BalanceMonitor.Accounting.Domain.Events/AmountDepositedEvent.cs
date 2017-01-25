using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Runtime.Serialization;

namespace BalanceMonitor.Accounting.Domain.Events
{
  [DataContract]
  public class AmountDepositedEvent : VersionedDomainEvent
  {
    public AmountDepositedEvent(Guid aggregateId, Money amount)
      : base(aggregateId)
    {
      this.Amount = amount;
    }

    /// <summary>
    /// Req for serialization
    /// </summary>
    private AmountDepositedEvent()
    { }

    public Money Amount { get; set; }

    public string AccountName { get; set; }
  }
}
