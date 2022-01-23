namespace TechiesMoneyExchange.Core.UseCases
{
    public class GetDefaultExchangeUseCase: IGetDefaultExchangeUseCase
    {        
        public Task Execute(IDefaultExchangeContext context)
        {
            var exchangeRate = context.ExchangeRate;

            //default buy 100
            context.IsBuying = true;
            context.SendingCurrency = exchangeRate.BaseCurrency;
            context.RecievingCurrency = exchangeRate.Currency;

            context.AmountYouPay = 100;
            context.AmountYouRecieve = exchangeRate.CalculateRecievingAmount(Model.ExchangeOperation.Buy,100m);

            return Task.CompletedTask;
        }
    }
}
