using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CRM_web.Controllers
{
    [Authorize]
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult Start()
        {
            return View();
        }
    }
}