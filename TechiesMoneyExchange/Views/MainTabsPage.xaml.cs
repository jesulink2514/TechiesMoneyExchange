namespace TechiesMoneyExchange.Views
{
    public partial class MainTabsPage: TabbedPage
	{
		public MainTabsPage()
		{
			InitializeComponent();
			SelectedItem = this.Children[2];
			Exchange.PushAsync(new ExchangePage());
		}
	}
}