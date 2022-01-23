using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.Infrastructure.Integration.Events
{
    public class ExchangeRequestRegistered
    {
        public ExchangeRequest Operation { get; }

        public ExchangeRequestRegistered(ExchangeRequest operation)
        {
            Operation = operation;
        }
    }
}
