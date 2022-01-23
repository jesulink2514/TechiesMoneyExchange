using System;
using DryIoc;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using TechiesMoneyExchange.Core.ViewModels;

namespace TechiesMoneyExchange.Views
{
	public partial class OperationsPage : ContentPage
	{
		public OperationsPage()
		{
			InitializeComponent();
            BindingContext = App.ServiceLocator.Resolve<ExchangeOperationsViewModel>();
        }
	}
}