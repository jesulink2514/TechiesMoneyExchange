using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Infrastructure.ExternalServices
{
    public interface IExchangeRateService
    {
        Task<PublishedExchangeRate> GetCurrentExchangeRate();
        Task<BankAccount> GetExchangeBankAccountFor(Currency currency);
    }
}
