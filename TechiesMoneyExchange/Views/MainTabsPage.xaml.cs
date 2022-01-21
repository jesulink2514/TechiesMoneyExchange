using TechiesMoneyExchange.Core.ViewModels;

namespace TechiesMoneyExchange.Views
{
    public partial class MainTabsPage: TabbedPage
	{
		public MainTabsPage()
		{
			InitializeComponent();
			SelectedItem = this.Children[2];

			var page = new ExchangePage();
			Exchange.PushAsync(page);
			if(page.BindingContext is INavigationAware vm)
            {
				vm.OnNavigatedTo(new Dictionary<string, object>());
            }
		}
	}
}