using Banking.Account.Command.Application.Aggregates;
using Banking.CQRS.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccounts.Commands.CloseAccount
{
    public class CloseAccountCommandHandler : IRequestHandler<CloseAccountCommand, bool>
    {

        private readonly IEventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public CloseAccountCommandHandler(IEventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            this._eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(CloseAccountCommand request, CancellationToken cancellationToken)
        {
            // The Aggregate creates, manages, and queries events.
            // Event Sourcing Handlder manages the aggregates.

            // Query an existing aggregate.

            var aggregate = await _eventSourcingHandler.GetById(request.Id);

            aggregate.Close();

            await _eventSourcingHandler.Save(aggregate);

            return true;

        }
    }
}
