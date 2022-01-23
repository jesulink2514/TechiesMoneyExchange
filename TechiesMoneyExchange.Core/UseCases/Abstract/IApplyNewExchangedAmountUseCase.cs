namespace TechiesMoneyExchange.Core.UseCases
{
    public interface IApplyNewExchangedAmountUseCase
    {
        void ExecuteForAmountYouPay(IApplyNewExchangedAmountContext context);
        void ExecuteForAmountYouRecieve(IApplyNewExchangedAmountContext context);
    }
}