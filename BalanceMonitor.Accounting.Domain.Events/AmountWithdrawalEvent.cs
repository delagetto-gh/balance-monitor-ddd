using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;

namespace BalanceMonitor.Accounting.Domain.Events
{
  [Serializable]
  public class AmountWithdrawalEvent : VersionedDomainEvent
  {
    public AmountWithdrawalEvent(Guid aggregateId, Money amount)
      : base(aggregateId)
    {
      this.Amount = amount;
    }

    /// <summary>
    /// Req for serialization
    /// </summary>
    private AmountWithdrawalEvent()
    { }

    public Money Amount { get; set; }
  }
}
