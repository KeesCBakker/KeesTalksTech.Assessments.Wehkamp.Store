using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Controllers
{
    [Route("api/[controller]")]
    public class BasketsController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;

        public BasketsController(IBasketService basketService, IProductService productService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        // GET api/baskets
        [HttpGet]
        public IActionResult Get()
        {
            //listing baskets not allowed
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        // PUT api/baskets
        [HttpPut("")]
        public IActionResult Create()
        {
            var basket = _basketService.Create();
            var model = ModelMapper.MapBasket(basket);

            return Ok(model);
        }

        // GET api/baskets/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequestDueToInvalidParameter(nameof(id), id);
            }

            var basket = _basketService.Get(id);
            if(basket == null)
            {
                return NotFound();
            }

            var model = ModelMapper.MapBasket(basket);
            return Ok(model);
        }

        // GET api/baskets/5/items
        [HttpGet("{basketId}/items")]
        public IActionResult GetBasketItems(string basketId)
        {
            var basket = _basketService.Get(basketId);
            if (basket == null)
            {
                return NotFound();
            }

            var model = ModelMapper.MapBasketItems(basket.Contents);
            return Ok(model);
        }

        // GET api/baskets/5/items/2
        [HttpGet("{basketId}/items/{basketItemId}")]
        public IActionResult GetBasketItem(string basketId, string basketItemId)
        {
            var basket = _basketService.Get(basketId);
            if (basket == null)
            {
                //could return not found - but it is a weird request
                //bad request might be better suited for debugging
                return BadRequestDueToInvalidParameter(nameof(basketId), basketId, "Basket not found.");
            }

            var item = basket.Contents.FirstOrDefault(bi => bi.Id == basketItemId);
            if (item == null)
            {
                return NotFound();
            }

            var model = ModelMapper.MapBasketItem(item);
            return Ok(model);
        }

        // DELETE api/baskets/5/items/2
        [HttpDelete("{basketId}/items/{basketItemId}")]
        public IActionResult RemoveBasketItem(string basketId, string basketItemId)
        {
            var basket = _basketService.Get(basketId);
            if (basket == null)
            {
                //could return not found - but it is a weird request
                //bad request might be better suited for debugging
                return BadRequestDueToInvalidParameter(nameof(basketId), basketId, "Basket not found.");
            }

            var item = basket.Contents.FirstOrDefault(bi => bi.Id == basketItemId);
            if (item == null)
            {
                return NotFound();
            }

            basket.Remove(item);

            _basketService.Store(basket);

            return Ok();
        }

        // PUT api/baskets/5/items
        [HttpPut("{basketId}/items")]
        public IActionResult AddBasketItem(string basketId, string productId, uint amount)
        {
            if(amount == 0)
            {
                return BadRequestDueToInvalidParameter(nameof(amount), amount);
            }

            var basket = _basketService.Get(basketId);
            if (basket == null)
            {
                //could return not found - but it is a weird request
                //bad request might be better suited for debugging
                return BadRequestDueToInvalidParameter(nameof(basketId), basketId, "Basket not found.");
            }

            var product = _productService.Get(productId);
            if (product == null)
            {
                return BadRequestDueToInvalidParameter(nameof(productId), productId, "Product not found.");
            }

            var newBasketItem = basket.Add(product, amount);

            _basketService.Store(basket);

            var model = ModelMapper.MapBasketItem(newBasketItem);
            return Ok(model);
        }

        [NonAction]
        public IActionResult BadRequestDueToInvalidParameter(string name, object value, string hint = null)
        {
            var textValue = value == null ? "{null}" : $"'{value}'";

            var message = $"Parameter invalid. Parameter '{name}' cannot have value {textValue}.";

            if (!String.IsNullOrEmpty(hint))
            {
                message += "\n" + hint;
            }

            return BadRequest(message);
        }
    }
}
