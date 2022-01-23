using System.ComponentModel;
using System.Windows.Input;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.UseCases;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.ViewModels
{
    public class RegisterOperationViewModel : INotifyPropertyChanged, INavigationAware, IRegisterExchangeOperationContext
    {
        private static Currency EmptyCurrency = new Currency();
        
        
        public decimal SendingAmount => ExchangeOperation?.SendingAmount ?? 0;
        
        
        public decimal RecievingAmount => ExchangeOperation?.RecievingAmount ?? 0;
        
        
        public Currency SendingCurrency => ExchangeOperation == null? EmptyCurrency :
            (ExchangeOperation?.OperationType == Model.ExchangeOperation.Buy ? ExchangeOperation.ExchangeRate.BaseCurrency : ExchangeOperation.ExchangeRate.Currency);
        
        
        public Currency RecievingCurrency => ExchangeOperation == null ? EmptyCurrency :
            (ExchangeOperation?.OperationType == Model.ExchangeOperation.Buy ? ExchangeOperation.ExchangeRate.Currency : ExchangeOperation.ExchangeRate.BaseCurrency);
        
        
        public Currency BaseCurrency => ExchangeOperation == null ? EmptyCurrency :
            (ExchangeOperation?.ExchangeRate?.BaseCurrency ?? EmptyCurrency);
        
        
        public decimal ExchangeRate => ExchangeOperation == null ? 0:
            (IsBuying ? ExchangeOperation.ExchangeRate.BuyingRate : ExchangeOperation.ExchangeRate.SellingRate);
        
        
        public bool IsBuying => ExchangeOperation == null ? false :
            (ExchangeOperation?.OperationType == Model.ExchangeOperation.Buy);
        
        public PublishedExchangeRate PublishedExchangeRate { get; private set; }
        public BankAccount SendingAccount { get; set; }
        public BankAccount RecievingAccount { get; set; }

        public ICommand ChangeSendingAccountCommand { get; private set; }
        public ICommand ChangeRecievingAccountCommand { get; private set; }

        public ICommand RegisterCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }
        public bool IsLoading { get; private set; }

        public DraftExchangeRequest ExchangeOperation { get;private set; }

        private readonly INavigationService _navigationService;
        private readonly IRegisterExchangeOperationUseCase _registerExchangeOperationUseCase;
        private readonly IBankAccountService _bankAccountService;


        public RegisterOperationViewModel(
            IRegisterExchangeOperationUseCase registerExchangeOperationUseCase,
            INavigationService navigationService,
            IBankAccountService bankAccountService)
        {
            _navigationService = navigationService;
            _registerExchangeOperationUseCase = registerExchangeOperationUseCase;
            _bankAccountService = bankAccountService;

            GoBackCommand = new Command(OnGoBack);
            RegisterCommand = new Command(OnRegisterExecute);
        }
        public async void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {
            IsLoading = true;

            if (navigationParameters.ContainsKey("operation") &&
                navigationParameters["operation"] is DraftExchangeRequest operation)
            {
                ExchangeOperation = operation;                                
                PublishedExchangeRate = ExchangeOperation.ExchangeRate;

                RecievingAccount = await _bankAccountService.GetDefaultBankAccountFor(RecievingCurrency);
                SendingAccount = await _bankAccountService.GetDefaultBankAccountFor(SendingCurrency);
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

            await _registerExchangeOperationUseCase.Execute(this);

            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
