using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.Accounting.Application.Commands
{
  public abstract class WindowsIdentityCommand : ICommand
  {
    public WindowsIdentityCommand()
    {
      this.User = new User(Environment.UserDomainName);
    }

    public User User { get; set; }
  }
}
