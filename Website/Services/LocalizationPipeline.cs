using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Services
{
    public class LocalizationPipeline
    {
        public void Configure(IApplicationBuilder app)
        {
            var options = new RequestLocalizationOptions();
            ConfigureOptions(options);

            app.UseRequestLocalization(options);
        }

        public static void ConfigureOptions(RequestLocalizationOptions options)
        {
            var supportedCultures = new List<CultureInfo>
                {

                          // new CultureInfo("en-US"),
                              new CultureInfo("en"),
                            //  new CultureInfo("fr-FR"),
                            //  new CultureInfo("fr"),
                            //  new CultureInfo("cs"),
                              new CultureInfo("fa")
                              //new CultureInfo("et"),
                              //new CultureInfo("en"),
                              //new CultureInfo("ru")
                };


             options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders = new[] {
                new RouteDataRequestCultureProvider()
                {
                    Options = options,
                    RouteDataStringKey = "lang",
                    UIRouteDataStringKey = "lang"
                }
            };

        }
    }
}
