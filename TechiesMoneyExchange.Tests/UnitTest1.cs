using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechiesMoneyExchange.Infrastructure.ExternalServices;

namespace TechiesMoneyExchange.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var exchangeService = new ExchangeRateService();
            var rate = await exchangeService.GetCurrentExchangeRate();
            
        }
    }
}