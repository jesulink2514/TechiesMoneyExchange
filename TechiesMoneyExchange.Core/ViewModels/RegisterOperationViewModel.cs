using System.ComponentModel;
using System.Windows.Input;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.ViewModels;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.ViewModels
{
    public class RegisterOperationViewModel : INotifyPropertyChanged, INavigationAware
    {
        public decimal SendingAmount { get; private set; }
        public decimal RecievingAmount { get; private set; }
        public Currency BaseCurrency { get; private set; }
        public Currency MainCurrency { get; private set; }
        public bool IsBuying { get; set; }

        public BankAccount SendingAccount { get; set; }
        public BankAccount RecievingAccount { get; set; }

        public ICommand ChangeSendingAccountCommand { get;private set;}
        public ICommand ChangeRecievingAccountCommand { get; private set; }

        public ICommand RegisterCommand { get;private set; }
        public ICommand GoBackCommand { get; private set; }

        private PublishedExchangeRate exchangeRate;

        private readonly INavigationService _navigationService;

        public RegisterOperationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBackCommand = new Command(OnGoBack);
            RegisterCommand = new Command(OnRegisterExecute);
        }
        
        public void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {
            
        }

        private async void OnGoBack(object obj)
        {
            await _navigationService.GoBack();
        }
        private async void OnRegisterExecute(object obj)
        {
            await _navigationService.NavigateTo(Pages.ConfirmationExchange);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
