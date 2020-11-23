using Benday.VsliveVirtual.WebUi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Benday.VsliveVirtual.WebUi.Controllers
{
    public class SecuritySummaryController : Controller
    {
        public SecuritySummaryController()
        {

        }

        public IActionResult Index()
        {
            var model = new SecuritySummaryViewModel();

            var principal = this.User;

            var identity = User.Identity;

            var claimsIdentityInstance =
                identity as ClaimsIdentity;

            if (claimsIdentityInstance == null)
            {
                model.Claims = new List<Claim>();
            }
            else
            {
                model.Claims =
                    claimsIdentityInstance.Claims.ToList();
            }

            model.Headers = this.Request.Headers;

            model.Cookies = this.Request.Cookies;

            return View(model);
        }
    }
}
