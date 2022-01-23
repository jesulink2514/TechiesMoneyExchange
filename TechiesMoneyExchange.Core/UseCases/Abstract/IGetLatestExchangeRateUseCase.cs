using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.UseCases
{
    public interface IGetLatestExchangeRateUseCase
    {
        Task<PublishedExchangeRate> Execute();
    }
}