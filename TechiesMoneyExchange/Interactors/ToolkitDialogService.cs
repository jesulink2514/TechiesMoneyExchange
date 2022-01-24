using CommunityToolkit.Maui.Alerts;
using TechiesMoneyExchange.Core.Infrastructure.Interactors;

namespace TechiesMoneyExchange.Interactors
{
    public class ToolkitDialogService : IDialogService
    {
        public async Task ShowMessage(string message)
        {
            await Snackbar.Make(message).Show();
        }
    }
}
