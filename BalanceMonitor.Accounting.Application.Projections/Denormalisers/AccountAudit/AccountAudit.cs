using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountAudit
  {
    public Guid AccountId { get; set; }
    public string AccountName { get; set; }
    public DateTime Time { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Action { get; set; }
  }
}
