namespace Banking.CQRS.Core.Events
{
    public class FundsDepositedEvent : EventBase
    {
        public double Amount { get; set; }

        public FundsDepositedEvent(string id, double amount) : base(id)
        {
            Amount = amount;
        }

    }
}
