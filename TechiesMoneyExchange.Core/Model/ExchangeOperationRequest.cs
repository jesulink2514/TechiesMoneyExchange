namespace TechiesMoneyExchange.Model
{
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
        public BankAccount SendingAccount { get; }
        public BankAccount RecievingAccount { get; }       
    }
}
