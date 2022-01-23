using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Infrastructure.ExternalServices
{
    public interface IBankAccountService
    {
        Task<BankAccount> GetDefaultBankAccountFor(Currency currency);
        Task<BankAccount[]> GetBankAccountsFor(Currency currency);
    }
}
