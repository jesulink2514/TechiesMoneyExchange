using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.ViewModels;
using TechiesMoneyExchange.Views;

namespace TechiesMoneyExchange.Core.Infrastructure.Navigation
{
    public class NavigationService : INavigationService
    {
        public static readonly IReadOnlyDictionary<Pages,Type>
            PageToViewModelMapping= new Dictionary<Pages, Type>
            {
                { Pages.ExchangeStart,          typeof(ExchangeViewModel) },
                { Pages.RegisterExchange,       typeof(RegisterOperationViewModel) },
                { Pages.ConfirmationExchange,   typeof(ConfirmationOperationViewModel) }
            };

        public async Task NavigateTo(Pages page,  IReadOnlyDictionary<string, object> parameters = null, bool clearHistory =false)
        {
           var pageToNavigate = ResolvePage(page);

            var navigation = GetNavigation();

            if (clearHistory)
            {
                await navigation.PopToRootAsync();
            }

            await navigation.PushAsync(pageToNavigate);
            if(pageToNavigate.BindingContext is INavigationAware vm)
            {
                vm.OnNavigatedTo(parameters ?? new Dictionary<string,object>());
            }
        }        

        public async Task GoBack()
        {
           await GetNavigation().PopAsync();
        }

        public Page ResolvePage(Pages page)
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

            //if (PageToViewModelMapping.ContainsKey(page))
            //{
            //    var resolvedVm = App.ServiceLocator.Resolve(PageToViewModelMapping[page], DryIoc.IfUnresolved.ReturnDefault);
            //    pageToNavigate.BindingContext = resolvedVm;
            //}

            return pageToNavigate;
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
