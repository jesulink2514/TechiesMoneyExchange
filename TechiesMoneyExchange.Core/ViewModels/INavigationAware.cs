using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechiesMoneyExchange.Core.ViewModels
{
    public interface INavigationAware
    {
        void OnNavigatedTo(IReadOnlyDictionary<string,object> navigationParameters);
    }
}
