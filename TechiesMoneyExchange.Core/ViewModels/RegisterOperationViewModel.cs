using System.ComponentModel;
using System.Windows.Input;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.ViewModels
{
    public class RegisterOperationViewModel : INotifyPropertyChanged, INavigationAware
    {
        public decimal SendingAmount { get; private set; }
        public decimal RecievingAmount { get; private set; }
        public Currency SendingCurrency { get; private set; }
        public Currency RecievingCurrency { get; private set; }
        public Currency BaseCurrency { get; private set; }
        public decimal ExchangeRate { get; set; }
        public bool IsBuying { get; set; }

        public BankAccount SendingAccount { get; set; }
        public BankAccount RecievingAccount { get; set; }

        public ICommand ChangeSendingAccountCommand { get;private set;}
        public ICommand ChangeRecievingAccountCommand { get; private set; }

        public ICommand RegisterCommand { get;private set; }
        public ICommand GoBackCommand { get; private set; }

        private DraftExchangeRequest exchangeOperation;

        private readonly INavigationService _navigationService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IExchangeRateService _exchangeRateService;

        public RegisterOperationViewModel(
            INavigationService navigationService,
            IBankAccountService bankAccountService,
            IExchangeRateService exchangeRateService)
        {
            _navigationService = navigationService;
            _bankAccountService = bankAccountService;
            _exchangeRateService = exchangeRateService;

            GoBackCommand = new Command(OnGoBack);
            RegisterCommand = new Command(OnRegisterExecute);
        }
        public bool IsLoading { get; private set; }
        public async void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {            
            IsLoading = true;
            
            if (navigationParameters.ContainsKey("operation") && 
                navigationParameters["operation"] is DraftExchangeRequest operation)
            {
                exchangeOperation = operation;

                SendingAmount = exchangeOperation.SendingAmount;
                RecievingAmount =  exchangeOperation.RecievingAmount;
                BaseCurrency = exchangeOperation.ExchangeRate.BaseCurrency;
                SendingCurrency = exchangeOperation.OperationType == ExchangeOperation.Buy ?
                    exchangeOperation.ExchangeRate.BaseCurrency: exchangeOperation.ExchangeRate.Currency;
                RecievingCurrency = exchangeOperation.OperationType == ExchangeOperation.Buy ?
                    exchangeOperation.ExchangeRate.Currency : exchangeOperation.ExchangeRate.BaseCurrency;
                
                IsBuying = exchangeOperation.OperationType == ExchangeOperation.Buy;

                ExchangeRate = IsBuying ? 
                    exchangeOperation.ExchangeRate.BuyingRate: 
                    exchangeOperation.ExchangeRate.SellingRate;

                
                RecievingAccount = await _bankAccountService.GetDefaultBankAccountFor(RecievingCurrency);
                SendingAccount   = await _bankAccountService.GetDefaultBankAccountFor(SendingCurrency);
            }

            IsLoading = false;
        }

        private async void OnGoBack(object obj)
        {
            await _navigationService.GoBack();
        }
        private async void OnRegisterExecute(object obj)
        {
            IsLoading = true;

            var exchangeRequest = new ExchangeOperationRequest(
                exchangeOperation.ExchangeRate,
                SendingAmount,
                RecievingAmount,
                exchangeOperation.OperationType,
                SendingAccount,
                RecievingAccount);

           var result = await _exchangeRateService.RegisterOperation(exchangeRequest);

            IsLoading = false;

            await _navigationService.NavigateTo(Pages.ConfirmationExchange, new Dictionary<string, object>
            {
                { "operation", result }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
