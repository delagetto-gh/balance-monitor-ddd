using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using System;

namespace BalanceMonitor.Accounting.Domain.Events
{
  [Serializable]
  public class AmountWithdrawalEvent : EsDomainEvent
  {
    public AmountWithdrawalEvent(Guid aggregateId, Cash amount)
      : base(aggregateId)
    {
      this.Cash = amount;
    }

    /// <summary>
    /// Req for serialization
    /// </summary>
    private AmountWithdrawalEvent()
    { }

    public Cash Cash { get; set; }
  }
}
