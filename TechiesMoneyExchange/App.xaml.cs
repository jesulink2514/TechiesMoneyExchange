using DryIoc;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.ViewModels;
using TechiesMoneyExchange.Views;
using TechiesMoneyExchange.Core.UseCases;

namespace TechiesMoneyExchange;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainTabsPage();		
	}
	public static IResolver ServiceLocator { get;private set; }
	protected override async void OnStart()
    {
        ConfigureServices();

        var tabbedPage = MainPage as TabbedPage;
        tabbedPage.SelectedItem = tabbedPage.Children[2];
        
        var navigationService = ServiceLocator.GetService<INavigationService>();
        var page = navigationService.ResolvePage(Pages.ExchangeStart);
        await tabbedPage.Children[2].Navigation.PushAsync(page);        
        if (page.BindingContext is INavigationAware vm)
        {
            vm.OnNavigatedTo(new Dictionary<string, object>());
        }
    }

    private static void ConfigureServices()
    {
        var container = new DryIoc.Container();

        container.Register<INavigationService, NavigationService>(Reuse.Singleton);

        container.Register<IExchangeRateService, ExchangeRateService>(Reuse.Singleton);
        container.Register<IBankAccountService,BankAccountService>(Reuse.Singleton);

        container.Register<IGetDefaultExchangeUseCase,GetDefaultExchangeUseCase>(Reuse.Singleton);
        container.Register<IGetLatestExchangeRateUseCase, GetLatestExchangeRateUseCase>(Reuse.Singleton);
        container.Register<IApplyNewExchangedAmountUseCase, ApplyNewExchangedAmountUseCase>(Reuse.Singleton);
        container.Register<IRegisterExchangeOperationUseCase, RegisterExchangeOperationUseCase>(Reuse.Singleton);

        container.Register<ExchangeViewModel>(Reuse.Transient);
        container.Register<RegisterOperationViewModel>(Reuse.Transient);
        container.Register<ConfirmationOperationViewModel>(Reuse.Transient);
        container.Register<ExchangeOperationsViewModel>(Reuse.Transient);

        ServiceLocator = container;
    }
}
