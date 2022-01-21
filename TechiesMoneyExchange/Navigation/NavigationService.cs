using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Views;

namespace TechiesMoneyExchange.Core.Infrastructure.Navigation
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateTo(Pages page,  IReadOnlyDictionary<string, object> parameters = null)
        {
            Page pageToNavigate;
            switch (page)
            {
                case Pages.ExchangeStart:
                    pageToNavigate = new ExchangePage();
                    break;
                case Pages.RegisterExchange:
                    pageToNavigate = new RegisterOperationPage();
                    break;
                case Pages.ConfirmationExchange:
                    pageToNavigate = new ConfirmationOperationPage();
                    break;
                case Pages.Operations:
                    pageToNavigate = new OperationsPage();
                    break;
                default:
                    pageToNavigate = new ExchangePage();
                    break;
            }

            var navigation = GetNavigation();
            await navigation.PushAsync(pageToNavigate);
            if(pageToNavigate is INavigationAware vm)
            {
                vm.OnNavigatedTo(parameters ?? new Dictionary<string,object>());
            }
        }

        public async Task GoBack()
        {
           await GetNavigation().PopAsync();
        }

        private INavigation GetNavigation()
        {
            if (Application.Current.MainPage is TabbedPage tabbed)
            {
                if (tabbed.CurrentPage is NavigationPage navigationPage)
                {
                    return navigationPage.Navigation;
                }
                else
                {
                    return tabbed.Navigation;
                }
            }
            else
            {
                return Application.Current.MainPage.Navigation;
            }
        }
    }
}
