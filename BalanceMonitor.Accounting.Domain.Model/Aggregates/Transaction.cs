using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;
using System;

namespace BalanceMonitor.Accounting.Domain.Model
{
  public class Transaction : EsAggregateRoot
  {
    private readonly Guid id;

    public Transaction() : this(Guid.NewGuid()) { }

    public Transaction(Guid id)
    {
      this.id = id;
    }
  }
}
