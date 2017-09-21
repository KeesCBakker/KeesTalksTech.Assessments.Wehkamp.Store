using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        IProductService _service;
        
        public ProductsController(IProductService productService)
        {
            _service = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        // GET api/products
        [HttpGet("")]
        [HttpGet("{page=1}/{pageSize=10}")]
        [HttpGet("{page=1}/{pageSize=10}/{search}")]
        public IActionResult Get(uint page = 1, uint pageSize = 10, string search = "")
        {
            if(page == 0)
            {
                return BadRequestDueToInvalidParameter(nameof(page), page);
            }

            if(pageSize == 0)
            {
                return BadRequestDueToInvalidParameter(nameof(pageSize), pageSize);
            }

            var list = _service.Search(search, page, pageSize);
            var result = list.Select(p => ModelMapper.MapProduct(p));

            return Ok(result);
        }

        // GET api/products/random
        [HttpGet("random")]
        [HttpGet("random/{count}")]
        public IActionResult Random(uint count = 25)
        {
            if (count == 0)
            {
                return BadRequestDueToInvalidParameter(nameof(count), count);
            }

            var list = _service.Random(count);
            var result = list.Select(p => ModelMapper.MapProduct(p));

            return Ok(result);
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequestDueToInvalidParameter(nameof(id), id);
            }

            var product = _service.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = ModelMapper.MapProduct(product);
            return Ok(model);
        }

        [NonAction]
        public IActionResult BadRequestDueToInvalidParameter(string name, object value)
        {
            var textValue = value == null ? "{null}" : $"'{value}'";

            return BadRequest($"Parameter invalid. Parameter '{name}' cannot have value {textValue}.");
        }
    }
}
