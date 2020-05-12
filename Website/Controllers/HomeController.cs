using System.Diagnostics;
using BotDetect.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Website.Models;
using Website.Services;


namespace Website.Controllers
{
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class HomeController : BaseController
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
        public IActionResult About()
        {
            return View();
        }
        public IActionResult ChangeCulture(string culture)
        {
            //Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            //using (IUnitOfWork unitOfWork = new UnitOfWork())
            //{
            //    var language = unitOfWork.StoredProcedures.GetAllLanguage(true);
            //    var idLanguage = language == null || language.Count <= 0 ? 0 : language.FirstOrDefault(f => f.CaltureName == culture)?.Id;
            //    if (idLanguage > 0)
            //        Core.Setting.SetLanguage(this.httpContextAccessor, idLanguage.GetValueOrDefault());
            //}
            var url = Request.Headers["Referer"].ToString()
                .Replace("/en", "/" + culture)
                .Replace("/de", "/" + culture)
                .Replace("/fr", "/" + culture)
                .Replace("/ar", "/" + culture)
                .Replace("/fa", "/" + culture);
            return Redirect(url);
        }

        [HttpGet]
        public ActionResult form1()
        {
            var model = new Form1VM();
                return View(model);
        }

        [CaptchaValidationActionFilter("CaptchaCode", "ExampleCaptcha", "کد کپچا را بدرستی وارد نمایید!")]
        [HttpPost]
        public ActionResult form1(Form1VM model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Captcha validation failed, show error message 
                return View(model);
            }
            else
            {
                // TODO: captcha validation succeeded; execute the protected action  

                // Reset the captcha if your app's workflow continues with the same view
                MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                return Content("captcha is Correct");
            }
        }

        

    }

}
