using BalanceMonitor.Accounting.Domain.Model;
using BalanceMonitor.Infrastructure.Core;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing;

namespace BalanceMonitor.Accounting.Domain.Services
{
  public class AccountRepository : EsAggregateRootRepository<Account>, IAccountRepository
  {
    private readonly IEventStore eventStore;

    public AccountRepository(IEventStore eventStore)
      : base(eventStore)
    {
      this.eventStore = eventStore;
    }
  }
}
