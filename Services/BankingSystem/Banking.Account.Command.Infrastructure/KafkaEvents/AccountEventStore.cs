using Banking.Account.Command.Application.Aggregates;
using Banking.Account.Command.Application.Contracts.Persistence;
using Banking.Account.Command.Domain;
using Banking.CQRS.Core.Events;
using Banking.CQRS.Core.Infrastructure;
using Banking.CQRS.Core.Producers;

namespace Banking.Account.Command.Infrastructure.KafkaEvents
{
    /// <summary>
    /// Event Store for Account related events.
    /// </summary>
    public class AccountEventStore : IEventStore
    {

        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventProducer _eventProducer;

        public AccountEventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer)
        {
            _eventStoreRepository = eventStoreRepository;
            _eventProducer = eventProducer;
        }

        public async Task<List<EventBase>> GetEvents(string aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

            if (eventStream == null || !eventStream.Any())
            {
                throw new Exception("The bank account doesn't exists. ");
            }

            var events = eventStream.Select(x => x.EventData).ToList();
            return events;
        }

        public async Task SaveEvent(string aggregateId, IEnumerable<EventBase> events, int expectedVersion)
        {

            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

            // Validate if is there any problem with the event based on its version.
            if (expectedVersion != -1 && eventStream.ElementAt(eventStream.Count() - 1).Version != expectedVersion)
            {
                throw new Exception("Concurrency Error");
            }

            var version = expectedVersion;

            foreach (var eventToSave in events)
            {
                version++;
                eventToSave.Version = version;
                var eventTypeName = eventToSave.GetType().Name;
                var eventModel = new EventModel
                {
                    Timespan = DateTime.Now,
                    AggregateIdentifier = aggregateId,
                    AggregateType = nameof(AccountAggregate),
                    Version = version,
                    EventType = eventTypeName,
                    EventData = eventToSave
                };

                await _eventStoreRepository.InsertDocument(eventModel);

                _eventProducer.Produce(eventTypeName, eventToSave);

            }

        }
    }

}
