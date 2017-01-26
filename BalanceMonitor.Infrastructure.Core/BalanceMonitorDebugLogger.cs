using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using System.Diagnostics;

namespace BalanceMonitor.Infrastructure.Core.Logging
{
  public class BalanceMonitorDebugLogger : ILogger
  {
    public void Log(string message)
    {
      Debug.WriteLine(string.Format("{0} @ {1}: {2}", this.GetType().Name, DateTime.UtcNow, message));
    }
  }
}
