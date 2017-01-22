using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace BalanceMonitor.Accounting
{
  public interface IAccountingCommandService
  {
    void SendCommand<TCommand>(TCommand cmd) where TCommand : ICommand;
  }
}
