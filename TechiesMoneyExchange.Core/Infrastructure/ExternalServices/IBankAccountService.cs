using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.Infrastructure.ExternalServices
{
    public interface IBankAccountService
    {
        Task<BankAccount> GetDefaultBankAccountFor(Currency currency);
        Task<BankAccount[]> GetBankAccountsFor(Currency currency);
    }
}
