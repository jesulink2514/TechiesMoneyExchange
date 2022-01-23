using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Infrastructure.ExternalServices
{
    public interface IExchangeRateService
    {
        Task<PublishedExchangeRate> GetCurrentExchangeRate();
        Task<BankAccount> GetExchangeBankAccountFor(Currency currency);
        Task<ExchangeRequest> RegisterOperation(ExchangeOperationRequest request);
        Task<ExchangeRequest[]> GetLast7daysOperations();
    }
}
