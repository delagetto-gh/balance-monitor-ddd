using BalanceMonitor.Accounting.Domain.Model;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using System.ServiceModel;

namespace BalanceMonitor.Accounting.Domain.Services
{
  [ServiceContract]
  public interface IAccountRepository : IEventSourcedRepsitory< IAggregateRootRepository<Account>
  {
  }
}
