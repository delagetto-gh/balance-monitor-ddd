using BalanceMonitor.Accounting.Domain.Model;
using BalanceMonitor.Infrastructure.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Interfaces.EventSourcing.Cqrs;
using BalanceMonitor.Infrastructure.Interfaces.UnitOfWork;
using System;

namespace BalanceMonitor.Accounting.Application.Commands
{
  public class CreateNewAccountCommandHandler : ICommandHandler<CreateAccountCommand>
  {
    private readonly IAggregateRootRepository<Account> repository;

    public CreateNewAccountCommandHandler(IAggregateRootRepository<Account> repository)
    {
      this.repository = repository;
    }

    public void HandleCommand(CreateAccountCommand cmd)
    {
      if (this.CommandIsValid(cmd))
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
    }

    private bool CommandIsValid(CreateAccountCommand cmd)
    {
      return cmd != null && !String.IsNullOrWhiteSpace(cmd.Name) && cmd.Created.Date >= DateTime.Today;
    }
  }
}
