using Banking.Account.Command.Application.Aggregates;
using Banking.CQRS.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Application.Features.BankAccounts.Commands.DepositFunds
{
    public class DepositFundsCommandHandler : IRequestHandler<DepositFundsCommand, bool>
    {

        private readonly IEventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public DepositFundsCommandHandler(IEventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(DepositFundsCommand request, CancellationToken cancellationToken)
        {
            // The Aggregate creates, manages, and queries events.
            // Event Sourcing Handlder manages the aggregates.

            var aggregate = await _eventSourcingHandler.GetById(request.Id);

            aggregate.Deposit(request.Amount);

            await _eventSourcingHandler.Save(aggregate);

            return true;

        }
    }
}
