namespace TechiesMoneyExchange.Model
{
    public class ExchangeRequest : DraftExchangeRequest
    {
        public ExchangeRequest(
            Guid id,
            int operationNo,
            PublishedExchangeRate exchangeRate,
            decimal sendingAmount,
            decimal recievingAmount,
            ExchangeOperation operationType,
            BankAccount sendingAccount,
            BankAccount recievingAccount,
            DateTime creationTimeUTC,
            ExchangeOperationStatus status
            ) : base(exchangeRate, sendingAmount, operationType)
        {
            Id = id;
            OperationNo = operationNo;
            SendingAccount = sendingAccount;
            RecievingAccount = recievingAccount;
            CreationTimeUTC = creationTimeUTC;
            Status = status;
        }
        public Guid Id { get; private set; }
        public int OperationNo { get; private set; }
        public BankAccount SendingAccount { get; private set; }
        public BankAccount RecievingAccount { get; private set; }
        public DateTime CreationTimeUTC { get; private set; }
        public ExchangeOperationStatus Status { get; private set; }
    }
    public enum ExchangeOperationStatus
    {
        Pending,
        Completed
    }
}
