using BalanceMonitor.Accounting.Application.Projections.Interfaces;
using BalanceMonitor.Accounting.Domain.Events;
using BalanceMonitor.Infrastructure.Core.Interfaces.DDD;
using BalanceMonitor.Infrastructure.Interfaces.Logging;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BalanceMonitor.Accounting.Application.Projections
{
    public class AccountDailyBalanceDenormaliser : IAccountDailyBalanceQuerier,
                                                   IHandleEvents<AmountDepositedEvent>,
                                                   IHandleEvents<AmountWithdrawalEvent>,
                                                   IHandleEvents<AccountCreatedEvent>
    {
        //private readonly ILogger logger;

        public AccountDailyBalanceDenormaliser()
        {
            //this.logger = log;
            //this.session = session;
        }

        public void Handle(AccountCreatedEvent @event)
        {
            //this.logger.Log(String.Format("Account created event {0} @ {1}", @event.Name, DateTimeOffset.Now));

            AccountDailyBalanceSession ctx = new AccountDailyBalanceSession();
            AccountDailyBalance newAcc = ctx.AccountDailyBalance.FirstOrDefault(o => o.AccountId == @event.AggregateId);
            if (newAcc == null)
            {
                newAcc = new AccountDailyBalance()
                {
                    AccountId = @event.AggregateId,
                    AccountName = @event.Name,
                    Date = @event.DateOccured,
                    Balance = @event.OpeningBalance
                };
                ctx.AccountDailyBalance.Add(newAcc);
            }
            else
            {
                throw new Exception(String.Format("Account already exists with Id: {0}", @event.AggregateId));
            }
        }

        public void Handle(AmountDepositedEvent @event)
        {
            //this.logger.Log(String.Format("Account deposited event @ {0}", DateTimeOffset.Now));

            AccountDailyBalanceSession ctx = new AccountDailyBalanceSession();
            AccountDailyBalance newAcc = ctx.AccountDailyBalance.FirstOrDefault(o => o.AccountId == @event.AggregateId);
            if (newAcc != null)
            {
                newAcc.Date = @event.DateOccured;
                newAcc.AccountName = newAcc.AccountName + @event.DateOccured;
            }
            else
            {
                throw new Exception(String.Format("Account does not exists with Id: {0}", @event.AggregateId));
            }
        }

        public void Handle(AmountWithdrawalEvent @event)
        {
            //this.logger.Log(String.Format("Account withdrawl event @ {0}", DateTimeOffset.Now));

            AccountDailyBalanceSession ctx = new AccountDailyBalanceSession();
            AccountDailyBalance newAcc = ctx.AccountDailyBalance.FirstOrDefault(o => o.AccountId == @event.AggregateId);
            if (newAcc != null)
            {
                newAcc.Date = @event.DateOccured;
                newAcc.AccountName = newAcc.AccountName + @event.DateOccured;
            }
            else
            {
                throw new Exception(String.Format("Account does not exists with Id: {0}", @event.AggregateId));
            }
        }

        public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(DateTime date)
        {
            AccountDailyBalanceSession ctx = new AccountDailyBalanceSession();
            return ctx.AccountDailyBalance.Where(o => o.Date.Date == date.Date);
        }

        public IEnumerable<AccountDailyBalance> GetAccountBalanceOn(Guid accId, DateTime date)
        {
            AccountDailyBalanceSession ctx = new AccountDailyBalanceSession();
            return ctx.AccountDailyBalance.Where(o => o.Date.Date == date.Date && o.AccountId == accId);
        }
    }
}
