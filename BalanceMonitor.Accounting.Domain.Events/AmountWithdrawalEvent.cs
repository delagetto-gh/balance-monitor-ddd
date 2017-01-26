using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;

namespace BalanceMonitor.Accounting.Domain.Events
{
  public class AmountWithdrawalEvent : EventSourcedDomainEvent
  {
    public AmountWithdrawalEvent(Guid aggregateId, Money amount)
      : base(aggregateId)
    {
      this.Amount = amount;
    }

    /// <summary>
    /// Req for serialization
    /// </summary>
    protected AmountWithdrawalEvent()
    { }

    public Money Amount { get; set; }

    public string AccountName { get; set; }
  }
}
