using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Runtime.Serialization;

namespace BalanceMonitor.Accounting.Domain.Events
{
  public class AmountDepositedEvent : DomainEvent
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
