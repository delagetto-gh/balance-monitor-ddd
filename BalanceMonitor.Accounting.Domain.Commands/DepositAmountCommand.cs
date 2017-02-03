using BalanceMonitor.Accounting.Domain.Common;
using System;

namespace BalanceMonitor.Accounting.Domain.Commands
{
  public class DepositAmountCommand : WindowsIdentityCommand
  {
    public Money Amount { get; set; }

    public Guid AccountId { get; set; }

    public DepositAmountCommand(Guid accountId, Money amount)
    {
      this.AccountId = accountId;
      this.Amount = amount;
    }
  }
}
