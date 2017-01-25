using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;

namespace BalanceMonitor.Accounting.Domain.Events
{
  [Serializable]
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
  }
}
