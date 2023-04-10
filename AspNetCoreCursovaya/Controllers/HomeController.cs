using AspNetCoreCursovaya.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCursovaya.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult aboutUs()
        {
            return View();
        }

        public IActionResult advertisement()
        {
            return View();
        }

        public IActionResult news()
        {
            return View();
        }

        public IActionResult ourActivities()
        {
            return View();
        }

        public IActionResult pageActivities()
        {
            return View();
        }

        public IActionResult partners()
        {
            return View();
        }

        public IActionResult reports()
        {
            return View();
        }

        public IActionResult writeUs()
        {
            return View();
        }

        public IActionResult calendar_events()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
