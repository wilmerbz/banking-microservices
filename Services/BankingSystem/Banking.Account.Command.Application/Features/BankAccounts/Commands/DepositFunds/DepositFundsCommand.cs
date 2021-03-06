using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccounts.Commands.DepositFunds
{
    public class DepositFundsCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;

        public double Amount { get; set; }

    }
}
