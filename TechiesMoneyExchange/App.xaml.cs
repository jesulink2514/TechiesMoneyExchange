using DryIoc;
using TechiesMoneyExchange.Core.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Core.Infrastructure.Integration;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.ViewModels;
using TechiesMoneyExchange.Views;

namespace TechiesMoneyExchange;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        
        ConfigureServices();

        MainPage = new MainTabsPage();
        var tabbedPage = MainPage as TabbedPage;
        tabbedPage.SelectedItem = tabbedPage.Children[2];
    }
    public static IResolver ServiceLocator { get;private set; }
	protected override async void OnStart()
    {                
        var navigationService = ServiceLocator.GetService<INavigationService>();
        var page = navigationService.ResolvePage(Pages.ExchangeStart);
        var tabbedPage = MainPage as TabbedPage;
        await tabbedPage.Children[2].Navigation.PushAsync(page);        
        if (page.BindingContext is INavigationAware vm)
        {
            vm.OnNavigatedTo(new Dictionary<string, object>());
        }
    }

    public ExchangeOperationsViewModel OperationsViewModel => ServiceLocator.Resolve<ExchangeOperationsViewModel>();
    private static void ConfigureServices()
    {
        var container = new DryIoc.Container();

        container.Register<INavigationService, NavigationService>(Reuse.Singleton);
        container.Register<IEventAggregator,EventAggregator>(Reuse.Singleton);

        container.Register<IExchangeRateService, ExchangeRateService>(Reuse.Singleton);
        container.Register<IBankAccountService,BankAccountService>(Reuse.Singleton);

        container.Register<ExchangeViewModel>(Reuse.Transient);
        container.Register<RegisterOperationViewModel>(Reuse.Transient);
        container.Register<ConfirmationOperationViewModel>(Reuse.Transient);
        
        container.Register<ExchangeOperationsViewModel>(Reuse.Transient);

        ServiceLocator = container;
    }
}
