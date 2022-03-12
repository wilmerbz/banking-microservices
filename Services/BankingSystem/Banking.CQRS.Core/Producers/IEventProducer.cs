using Banking.CQRS.Core.Events;

namespace Banking.CQRS.Core.Producers
{
    public interface IEventProducer
    {

        void Produce(string topic, EventBase eventToProduce);

    }
}
