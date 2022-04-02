using Banking.Account.Command.Application.Features.BankAccounts.Commands;
using Banking.Account.Command.Application.Features.BankAccounts.Commands.OpenAccount;
using Banking.CQRS.Core.Domain;
using Banking.CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Command.Application.Aggregates
{
    /// <summary>
    /// Manages the events related to Account management.
    /// </summary>
    public class AccountAggregate : AggregateRoot
    {

        public bool Active { get; set; }

        public double Balance { get; set; }

        public AccountAggregate()
        {
        }

        #region Open Account

        public AccountAggregate(OpenAccountCommand command)
        {
            // Event object to be inserted on the events DB and send using Apache Kafka
            var accountOpenedEvent = new AccountOpenedEvent(command.Id, command.AccountHolder, command.AccountType, DateTime.UtcNow, command.OpeningBalance);

            RaiseEvent(accountOpenedEvent);
        }

        /// <summary>
        /// Open/Create a new account.
        /// </summary>
        /// <param name="eventToApply">Event with the opened account details.</param>
        public void Apply(AccountOpenedEvent eventToApply)
        {
            Id = eventToApply.Id;
            Active = true;
            Balance = eventToApply.OpeningBalance;

        }

        #endregion


        #region Deposit

        /// <summary>
        /// Deposit the given amount into the account.
        /// </summary>
        /// <param name="amount">Amount to deposit.</param>
        /// <exception cref="Exception">The account is inactive or the amount is not greater than zero.</exception>
        public void Deposit(double amount)
        {
            if (!Active)
            {
                throw new Exception("Funds cannot be deposit into an inactive account.");
            }

            if (amount <= 0)
            {
                throw new Exception("The deposit amount must be greater than zero (0).");
            }

            var fundsDepositedEvent = new FundsDepositedEvent(Id, amount);

            RaiseEvent(fundsDepositedEvent);
        }


        /// <summary>
        /// Deposit funds into the account.
        /// </summary>
        /// <param name="eventToApply">Event with the funds deposited details.</param>
        public void Apply(FundsDepositedEvent eventToApply)
        {
            Id = eventToApply.Id;
            Balance += eventToApply.Amount;
        }

        #endregion


        #region Withdraw

        /// <summary>
        /// Withdraw the given amount from the account.
        /// </summary>
        /// <param name="amount">Amount to withdraw.</param>
        /// <exception cref="Exception">The account is inactive or the amount is not greater than zero.</exception>
        public void Withdraw(double amount)
        {
            if (!Active)
            {
                throw new Exception("The account is closed.");
            }

            if (amount <= 0)
            {
                throw new Exception("The withdraw amount must be greater than zero (0).");
            }

            if (amount > Balance)
            {
                throw new Exception("Not enough funds.");
            }

            var fundsWithdrawnEvent = new FundsWithdrawnEvent(Id, amount);

            RaiseEvent(fundsWithdrawnEvent);
        }


        /// <summary>
        /// Withdraw funds from the account.
        /// </summary>
        /// <param name="eventToApply">Event with the funds withdraw details.</param>
        public void Apply(FundsWithdrawnEvent eventToApply)
        {
            Id = eventToApply.Id;
            Balance -= eventToApply.Amount;

        }

        #endregion


        #region Close account

        /// <summary>
        /// Withdraw the given amount from the account.
        /// </summary>
        /// <param name="amount">Amount to withdraw.</param>
        /// <exception cref="Exception">The account is inactive or the amount is not greater than zero.</exception>
        public void Close()
        {
            if (!Active)
            {
                throw new Exception("The account is closed.");
            }
            
            var accountClosedEvent = new AccountClosedEvent(Id);

            RaiseEvent(accountClosedEvent);
        }


        /// <summary>
        /// Withdraw funds from the account.
        /// </summary>
        /// <param name="eventToApply">Event with the funds withdraw details.</param>
        public void Apply(AccountClosedEvent eventToApply)
        {
            Id = eventToApply.Id;
            Active = false;
        }

        #endregion

    }
}
