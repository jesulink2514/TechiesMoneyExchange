using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.UseCases
{
    public class RegisterExchangeOperationUseCase : IRegisterExchangeOperationUseCase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly INavigationService _navigationService;

        public RegisterExchangeOperationUseCase(
            IExchangeRateService exchangeRateService,
            INavigationService navigationService)
        {
            _exchangeRateService = exchangeRateService;
            _navigationService = navigationService;
        }

        public async Task Execute(IRegisterExchangeOperationContext context)
        {
            var exchangeRate = context.PublishedExchangeRate;

            var exchangeRequest = new ExchangeOperationRequest(
                exchangeRate,
                context.SendingAmount,
                context.IsBuying.ToExchangeOperation(),
                context.SendingAccount,
                context.RecievingAccount);

            var result = await _exchangeRateService.RegisterOperation(exchangeRequest);


            await _navigationService.NavigateTo(Pages.ConfirmationExchange, new Dictionary<string, object>
            {
                { "operation", result }
            });
        }
    }
}
