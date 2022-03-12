namespace Banking.CQRS.Core.Events
{
    public class AccountOpenedEvent : EventBase
    {
        public string AccountHolder { get; set; } = string.Empty;

        public string AccountType { get; set; } = string.Empty;

        public DateOnly CreatedDate { get; set; }

        public double OpeningBalance { get; set; }

        public AccountOpenedEvent(string id, string accountHolder, string accountType, DateOnly createdDate, double openingBalance) : base(id)
        {
            AccountHolder = accountHolder;
            AccountType = accountType;
            CreatedDate = createdDate;
            OpeningBalance = openingBalance;
        }

    }
}
