using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace PartonetMLM.Logging.Test
{
    public class Program
    {


        //Change Main Method For Trace Nlog 
        //public static void Main(string[] args)
        //{



        //    var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //    try
        //    {
        //        logger.Debug("Init main");
        //        CreateWebHostBuilder(args).Build().Run();
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e, "Program stopped because of an exception.");
        //        throw;
        //    }
        //    finally
        //    {
        //        LogManager.Shutdown();
        //    }
        //}

      

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        //Change CreateHostBuilder Method
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().ConfigureLogging(builder => {

                        builder.ClearProviders();
                        builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                    }).UseNLog();
                });

    }
}