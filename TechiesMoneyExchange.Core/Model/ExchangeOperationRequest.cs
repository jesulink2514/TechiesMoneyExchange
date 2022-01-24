namespace TechiesMoneyExchange.Model
{
    public class ExchangeOperationRequest : DraftExchangeRequest
    {
        public ExchangeOperationRequest(            
            PublishedExchangeRate exchangeRate,
            decimal sendingAmount,            
            ExchangeOperation operationType,
            BankAccount sendingAccount,
            BankAccount recievingAccount) : base(exchangeRate, sendingAmount, operationType)
        {            
            SendingAccount = sendingAccount;
            RecievingAccount = recievingAccount;            
        }
        public BankAccount SendingAccount { get; }
        public BankAccount RecievingAccount { get; }      
        public bool IsUsingValidExchangeRate => ExchangeRate.IsStillValid;
    }
}
