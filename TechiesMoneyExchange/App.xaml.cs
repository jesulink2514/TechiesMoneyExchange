using TechiesMoneyExchange.Views;

namespace TechiesMoneyExchange;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainTabsPage();
	}
}
