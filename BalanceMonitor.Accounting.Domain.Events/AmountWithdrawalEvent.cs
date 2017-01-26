using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;
using System;
using System.Runtime.Serialization;

namespace BalanceMonitor.Accounting.Domain.Events
{
  public class AmountWithdrawalEvent : DomainEvent
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

    public string AccountName { get; set; }
  }
}
