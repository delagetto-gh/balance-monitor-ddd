using BalanceMonitor.Accounting.Application.Projections.Interfaces;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using System.ServiceModel;

namespace BalanceMonitor.Accounting.Application.Services.ApplicationServices
{
  [ServiceContract]
  public interface IAccountingService : ICommandBus,
                                        IAccountDailyBalanceQuerier,
                                        IAccountAuditQuerier
  {
  }
}
