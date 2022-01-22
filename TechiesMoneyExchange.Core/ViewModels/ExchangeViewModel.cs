using System.ComponentModel;
using System.Windows.Input;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.UseCases;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.ViewModels
{
    public class ExchangeViewModel : INotifyPropertyChanged, INavigationAware, IDefaultExchangeContext, ICalculateExchangedAmountContext
    {
        private readonly GetLatestExchangeRateUseCase _getLatestExchangeRateUseCase;
        private readonly GetDefaultExchangeUseCase _getDefaultExchangeUseCase;
        private readonly CalculateExchangedAmountUseCase _calculateExchangedAmountUseCase;
        private readonly INavigationService _navigationService;

        public ExchangeViewModel(
            GetLatestExchangeRateUseCase getLatestExchangeRateUseCase,
            GetDefaultExchangeUseCase getDefaultExchangeUseCase,
            CalculateExchangedAmountUseCase calculateExchangedAmountUseCase,
            INavigationService navigationService)
        {
            
            _getLatestExchangeRateUseCase = getLatestExchangeRateUseCase;
            _getDefaultExchangeUseCase = getDefaultExchangeUseCase;
            _calculateExchangedAmountUseCase = calculateExchangedAmountUseCase;
            
            _navigationService = navigationService;
            
            SwitchCommand = new Command(OnSwitchExecute);
            StartCommand = new Command(OnStartExecute);
        }

        public PublishedExchangeRate ExchangeRate { get;private set; }

        private Currency EmptyCurrency = new Currency();
        
        public decimal AmountYouPay { get; set; }
        public decimal AmountYouRecieve { get; set; }
        public Currency SendingCurrency { get; set; }
        public Currency RecievingCurrency { get; set; }

        public decimal BuyingRate => ExchangeRate?.BuyingRate ?? 0;       
        public decimal SellingRate => ExchangeRate?.SellingRate ?? 0;
        public Currency MainCurrency => ExchangeRate?.Currency ?? EmptyCurrency;
        public Currency BaseCurrency => ExchangeRate?.BaseCurrency ?? EmptyCurrency;

        public bool IsBuying { get; set; }

        public bool IsLoading { get; private set; }

        public ICommand SwitchCommand { get;private set;}
        public ICommand StartCommand { get;private set;}

        public const int DECIMAL_PLACES = 2;
        public const decimal PRECISION_TOLERANCE = 0.02m;
        public async void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {
            IsLoading = true;

            ExchangeRate = await _getLatestExchangeRateUseCase.Execute();

            //load-defaults
            await _getDefaultExchangeUseCase.Execute(this);

            IsLoading = false;
        }

        private void OnAmountYouPayChanged()
        {
            _calculateExchangedAmountUseCase.ExecuteForAmountYouRecieve(this);
        }

        private void OnAmountYouRecieveChanged()
        {
            _calculateExchangedAmountUseCase.ExecuteForAmountYouPay(this);
        }

        private void OnSwitchExecute(object obj)
        {
            var buyingSelling = !IsBuying;

            SendingCurrency     = buyingSelling ? ExchangeRate.BaseCurrency : ExchangeRate.Currency;
            RecievingCurrency   = buyingSelling ? ExchangeRate.Currency     : ExchangeRate.BaseCurrency;

            IsBuying = buyingSelling;

            _calculateExchangedAmountUseCase.ExecuteForAmountYouRecieve(this);
        }

        private async void OnStartExecute(object obj)
        {
            var exchangeOperation = new DraftExchangeRequest(ExchangeRate, 
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
