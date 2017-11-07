using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.ViewComponents
{
    public class AmazingMessageViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CancellationToken token, string text, int wait)
        {
            await Task.Delay(wait, token);

            token.ThrowIfCancellationRequested(); 

            return View<string>(text);
        }
    }
    
}