using System;

namespace BalanceMonitor.Accounting.Application.Commands
{
  public class CreateAccountCommand : WindowsIdentityCommand
  {
    public CreateAccountCommand(Guid id, string name)
    {
      this.Identifier = id;
      this.Name = name;
      this.Created = DateTime.Now;
    }

    public Guid Identifier { get; private set; }

    public string Name { get; private set; }

    public DateTime Created { get; private set; }
  }
}
