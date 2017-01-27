using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;

namespace BalanceMonitor.Accounting.Domain.Model.Repositories
{
  public interface IAccountRepository : IAggregateRootRepository<Account>
  {
  }
}
