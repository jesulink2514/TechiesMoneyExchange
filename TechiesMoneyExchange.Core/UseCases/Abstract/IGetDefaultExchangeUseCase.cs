namespace TechiesMoneyExchange.Core.UseCases
{
    public interface IGetDefaultExchangeUseCase
    {
        Task Execute(IDefaultExchangeContext context);
    }
}
