using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproach1.Models;


namespace EFDbFirstApproach1.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            ComponyDbContext db = new ComponyDbContext();
            List<Brand> brand = db.Brands.ToList();
            return View(brand);
        }
    }
}