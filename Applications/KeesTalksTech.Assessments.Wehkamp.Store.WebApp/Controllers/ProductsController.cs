using KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Controllers
{
    [Route("[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IStoreSdkClient _client;

        public ProductsController(IStoreSdkClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        [HttpGet(""), HttpGet("/"), HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var data = await _client.Products.RandomAsync(4 * 5);
            var list = data.ToList();

            if(list.Count == 0)
            {
                return View("InvalidConfiguration");
            }

            return View(list);
        }

        [HttpGet("/p/d/{id}"), HttpGet("Detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var data = await _client.Products.GetAsync(id);
            if (data != null)
            {
                return View(data);
            }

            return RedirectToAction("Invalid");
        }

        [HttpGet("Invalid")]
        public IActionResult Invalid()
        {
            return View();
        }
    }
}
