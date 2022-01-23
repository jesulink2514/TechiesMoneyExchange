using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.ViewModels;

namespace TechiesMoneyExchange.Views
{
	public partial class RegisterOperationPage : ContentPage
	{
		public RegisterOperationPage()
		{
			InitializeComponent();
			BindingContext = new RegisterOperationViewModel(new NavigationService(),new BankAccountService(),new ExchangeRateService());
		}	
	}
}