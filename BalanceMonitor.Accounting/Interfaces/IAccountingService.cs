using BalanceMonitor.Accounting.Application.Projections.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Accounting
{
  public interface IAccountingService
  {
    IAccountingQueryService Query { get; }

    IAccountingCommandService Command { get; }
  }
}
