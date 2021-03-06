namespace TechiesMoneyExchange.Model
{
    public class DraftExchangeRequest
    {
        public DraftExchangeRequest(
            PublishedExchangeRate exchangeRate, 
            decimal sendingAmount, 
            decimal recievingAmount, 
            ExchangeOperation operationType)
        {
            ExchangeRate = exchangeRate;
            SendingAmount = sendingAmount;
            RecievingAmount = recievingAmount;
            OperationType = operationType;
        }
        public PublishedExchangeRate ExchangeRate { get; private set; }
        public decimal SendingAmount { get; private set; }
        public decimal RecievingAmount { get; private set; }
        public ExchangeOperation OperationType { get; private set; }
    }

    public class ExchangeOperationRequest : DraftExchangeRequest
    {
        public ExchangeOperationRequest(            
            PublishedExchangeRate exchangeRate,
            decimal sendingAmount,
            decimal recievingAmount,
            ExchangeOperation operationType,
            BankAccount sendingAccount,
            BankAccount recievingAccount) : base(exchangeRate, sendingAmount, recievingAmount, operationType)
        {            
            SendingAccount = sendingAccount;
            RecievingAccount = recievingAccount;            
        }
        public BankAccount SendingAccount { get; private set; }
        public BankAccount RecievingAccount { get; private set; }       
    }

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
