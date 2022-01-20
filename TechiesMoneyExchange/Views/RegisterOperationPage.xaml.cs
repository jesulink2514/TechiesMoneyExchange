using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace TechiesMoneyExchange
{
	public partial class RegisterOperationPage : ContentPage
	{
		public RegisterOperationPage()
		{
			InitializeComponent();
		}

		private async void Register_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new ConfirmationOperationPage());
        }

		private async void Page_Appearing(object sender, EventArgs e)
        {

        }
	}
}