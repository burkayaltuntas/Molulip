using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Molulip.Models;
using Newtonsoft.Json;
using ServiceStack.Redis;
using Molulip.Import;
using System.Collections.Generic;
using System.Linq;
using System;


namespace Molulip.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            MealAccess ma = new MealAccess();
            return View(ma.GetTodaysSpecial());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
