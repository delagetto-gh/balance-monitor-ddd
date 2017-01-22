using BalanceMonitor.Accounting.Application.Projections.Repositories;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;

namespace BalanceMonitor.Accounting
{
  public class AccountingService : IAccountingService
  {
    private readonly IAccountingCommandService commandService;
    private readonly IAccountingQueryService queryService;

    public AccountingService(IAccountingCommandService cmdSvc, IAccountingQueryService qrySvc)
    {
      this.commandService = cmdSvc;
      this.queryService = qrySvc;
    }

    public IAccountingQueryService Query
    {
      get { return this.queryService; }
    }

    public IAccountingCommandService Command
    {
      get { return this.commandService; }
    }
  }
}
