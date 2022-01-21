using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Infrastructure.ExternalServices
{
    public class BankAccountService : IBankAccountService
    {
        private readonly Bank[] Banks = new Bank[]
        {
            new Bank(1, "ABC Bank"),
            new Bank(1, "DEF Bank")
        };
        public async Task<BankAccount[]> GetBankAccountsFor(Currency currency)
        {
            await Task.Delay(2000);

            return new BankAccount[]{ new BankAccount(Guid.NewGuid(),
                currency,
                "Jesus Angulo",
                Banks[0],
                "13245645664456",
                BankAccountType.Savings) };
        }

        public async Task<BankAccount> GetDefaultBankAccountFor(Currency currency)
        {
            await Task.Delay(2000);

            return new BankAccount(Guid.NewGuid(),
                currency,
                "Jesus Angulo",
                Banks[0],
                "13245645664456",
                BankAccountType.Savings);
        }
    }
}
