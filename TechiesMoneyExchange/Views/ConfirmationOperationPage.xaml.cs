using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Infrastructure.ExternalServices;

namespace TechiesMoneyExchange
{
    public partial class ConfirmationOperationPage : ContentPage
	{
		public ConfirmationOperationPage()
		{
			InitializeComponent();
			BindingContext = new ConfirmationOperationViewModel(new NavigationService(),new ExchangeRateService());
		}
	}
}