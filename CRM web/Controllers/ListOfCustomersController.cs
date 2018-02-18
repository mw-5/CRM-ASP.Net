using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM_web.Controllers
{
    [Authorize]
    public class ListOfCustomersController : Controller
    {
        // GET: ListOfCustomers
        public ActionResult ListOfCustomers()
        {
            return View();
        }
    }
}