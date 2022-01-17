using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string arg)
        {
            if(arg=="sample")
            {
                return File("~/SYNOPSIS_JS.pdf","application/pdf");

            }
            else if(arg=="gotoabout")
            {
                return RedirectToAction("about");
            }
            else if(arg=="login")
            {
                return View("login");
            }
            else
            {
                return Content("you entered " + arg);
            }
        }
         public ActionResult about()
        {
            return Content("about content here");
        }
    }
}