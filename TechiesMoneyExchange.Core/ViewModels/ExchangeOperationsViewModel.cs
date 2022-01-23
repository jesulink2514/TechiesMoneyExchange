using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechiesMoneyExchange.Core.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Core.Infrastructure.Integration;
using TechiesMoneyExchange.Core.Infrastructure.Integration.Events;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.ViewModels
{
    public class ExchangeOperationsViewModel : INotifyPropertyChanged, INavigationAware
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IEventAggregator _eventAggregator;

        public ExchangeOperationsViewModel(
            IExchangeRateService exchangeRateService,
            IEventAggregator eventAggregator
            )
        {
            _exchangeRateService = exchangeRateService;
            _eventAggregator = eventAggregator;

            _eventAggregator.RegisterHandler<ExchangeRequestRegistered>(OnExchangeRequestRegistered);
        }

        public ExchangeRequest[] Operations { get; private set; }

        public async void OnNavigatedTo(IReadOnlyDictionary<string, object> navigationParameters)
        {
            Operations = await _exchangeRateService.GetLast7daysOperations();
        }

        private async void OnExchangeRequestRegistered(ExchangeRequestRegistered obj)
        {
            Operations = await _exchangeRateService.GetLast7daysOperations();
        }

        public event PropertyChangedEventHandler PropertyChanged;        
    }
}
