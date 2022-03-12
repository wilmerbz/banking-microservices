using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccounts
{
    public class CloseAccountCommand:IRequest<bool>
    {
        public string AccountId { get; set; } = string.Empty;

    }
}
