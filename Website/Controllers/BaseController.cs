using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class BaseController : Controller
    {
        private string _currentLanguage;

        private string CurrentLanguage
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentLanguage))
                {
                    return _currentLanguage;
                }



                if (string.IsNullOrEmpty(_currentLanguage))
                {
                    var feature = HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
                }

                return _currentLanguage;
            }
        }

        public ActionResult RedirectToDefaultLanguage()
        {
            var lang = CurrentLanguage;
            if (lang == "et")
            {
                lang = "ee";
            }

            return RedirectToAction("Index", new { lang = lang });
        }

    }
}