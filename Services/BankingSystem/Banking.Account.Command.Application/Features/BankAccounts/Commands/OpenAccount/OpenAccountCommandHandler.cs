using Banking.Account.Command.Application.Aggregates;
using Banking.CQRS.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccounts.Commands.OpenAccount
{

    public class OpenAccountCommandHandler : IRequestHandler<OpenAccountCommand, bool>
    {

        private readonly IEventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public OpenAccountCommandHandler(IEventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(OpenAccountCommand request, CancellationToken cancellationToken)
        {
            // The Aggregate creates, manages, and queries events.
            // Event Sourcing Handlder manages the aggregates.

            // Create Aggregate instance.
            // In this case it is a new account, so it will be a new object

            var aggregate = new AccountAggregate(request);
            await _eventSourcingHandler.Save(aggregate);

            return true;

        }
    }
}
