using System.ComponentModel;
using System.Windows.Input;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.ViewModels
{
    public class ConfirmationOperationViewModel : INotifyPropertyChanged, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IExchangeRateService _exchangeRateService;

        public ConfirmationOperationViewModel(INavigationService navigationService,
            IExchangeRateService exchangeRateService)
        {            
            _navigationService = navigationService;
            _exchangeRateService = exchangeRateService;

            FinishCommand = new Command(OnFinsh);
            CancelCommand = new Command(OnCancel);
        }
        public decimal SendingAmount { get; private set; }
        public Currency SendingCurrency { get; private set; }
        public int OperationNo { get; set; }

        public Currency BankAccountCurrency { get; private set; }        
        public string NameOnAccount { get; private set; }
        public BankAccountType AccountType { get; private set; }
        public string BankName { get; private set; }
        public string AccountNo { get; private set; }

        public ICommand FinishCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public bool IsLoading { get; private set; }

        public async void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {
            IsLoading = true;
            if(navigationParameters.ContainsKey("operation") && navigationParameters["operation"] is ExchangeRequest operation)
            {
                SendingAmount = operation.SendingAmount;
                SendingCurrency = operation.OperationType == ExchangeOperation.Buy ? operation.ExchangeRate.Currency : operation.ExchangeRate.BaseCurrency;
                OperationNo = operation.OperationNo;
            }
            
            var account =  await _exchangeRateService.GetExchangeBankAccountFor(SendingCurrency);

            BankAccountCurrency = SendingCurrency;
            NameOnAccount = account.NameOnAccount;
            AccountType = account.AccountType;
            BankName = account.Bank.Name;
            AccountNo = account.AccountNo;

            IsLoading = false;
        }
        private async void OnFinsh(object obj)
        {            
            await _navigationService.NavigateTo(Pages.ExchangeStart,clearHistory: true);
        }
        private async void OnCancel(object obj)
        {
            IsLoading = true;

            await _navigationService.NavigateTo(Pages.ExchangeStart, clearHistory: true);

            IsLoading = true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
