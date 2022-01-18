using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RazorWebApp2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult StudentDetails()
        {
            ViewBag.StudentId = 101;
            ViewBag.StudentName = "jayant";
            ViewBag.Marks = 80;
            ViewBag.NoOfSemister = 6;
            ViewBag.Subject = new List<string>() { "chem", "phy", "math" };
            return View();
        }

    }
}