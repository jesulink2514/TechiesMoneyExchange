namespace TechiesMoneyExchange.Views;

public partial class ExchangePage : ContentPage
{
	public ExchangePage()
	{
		InitializeComponent();
	}

	private async void Start_Clicked(object sender, EventArgs e)
    {
		await this.Navigation.PushAsync(new RegisterOperationPage());
    }
}

