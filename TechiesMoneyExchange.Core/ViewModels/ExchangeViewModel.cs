using System.ComponentModel;
using System.Windows.Input;
using TechiesMoneyExchange.Core.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.ViewModels;
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

        public bool IsLoading { get; private set; }

        public ICommand SwitchCommand { get;private set;}
        public ICommand StartCommand { get;private set;}

        public const int DECIMAL_PLACES = 2;
        public const decimal PRECISION_TOLERANCE = 0.02m;
        public async void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {
            IsLoading = true;

            exchangeRate = await _exchangeRateService.GetCurrentExchangeRate();
            
            IsLoading = false;

            
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
            var oldValue = AmountYouRecieve;

            var newValue = Math.Round(IsBuying ?
                (AmountYouPay / BuyingRate) :
                (AmountYouPay * SellingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero);

            if(Math.Abs(oldValue  - newValue) > PRECISION_TOLERANCE)
            {
                AmountYouRecieve = newValue;
            }
        }

        private void OnAmountYouRecieveChanged()
        {
            var oldValue = AmountYouPay;

            var newValue = Math.Round(IsBuying ?
                (AmountYouRecieve * BuyingRate) :
                (AmountYouRecieve / SellingRate), DECIMAL_PLACES, MidpointRounding.AwayFromZero);

            if(Math.Abs(oldValue - newValue) > PRECISION_TOLERANCE)
            {
                AmountYouPay = newValue;
            }
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
            var exchangeOperation = new DraftExchangeRequest(exchangeRate, 
                AmountYouPay,
                AmountYouRecieve,
                IsBuying? ExchangeOperation.Buy : ExchangeOperation.Sell);

            var parameters = new Dictionary<string, object>
            {
                { "operation" , exchangeOperation }
            };

            await _navigationService.NavigateTo(Pages.RegisterExchange, parameters);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
