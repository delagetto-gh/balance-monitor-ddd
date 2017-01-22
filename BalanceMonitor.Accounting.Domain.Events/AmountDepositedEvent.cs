using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using System;

namespace BalanceMonitor.Accounting.Domain.Events
{
  [Serializable]
  public class AmountDepositedEvent : EsDomainEvent
  {
    public AmountDepositedEvent(Guid aggregateId, Cash amount)
      : base(aggregateId)
    {
      this.Cash = amount;
    }

    /// <summary>
    /// Req for serialization
    /// </summary>
    private AmountDepositedEvent()
    { }

    public Cash Cash { get; set; }
  }
}
