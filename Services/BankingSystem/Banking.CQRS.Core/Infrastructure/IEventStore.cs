using Banking.CQRS.Core.Events;

namespace Banking.CQRS.Core.Infrastructure
{
    public interface IEventStore
    {

        /// <summary>
        /// Saves the event into the Event Store and sends it using the distribution platform.
        /// </summary>
        /// <param name="aggregateId">Unique identifier of the aggregate to save the events.</param>
        /// <param name="events">List of events to save.</param>
        /// <param name="expectedVersion">Version of the events to save.</param>
        /// <returns>Task.</returns>
        Task SaveEvent(string aggregateId, IEnumerable<EventBase> events, int expectedVersion);

        /// <summary>
        /// Gets the events from the event store.
        /// </summary>
        /// <param name="aggregateId">Unique identifier of the aggregate to get the events.</param>
        /// <returns>List of Events.</returns>
        Task<List<EventBase>> GetEvents(string aggregateId);
    }
}
