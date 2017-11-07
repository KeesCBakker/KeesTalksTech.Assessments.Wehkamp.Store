using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Injection()
        {
            return View();
        }

        public IActionResult AsyncResult()
        {
            return View();
        }
    }
}
