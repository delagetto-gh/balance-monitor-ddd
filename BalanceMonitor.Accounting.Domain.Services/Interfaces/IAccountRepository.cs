using BalanceMonitor.Accounting.Domain.Model;
using BalanceMonitor.Infrastructure.Interfaces.DDD;
using System.ServiceModel;

namespace BalanceMonitor.Accounting.Domain.Services
{
  [ServiceContract]
  public interface IAccountRepository : IAggregateRootRepository<Account>
  {
  }
}
