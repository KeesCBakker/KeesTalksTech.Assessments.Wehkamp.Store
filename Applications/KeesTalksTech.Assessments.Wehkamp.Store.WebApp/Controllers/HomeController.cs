using Microsoft.AspNetCore.Mvc;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
