namespace Banking.CQRS.Core.Events
{
    public class FundsWithdrawnEvent : EventBase
    {
        public double Amount { get; set; }

        public FundsWithdrawnEvent(string id, double amount): base(id)
        {
            Amount = amount;
        }
    }
}
