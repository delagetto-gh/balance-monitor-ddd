using BalanceMonitor.Accounting.Domain.Commands;
using BalanceMonitor.Accounting.Domain.Model;
using BalanceMonitor.Accounting.Domain.Services;
using BalanceMonitor.Infrastructure.Core.Interfaces.Cqrs;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;

namespace BalanceMonitor.Accounting.Application.CommonHandlers
{
  public class BalanceMonitorAccountingCommandHandler : ICommandHandler<CreateAccountCommand>,
                                                        ICommandHandler<WithdrawMoneyCommand>
  {
    private readonly IAccountRepository repository;
    private readonly ILogger logger;

    public BalanceMonitorAccountingCommandHandler(ILogger logger, IAccountRepository accountRepository)
    {
      this.repository = accountRepository;
      this.logger = logger;
    }

    public void HandleCommand(CreateAccountCommand cmd)
    {
      this.logger.Log(string.Format("In {0} command handler", cmd.GetType().Name));

      if (!String.IsNullOrWhiteSpace(cmd.Name) && cmd.Created.Date >= DateTime.Today)
      {
        Account acct = this.repository.Get(cmd.Identifier);
        if (acct == null)
        {
          acct = Account.Create(cmd.Identifier, cmd.Name, cmd.Created);
          this.repository.Save(acct);
        }
        else
        {
          throw new Exception(String.Format("Account number already exists for account {0} [{1}]", cmd.Name, cmd.Identifier));
        }
      }
      else
      {
        throw new Exception("Invalid arguments for command");
      }
      this.logger.Log(string.Format("Exit {0} command handler", cmd.GetType().Name));
    }

    public void HandleCommand(WithdrawMoneyCommand cmd)
    {
      this.logger.Log(string.Format("In {0} command handler", cmd.GetType().Name));

      if (cmd != null)
      {
        Account acc = this.repository.Get(cmd.AccountId);
        if (acc != null)
        {
          acc.Withdraw(cmd.Amount.Currency, cmd.Amount.Amount);
          this.repository.Save(acc);
        }
        else
        {
          throw new Exception(String.Format("No Account exists with Id {0}", cmd.AccountId));
        }
      }
    }
  }
}
