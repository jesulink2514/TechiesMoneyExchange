
namespace TechiesMoneyExchange.Core.Infrastructure.Navigation
{
    public interface INavigationService
    {
        Task GoBack();
        Page ResolvePage(Pages page);
        Task NavigateTo(Pages page, IReadOnlyDictionary<string, object> parameters = null, bool clearHistory = false);
    }
    public enum Pages
    {
        ExchangeStart,
        RegisterExchange,
        ConfirmationExchange,

        Operations
    }
}