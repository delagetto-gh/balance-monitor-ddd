using System;

namespace BalanceMonitor.Accounting.Application.Commands
{
  public class HelloWorldCommand : WindowsIdentityCommand
  {
    public DateTime Date { get; private set; }

    public string Message { get; private set; }

    public HelloWorldCommand(string message)
    {
      this.Message = message;
      this.Date = DateTime.Now;
    }
  }
}
