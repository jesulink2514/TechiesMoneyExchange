namespace TechiesMoneyExchange.Core.UseCases
{
    public class ApplyNewExchangedAmountUseCase : IApplyNewExchangedAmountUseCase
    {
        private const decimal PRECISION_TOLERANCE = 0.01m;

        public void ExecuteForAmountYouRecieve(IApplyNewExchangedAmountContext context)
        {
            var exchangeRate = context.ExchangeRate;
            var oldValue = context.AmountYouRecieve;

            var newValue = exchangeRate.CalculateRecievingAmount(context.IsBuying.ToExchangeOperation(), context.AmountYouPay);

            if (Math.Abs(oldValue - newValue) > PRECISION_TOLERANCE)
            {
                context.AmountYouRecieve = newValue;
            }
        }

        public void ExecuteForAmountYouPay(IApplyNewExchangedAmountContext context)
        {
            var exchangeRate = context.ExchangeRate;
            var oldValue = context.AmountYouPay;

            var newValue = exchangeRate.CalculateSendingAmount(context.IsBuying.ToExchangeOperation(), context.AmountYouRecieve);

            if (Math.Abs(oldValue - newValue) > PRECISION_TOLERANCE)
            {
                context.AmountYouPay = newValue;
            }
        }
    }
}
