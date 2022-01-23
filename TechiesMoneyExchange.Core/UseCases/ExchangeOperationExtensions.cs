namespace TechiesMoneyExchange.Core.UseCases
{
    public static class ExchangeOperationExtensions
    {
        public static Model.ExchangeOperation ToExchangeOperation(this bool isBuying)
        {
            return isBuying ? Model.ExchangeOperation.Buy : Model.ExchangeOperation.Sell;
        }
    }
}
