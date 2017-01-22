using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public enum ActivityType
  {
    AmountCredited,
    AmountDebited,
    AccountOpened,
    AccountClosed
  }
}
