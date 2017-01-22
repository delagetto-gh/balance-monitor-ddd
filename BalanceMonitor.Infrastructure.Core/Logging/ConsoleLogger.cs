using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;

namespace BalanceMonitor.Infrastructure.Core.Logging
{
  public class ConsoleLogger : ILogger
  {
    public void Log(string message)
    {
      Console.WriteLine(String.Format("{0}:{1}", DateTimeOffset.Now, message));
    }
  }
}
