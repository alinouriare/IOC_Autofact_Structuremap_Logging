using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IOCConfig_Structuremap.NewFolder;
using Microsoft.AspNetCore.Mvc;
using WebApp_Test_IOC_Structuremap.Models;

namespace WebApp_Test_IOC_Structuremap.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICalculator calculator;

        public HomeController(ICalculator calculator)
        {
            this.calculator = calculator;
        }

        public IActionResult Index()
        {
            ViewBag.Set = calculator.Add(25, 90);

            return View();
        }

        public IActionResult Privacy()
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
