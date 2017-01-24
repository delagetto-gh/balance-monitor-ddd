using BalanceMonitor.Accounting.Domain.Common;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using System;

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
