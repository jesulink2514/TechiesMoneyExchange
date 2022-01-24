using CommunityToolkit.Maui.Alerts;
using TechiesMoneyExchange.Core.Infrastructure.Interactors;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.UseCases
{
    public class RegisterExchangeOperationUseCase : IRegisterExchangeOperationUseCase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public RegisterExchangeOperationUseCase(
            IExchangeRateService exchangeRateService,
            INavigationService navigationService,
            IDialogService dialogService)
        {
            _exchangeRateService = exchangeRateService;
            _navigationService = navigationService;
            _dialogService = dialogService;
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

            if (!exchangeRequest.IsUsingValidExchangeRate)
            {
                await _dialogService.ShowMessage("Your exchange rate is no longer valid.");
                return;
            }

            var result = await _exchangeRateService.RegisterOperation(exchangeRequest);

            await _navigationService.NavigateTo(Pages.ConfirmationExchange, new Dictionary<string, object>
            {
                { "operation", result }
            });
        }
    }
}
