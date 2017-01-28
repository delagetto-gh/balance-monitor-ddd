using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System.ServiceModel;

namespace BalanceMonitor.Accounting.Domain.Model.Repositories
{
  [ServiceContract]
  public interface IAccountRepository : IAggregateRootRepository<Account>
  {
  }
}
