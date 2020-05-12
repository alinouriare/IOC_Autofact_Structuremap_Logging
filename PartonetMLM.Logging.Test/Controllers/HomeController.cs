using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PartonetMLM.Logging.Contracts;
using PartonetMLM.Logging.Enums;
using PartonetMLM.Logging.Test.Models;

namespace PartonetMLM.Logging.Test.Controllers
{
    public class HomeController : Controller
    {
        private IPlog<HomeController> _plog;

      
        public HomeController(IPlog<HomeController> plog)
        {
            _plog= plog;
           
        }

        public IActionResult Index()
        {


            _plog.Warrning(ProjectEnum.Admin, "Warrning Test");
            _plog.Error(ProjectEnum.Admin, "Error Test");
            _plog.Info(ProjectEnum.Admin, "Info Test");

            
         
            return View();
        }
    }
}
