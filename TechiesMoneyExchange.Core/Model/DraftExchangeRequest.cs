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
        public PublishedExchangeRate ExchangeRate { get; }
        public decimal SendingAmount { get; }
        public decimal RecievingAmount { get; }
        public ExchangeOperation OperationType { get;  }
    }
}
