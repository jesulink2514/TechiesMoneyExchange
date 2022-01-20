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
        public async Task<PublishedExchangeRate> GetCurrentExchangeRate()
        {
            await Task.Delay(1000);

            var randomVariance = ((decimal)new Random().Next(-10,10))/10m;

            return new PublishedExchangeRate(Guid.NewGuid(),
                DateTime.UtcNow,
                TimeSpan.FromMinutes(5),
                new ConversionRate(new Currency
                {
                    Id = 1,
                    Name = "PEN",
                    Symbol = "S/."
                },0.2512m + randomVariance),
                new ConversionRate(new Currency()
                {
                    Id = 2,
                    Name = "USD",
                    Symbol = "$"
                },3.98m + randomVariance));
        }
    }
    public interface IExchangeRateService
    {
        Task<PublishedExchangeRate> GetCurrentExchangeRate();
    }
}
