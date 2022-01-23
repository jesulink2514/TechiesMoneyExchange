
namespace TechiesMoneyExchange.Core.UseCases
{
    public interface IRegisterExchangeOperationUseCase
    {
        Task Execute(IRegisterExchangeOperationContext context);
    }
}