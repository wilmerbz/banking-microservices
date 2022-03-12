using Banking.Account.Command.Domain;


namespace Banking.Account.Command.Application.Contracts.Persistence
{
    public interface IEventStoreRepository : IMongoRepository<EventModel>
    {

        Task<IEnumerable<EventModel>> FindByAggregateId(string id);
    }
}
