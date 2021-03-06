﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logging.Contracts;
using Logging.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PartonetMLM.Core.Enums;
using TestLogging.Models;

namespace TestLogging.Controllers
{
    public class HomeController : Controller
    {
        private INlogCodeBase<HomeController> _nlogCodeBase;

        private ISerilog<HomeController> _serilog;
        private string _source = "App.web";
        public HomeController(INlogCodeBase<HomeController> nlogCodeBase, ISerilog<HomeController> serilog)
        {
            _nlogCodeBase = nlogCodeBase;
            _serilog = serilog;
        }

        public IActionResult Index()
        {


            _nlogCodeBase.Warrning(ProjectEnum.Admin, "Warrning Test");
            _nlogCodeBase.Error(ProjectEnum.Admin, "Error Test");
            _nlogCodeBase.Info(ProjectEnum.Admin, "Info Test");




            var info = $"record at {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
            var result = "{source} ValuesController.Get(). This is a test log {info} year {year}";
            _serilog.LogInformation(LogLevels.Error, result, null, new object[] { _source, info, DateTime.Now.Year });
            _serilog.Warrning(ProjectEnum.Admin, "Warrning Test");
            _serilog.Error(ProjectEnum.Admin, "Error Test");
            _serilog.Info(ProjectEnum.Admin, "Info Test");
            return View();
        }
    }
}
