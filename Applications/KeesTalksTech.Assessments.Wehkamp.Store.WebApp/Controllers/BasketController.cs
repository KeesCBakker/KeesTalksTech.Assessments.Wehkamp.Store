using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Controllers
{
    public class BasketController : Controller
    {
        private const string BasketIdentifierCookieName = "BasketIdentifier";

        private readonly IStoreSdkClient _client;

        public BasketController(IStoreSdkClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var basket = await EnsureBasketAsync();

            if (basket.Contents.Count == 0)
            {
                return View("Empty");
            }

            return View(basket);
        }

        [HttpPost("RemoveFromBasket/{basketItemId}")]
        public async Task<IActionResult> RemoveFromBasket(string basketItemId)
        {
            var basket = await EnsureBasketAsync();

            //check if the item is present
            var item = basket
                .Contents
                .FirstOrDefault(c => c.Id == basketItemId);

            //only remove the item if present
            if (item != null)
            {
                await _client.Baskets.RemoveBasketItemAsync(basket.Id, basketItemId);
            }

            return RedirectToAction("Index", "Basket");
        }

        [HttpPost("AddToBasket/{productId}/{amount}")]
        public async Task<IActionResult> AddToBasket(string productId, uint amount)
        {
            //check if product exists
            var product = await _client.Products.GetAsync(productId);
            if (product == null)
            {
                return BadRequest();
            }

            var basket = await EnsureBasketAsync();

            //check if the product has been ordered
            var item = basket
                .Contents
                .FirstOrDefault(c => c.Content.Id == product.Id);

            //only create the item if not present
            if (item == null)
            {
                var basketItem = await _client.Baskets.AddToBasketAsync(basket.Id, product.Id, amount);
                basket.Contents.Add(basketItem);
            }

            return RedirectToAction("Index", "Basket");
        }

        [NonAction]
        public async Task<BasketModel> EnsureBasketAsync()
        {
            //check if basket exists
            BasketModel basket = null;
            var basketId = Request.Cookies[BasketIdentifierCookieName];
            if (!String.IsNullOrEmpty(basketId))
            {
                basket = await _client.Baskets.GetAsync(basketId);
            }

            //might have been a reset - recreate the basket
            if (basket == null)
            {
                basket = await _client.Baskets.CreateAsync();
                basketId = basket.Id;

                //write cookie
                Response.Cookies.Append(BasketIdentifierCookieName, basketId, new CookieOptions { HttpOnly = true });
            }

            return basket;
        }
    }
}