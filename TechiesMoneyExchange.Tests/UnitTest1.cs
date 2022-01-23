using System.Threading.Tasks;
using TechiesMoneyExchange.Core.Infrastructure.ExternalServices;
using Xunit;

namespace TechiesMoneyExchange.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var exchangeService = new ExchangeRateService();
            var rate = await exchangeService.GetCurrentExchangeRate();

        }
    }
}