using Banking.CQRS.Core.Domain;

namespace Banking.CQRS.Core.Handlers
{
    public interface IEventSourcingHandler<T>
    {
        Task Save(AggregateRoot aggregate);
        Task<T> GetById(string id);
    }
}
