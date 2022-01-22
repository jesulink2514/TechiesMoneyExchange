namespace TechiesMoneyExchange.Core.UseCases
{
    public class CalculateExchangedAmountUseCase
    {
        private const decimal PRECISION_TOLERANCE = 0.01m;
        private const int DECIMAL_PLACES = 4;
        public void ExecuteForAmountYouRecieve(ICalculateExchangedAmountContext context)
        {
            var exchangeRate = context.ExchangeRate;
            var oldValue = context.AmountYouRecieve;

            var newValue = Math.Round(context.IsBuying ?
                (context.AmountYouPay / exchangeRate.SellingRate) :
                (context.AmountYouPay * exchangeRate.BuyingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero);

            if (Math.Abs(oldValue - newValue) > PRECISION_TOLERANCE)
            {
                context.AmountYouRecieve = newValue;
            }                        
        }

        public void ExecuteForAmountYouPay(ICalculateExchangedAmountContext context)
        {
            var exchangeRate = context.ExchangeRate;
            var oldValue = context.AmountYouPay;

            var newValue = Math.Round(context.IsBuying ?
                (context.AmountYouRecieve * exchangeRate.SellingRate) :
                (context.AmountYouRecieve / exchangeRate.BuyingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero);

            if (Math.Abs(oldValue - newValue) > PRECISION_TOLERANCE)
            {
                context.AmountYouPay = newValue;
            }
        }
    }
}
