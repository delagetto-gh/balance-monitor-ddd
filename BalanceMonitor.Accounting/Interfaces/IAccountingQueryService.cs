using BalanceMonitor.Accounting.Application.Projections.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Accounting
{
  public interface IAccountingQueryService : IAccountAuditRepository, IAccountDailyBalanceRepository
  { }
}
