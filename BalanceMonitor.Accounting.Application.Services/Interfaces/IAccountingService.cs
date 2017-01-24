using BalanceMonitor.Accounting.Application.Commands;
using BalanceMonitor.Accounting.Application.Projections.Interfaces;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing.Cqrs;

namespace BalanceMonitor.Accounting.Application.Services.ApplicationServices
{
  public interface IAccountingService : ICommandHandler<CreateAccountCommand>,
                                        IAccountDailyBalanceService, IAccountAuditService
  {
  }
}
