using TechiesMoneyExchange.Model;

namespace TechiesMoneyExchange.Core.UseCases
{
    public interface IRegisterExchangeOperationContext
    {
        PublishedExchangeRate PublishedExchangeRate { get; }   
        decimal SendingAmount { get;}
        bool IsBuying { get;}
        BankAccount SendingAccount { get;}
        BankAccount RecievingAccount { get;}
    }
}
