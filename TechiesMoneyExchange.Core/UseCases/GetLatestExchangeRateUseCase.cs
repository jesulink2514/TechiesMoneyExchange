using Polly;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.UseCases
{
    public class GetLatestExchangeRateUseCase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public GetLatestExchangeRateUseCase(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        public async Task<PublishedExchangeRate> Execute()
        {
            var policy = Policy.Handle<Exception>().RetryAsync(3);

            return await policy.ExecuteAsync(async () =>
            {

                return await _exchangeRateService.GetCurrentExchangeRate();
            });
        }
    }
}
