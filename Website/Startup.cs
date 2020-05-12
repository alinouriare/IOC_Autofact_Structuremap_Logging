using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BotDetect.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Website.Services;


namespace Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // services.AddPortableObjectLocalization(options => options.ResourcesPath = "Resources");
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            //services.AddMvc(options =>
            //{
            //    // options.Filters.AddService<>(SetViewDataFilter);

            //}).AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder, options => { options.ResourcesPath = "Resources"; })
            //     .AddDataAnnotationsLocalization(options =>
            //     {
            //         options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Base.ShareResource));
            //     });

            services.AddMvc(c => c.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
               .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder, options => { options.ResourcesPath = "Resources"; })
               .AddDataAnnotationsLocalization(options =>
               {
                   options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Base.ShareResource));
               });


            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("lang", typeof(LanguageRouteConstraint));
            });

            services.AddTransient<CustomLocalizer>();

            //services.AddLocalization();
            //services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();





            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddMemoryCache(); // Adds a default in-memory 
                                       // implementation of 
                                       // IDistributedCache
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, ILoggerFactory loggerFactory*/)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            //LocalizationPipeline.ConfigureOptions(options.Value);
            app.UseRequestLocalization(options.Value);

            // app.UseHttpsRedirection();
            // app.UseCookiePolicy();
            //app.UseAuthentication();






            // configures Session middleware
            app.UseSession();

            // configure your application pipeline to use Captcha middleware
            // Important! UseCaptcha(...) must be called after the UseSession() call
            app.UseCaptcha(Configuration);







            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "LocalizedDefault",
                    pattern: "{lang:lang}/{controller=Home}/{action=Index}"
                );

                endpoints.MapControllerRoute(
                     name: "LocalizedDefault",
                     pattern: "{lang:lang}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                   name: "LocalizedDefault",
                   pattern: "{lang:lang}/{area}/{controller=Home}/{action=Index}/{id?}"
               );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{*catchall}",
                    defaults: new { controller = "Home", action = "RedirectToDefaultLanguage"/*, lang = "en"*/ });

            });
        }
    }
}
