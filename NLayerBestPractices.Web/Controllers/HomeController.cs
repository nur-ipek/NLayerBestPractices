using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLayerBestPractices.Web.DTOs;
using NLayerBestPractices.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractices.Web.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorDto errorDto)
        {
            return View(errorDto);
        }
    }
}
