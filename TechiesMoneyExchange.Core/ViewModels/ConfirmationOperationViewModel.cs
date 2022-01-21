using System.ComponentModel;
using System.Windows.Input;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.ViewModels
{
    public class ConfirmationOperationViewModel : INotifyPropertyChanged, INavigationAware
    {
        private readonly INavigationService _navigationService;

        public ConfirmationOperationViewModel(INavigationService navigationService)
        {            
            _navigationService = navigationService;

            FinishCommand = new Command(OnFinsh);
            CancelCommand = new Command(OnCancel);
        }        

        public Currency Currency { get; private set; }
        public decimal SendingAmount { get; private set; }
        public string NameOnAccount { get; private set; }
        public string AccountType { get; private set; }
        public string BankName { get; private set; }
        public string AccountNo { get; private set; }

        public ICommand FinishCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
                
        public void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {
            
        }
        private async void OnFinsh(object obj)
        {            
            await _navigationService.NavigateTo(Pages.ExchangeStart,clearHistory: true);
        }
        private async void OnCancel(object obj)
        {
            await _navigationService.NavigateTo(Pages.ExchangeStart, clearHistory: true);
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
