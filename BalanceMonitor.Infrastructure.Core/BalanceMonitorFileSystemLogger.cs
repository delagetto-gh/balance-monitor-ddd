using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using System.IO;

namespace BalanceMonitor.Infrastructure.Core.Logging
{
  public class BalanceMonitorFileSystemLogger : ILogger
  {
    private readonly string logFileName;
    private readonly string logFilePath;

    public BalanceMonitorFileSystemLogger(string dirPath)
    {
      this.logFileName = string.Format("BalanceMonitor_Log_{0}", DateTime.UtcNow.ToString());
      this.logFilePath = dirPath;
    }

    public void Log(string message)
    {
      DirectoryInfo dir = new DirectoryInfo(this.logFilePath);
      if (!dir.Exists)
        dir.Create();

      FileInfo file = new FileInfo(Path.Combine(dir.FullName, this.logFilePath));
      if (!file.Exists)
        file.Create();

      using (var fs = new StreamWriter(file.OpenWrite()))
      {
        fs.Write(string.Format("{0} @ {1}: {2}", this.GetType().Name, DateTimeOffset.Now.ToString(), message));
      }
    }
  }
}
