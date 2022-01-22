namespace TechiesMoneyExchange.Core.UseCases
{
    public class GetDefaultExchangeUseCase
    {
        private const int DECIMAL_PLACES = 2;
        public Task Execute(IDefaultExchangeContext context)
        {
            var exchangeRate = context.ExchangeRate;

            context.IsBuying = true;
            context.SendingCurrency = exchangeRate.BaseCurrency;
            context.RecievingCurrency = exchangeRate.Currency;

            context.AmountYouPay = 100;
            context.AmountYouRecieve = Math.Round(context.IsBuying ?
                (context.AmountYouPay / exchangeRate.SellingRate) :
                (context.AmountYouPay * exchangeRate.BuyingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero);

            return Task.CompletedTask;
        }
    }
}
