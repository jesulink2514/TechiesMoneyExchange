using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.UseCases
{
    public interface IApplyNewExchangedAmountContext
    {
        PublishedExchangeRate ExchangeRate { get; }

        decimal AmountYouPay { get; set; }
        decimal AmountYouRecieve { get; set; }
        Currency SendingCurrency { get; set; }
        Currency RecievingCurrency { get; set; }
        bool IsBuying { get; set; }
    }
}
