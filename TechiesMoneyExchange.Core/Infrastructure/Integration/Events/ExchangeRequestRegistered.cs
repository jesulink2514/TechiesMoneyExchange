using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.Infrastructure.Integration.Events
{
    public class ExchangeRequestRegistered
    {
        public ExchangeRequest Operation { get; private set; }

        public ExchangeRequestRegistered(ExchangeRequest operation)
        {
            Operation = operation;
        }
    }
}
