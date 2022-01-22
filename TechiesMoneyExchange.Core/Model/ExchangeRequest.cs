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
            DateTime creationTimeUTC) : base(exchangeRate, sendingAmount, recievingAmount, operationType)
        {
            Id = id;
            OperationNo = operationNo;
            SendingAccount = sendingAccount;
            RecievingAccount = recievingAccount;
            CreationTimeUTC = creationTimeUTC;
        }
        public Guid Id { get; set; }
        public int OperationNo{ get; set; }
        public BankAccount SendingAccount { get; private set; }
        public BankAccount RecievingAccount { get; private set; }
        public DateTime CreationTimeUTC { get; private set; }
    }
}
