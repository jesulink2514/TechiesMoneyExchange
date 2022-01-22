using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Infrastructure.ExternalServices
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly Bank[] Banks = new Bank[]
       {
            new Bank(1, "ABC Bank"),
            new Bank(1, "DEF Bank")
       };
        public async Task<PublishedExchangeRate> GetCurrentExchangeRate()
        {
            await Task.Delay(1000);

            var randomVariance = ((decimal)new Random().Next(-10,10))/100m;

            return new PublishedExchangeRate(Guid.NewGuid(),
                DateTime.UtcNow,
                TimeSpan.FromMinutes(5),
                new Currency()
                {
                    Id = 2,
                    Name = "USD",
                    Symbol = "$"
                },
                new Currency
                {
                    Id = 1,
                    Name = "PEN",
                    Symbol = "S/."
                },
                3.98m + randomVariance,
                3.98m - randomVariance);
        }

        public async Task<BankAccount> GetExchangeBankAccountFor(Currency currency)
        {
            await Task.Delay(2000);

            return new BankAccount(Guid.NewGuid(),            
                currency,
                "TechiesMoneyExchange co.",
                Banks[1],
                "13245645664456",
                BankAccountType.Savings);
        }

        public async Task<ExchangeRequest> RegisterOperation(ExchangeOperationRequest request)
        {
            await Task.Delay(2500);

            var result = new ExchangeRequest(Guid.NewGuid(),
                
                new Random().Next(123456,999999),
                
                request.ExchangeRate,
                
                request.SendingAmount,
                request.RecievingAmount,
                
                request.OperationType,

                request.SendingAccount,
                request.RecievingAccount,
                DateTime.UtcNow
                );

            return result;
        }
    }
}
