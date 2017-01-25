using BalanceMonitor.Infrastructure.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Accounting.Application.Projections
{
  public class AccountAuditInMemoryContext : ISession<AccountAuditInMemoryContext>
  {
    public AccountAuditInMemoryContext Open()
    {
      throw new NotImplementedException();
    }

    public void Commit()
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}
