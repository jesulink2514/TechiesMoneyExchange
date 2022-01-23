namespace TechiesMoneyExchange.Model
{
    public class DraftExchangeRequest
    {
        public DraftExchangeRequest(
            PublishedExchangeRate exchangeRate, 
            decimal sendingAmount,             
            ExchangeOperation operationType)
        {
            ExchangeRate = exchangeRate;
            SendingAmount = sendingAmount;
            RecievingAmount = exchangeRate.CalculateRecievingAmount(operationType,SendingAmount);
            OperationType = operationType;
        }
        public PublishedExchangeRate ExchangeRate { get; }
        public decimal SendingAmount { get; }
        public decimal RecievingAmount { get; }
        public ExchangeOperation OperationType { get;  }
    }
}
