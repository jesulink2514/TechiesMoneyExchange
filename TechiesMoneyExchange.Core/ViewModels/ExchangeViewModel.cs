using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.ViewModels
{
    public class ExchangeViewModel : INotifyPropertyChanged, INavigationAware
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly INavigationService _navigationService;

        public ExchangeViewModel(
            IExchangeRateService exchangeRateService,
            INavigationService navigationService)
        {
            _exchangeRateService = exchangeRateService;
            _navigationService = navigationService;
            SwitchCommand = new Command(OnSwitchExecute);
            StartCommand = new Command(OnStartExecute);
        }

        private PublishedExchangeRate exchangeRate;
        
        public decimal AmountYouPay { get; set; }
        public decimal AmountYouRecieve { get; set; }
        public decimal BuyingRate { get; private set;}        
        public decimal SellingRate { get;private set;}
        public Currency SendingCurrency { get;private set; }
        public Currency RecievingCurrency { get;private set;}
        public Currency MainCurrency { get;private set;}
        public Currency BaseCurrency { get; private set; }
        public bool IsBuying { get; set; }

        public ICommand SwitchCommand { get;private set;}
        public ICommand StartCommand { get;private set;}

        public const int DECIMAL_PLACES = 2;
        public async void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {
            exchangeRate = await _exchangeRateService.GetCurrentExchangeRate();
            
            IsBuying = true; //by-default

            BuyingRate = exchangeRate.BuyingRate;
            SellingRate = exchangeRate.SellingRate;
            MainCurrency = exchangeRate.Currency;
            BaseCurrency = exchangeRate.BaseCurrency;

            SendingCurrency = exchangeRate.BaseCurrency;
            RecievingCurrency = exchangeRate.Currency;

            AmountYouPay = 100;
            AmountYouRecieve = Math.Round(IsBuying ? 
                (AmountYouPay / BuyingRate) : 
                (AmountYouPay * SellingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero); 

        }

        private void OnAmountYouPayChanged()
        {
            AmountYouRecieve = Math.Round(IsBuying ?
                (AmountYouPay / BuyingRate) :
                (AmountYouPay * SellingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero);
        }

        private void OnAmountYouRecieveChanged()
        {
            AmountYouPay = Math.Round(IsBuying ?
                (AmountYouRecieve * BuyingRate) :
                (AmountYouRecieve / SellingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero);
        }

        private void OnSwitchExecute(object obj)
        {
            var buyingSelling = !IsBuying;

            SendingCurrency     = buyingSelling ? exchangeRate.BaseCurrency : exchangeRate.Currency;
            RecievingCurrency   = buyingSelling ? exchangeRate.Currency     : exchangeRate.BaseCurrency;

            IsBuying = buyingSelling;

            OnAmountYouPayChanged();
        }

        private async void OnStartExecute(object obj)
        {
            await _navigationService.NavigateTo(Pages.RegisterExchange);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
