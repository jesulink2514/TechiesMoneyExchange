using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.ViewModels;

namespace TechiesMoneyExchange.Views;

public partial class ExchangePage : ContentPage
{
	public ExchangePage()
	{
		InitializeComponent();
		BindingContext = new ExchangeViewModel(new ExchangeRateService(),new NavigationService());
	}
}

