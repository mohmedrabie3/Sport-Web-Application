using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsIdentity.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult About()
        {
            return View();
        }
    }
}