﻿using KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IStoreSdkClient _client;

        public SearchController(IStoreSdkClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        
        public async Task<IActionResult> Index(string query = null)
        {
            if (String.IsNullOrEmpty(query))
            {
                return await NothingFound();
            }

            ViewBag.Query = query;

            var result = await _client.Products.SearchAsync(query, 1, 4 * 5);
            var list = result.ToList();

            if(list.Count == 0)
            {
                return await NothingFound();
            }

            return View(list);
        }

        [NonAction]
        public async Task<IActionResult> NothingFound()
        {
            var random = await _client.Products.RandomAsync(1);
            if (random != null)
            {
                ViewBag.Suggestions = random.FirstOrDefault()?.Keywords;
            }


            return View("Nada");
        }
    }
}
