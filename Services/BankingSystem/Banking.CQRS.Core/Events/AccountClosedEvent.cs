namespace Banking.CQRS.Core.Events
{
    public class AccountClosedEvent : EventBase
    {

        public AccountClosedEvent(string id) : base(id)
        {
        }
    }
}
