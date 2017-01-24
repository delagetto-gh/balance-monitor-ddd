using BalanceMonitor.Accounting.Application.Commands;
using BalanceMonitor.Accounting.Application.Projections.Interfaces;
using BalanceMonitor.Infrastructure.Interfaces.Cqrs;
using System.ServiceModel;

namespace BalanceMonitor.Accounting.Application.Services.ApplicationServices
{
  [ServiceContract]
  public interface IAccountingService : ICommandHandler<CreateAccountCommand>,
                                        IAccountDailyBalanceService,
                                        IAccountAuditService
  {
  }
}
