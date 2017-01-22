using BalanceMonitor.Accounting.Domain.Model;
using BalanceMonitor.Infrastructure.Core.Cqrs;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceMonitor.Accounting.Infrastructure.Repositories
{
  public class AccountingRepository : EsAggregateRootRepository<Account>
  {
    private readonly IEventStore eventStore;

    public AccountingRepository(IEventStore eventStore)
      : base(eventStore)
    {
      this.eventStore = eventStore;
    }
  }
}
