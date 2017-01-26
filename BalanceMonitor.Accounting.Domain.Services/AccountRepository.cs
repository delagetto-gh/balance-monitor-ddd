using BalanceMonitor.Accounting.Domain.Model;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Core.Interfaces.EventSourcing;

namespace BalanceMonitor.Accounting.Domain.Services
{
  public class AccountRepository : EventSourcedRepository<Account>, IAccountRepository
  {
    public AccountRepository(IEventStore eventStore, IDomainEvents eventPublisher)
      : base(eventStore, eventPublisher)
    { }
  }
}