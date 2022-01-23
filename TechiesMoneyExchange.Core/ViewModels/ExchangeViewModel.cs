using System.ComponentModel;
using System.Windows.Input;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Model;
using TechiesMoneyExchange.Core.UseCases;

namespace TechiesMoneyExchange.ViewModels
{
    public class ExchangeViewModel : INotifyPropertyChanged, INavigationAware, 
        IDefaultExchangeContext, 
        IApplyNewExchangedAmountContext
    {
        private readonly IGetDefaultExchangeUseCase _getDefaultExchangeUseCase;
        private readonly IGetLatestExchangeRateUseCase _getLastestExchangeRateUseCase;
        private readonly IApplyNewExchangedAmountUseCase _applyNewExchangedAmountUseCase;
        private readonly INavigationService _navigationService;

        public ExchangeViewModel(
            IGetDefaultExchangeUseCase getDefaultExchangeUseCase,
            IGetLatestExchangeRateUseCase getLastestExchangeRateUseCase,
            IApplyNewExchangedAmountUseCase applyNewExchangedAmountUseCase,
            INavigationService navigationService)
        {
            
            _getDefaultExchangeUseCase = getDefaultExchangeUseCase;
            _getLastestExchangeRateUseCase = getLastestExchangeRateUseCase;
            _applyNewExchangedAmountUseCase = applyNewExchangedAmountUseCase;
            _navigationService = navigationService;

            SwitchCommand = new Command(OnSwitchExecute);
            StartCommand = new Command(OnStartExecute);
        }
        private static Currency EmptyCurrency = new Currency();
        public PublishedExchangeRate ExchangeRate { get;private set; }
        
        public decimal AmountYouPay { get; set; }
        public decimal AmountYouRecieve { get; set; }
        public decimal BuyingRate => ExchangeRate?.BuyingRate ?? 0;    
        public decimal SellingRate => ExchangeRate?.SellingRate ?? 0;
        public Currency MainCurrency => ExchangeRate?.Currency ?? EmptyCurrency;
        public Currency BaseCurrency => ExchangeRate?.BaseCurrency ?? EmptyCurrency;
        
        public bool IsBuying { get; set; }
        public Currency SendingCurrency { get; set; }
        public Currency RecievingCurrency { get; set;}
        
        public bool IsLoading { get; private set; }

        public ICommand SwitchCommand { get;private set;}
        public ICommand StartCommand { get;private set;}
        
        public async void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {
            IsLoading = true;

            ExchangeRate = await _getLastestExchangeRateUseCase.Execute();
            await _getDefaultExchangeUseCase.Execute(this);

            IsLoading = false;
        }

        private void OnAmountYouPayChanged()
        {
            _applyNewExchangedAmountUseCase.ExecuteForAmountYouRecieve(this);
        }

        private void OnAmountYouRecieveChanged()
        {
            _applyNewExchangedAmountUseCase.ExecuteForAmountYouPay(this);
        }

        private void OnSwitchExecute(object obj)
        {
            var buyingSelling = !IsBuying;

            SendingCurrency     = buyingSelling ? ExchangeRate.BaseCurrency : ExchangeRate.Currency;
            RecievingCurrency   = buyingSelling ? ExchangeRate.Currency     : ExchangeRate.BaseCurrency;

            IsBuying = buyingSelling;

            _applyNewExchangedAmountUseCase.ExecuteForAmountYouRecieve(this);
        }

        private async void OnStartExecute(object obj)
        {
            var opType = IsBuying ? ExchangeOperation.Buy : ExchangeOperation.Sell;
            var exchangeOperation = ExchangeRate.CreateExchangeRequestFor(opType, AmountYouPay);

            var parameters = new Dictionary<string, object>
            {
                { "operation" , exchangeOperation }
            };

            await _navigationService.NavigateTo(Pages.RegisterExchange, parameters);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
