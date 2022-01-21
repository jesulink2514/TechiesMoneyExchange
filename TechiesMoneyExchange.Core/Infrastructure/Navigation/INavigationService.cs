
namespace TechiesMoneyExchange.Core.Infrastructure.Navigation
{
    public interface INavigationService
    {
        Task GoBack();
        Task NavigateTo(Pages page, IReadOnlyDictionary<string, object> parameters = null);
    }
    public enum Pages
    {
        ExchangeStart,
        RegisterExchange,
        ConfirmationExchange,

        Operations
    }
}