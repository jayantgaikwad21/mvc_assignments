using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproach1.Models;

namespace EFDbFirstApproach1.Controllers
{
    public class ProductController : Controller
    {
       
        // GET: Product
        public ActionResult Index(String search="")
        {
            ComponyDbContext db = new ComponyDbContext();
            List<Product> product = db.Products.Where(Temp=>Temp.ProductName.Contains(search)).ToList();
            return View(product);
        }

        public ActionResult Detail(long id)
        {
            ComponyDbContext db = new ComponyDbContext();
            Product p = db.Products.Where(Temp =>Temp.ProductId == id).FirstOrDefault();
            return View(p);
        }

        public ActionResult Create()
        {
            ComponyDbContext db = new ComponyDbContext();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            ComponyDbContext db = new ComponyDbContext();
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id)
        {
            ComponyDbContext db = new ComponyDbContext();
            Product existingProduct = db.Products.Where(Temp => Temp.ProductId == id).FirstOrDefault();
            return View(existingProduct);
        }

        [HttpPost]
        public ActionResult Edit(Product p)
        {
            ComponyDbContext db = new ComponyDbContext();
            Product existingProduct = db.Products.Where(Temp => Temp.ProductId ==p.ProductId).FirstOrDefault();
            existingProduct.ProductName = p.ProductName;
            existingProduct.Price = p.Price;
            existingProduct.DateOdPurchase = p.DateOdPurchase;
            existingProduct.CategoryId = p.CategoryId;
            existingProduct.BrandId = p.BrandId;
            existingProduct.AvailabilityStatus = p.AvailabilityStatus;
            existingProduct.Active = p.Active;
            db.SaveChanges();
            return RedirectToAction("Index","Product");
        }

        public ActionResult Delete(long id)
        {
            ComponyDbContext db = new ComponyDbContext();
            Product existingProduct = db.Products.Where(Temp => Temp.ProductId == id).FirstOrDefault();
            return View(existingProduct);
        }

        [HttpPost]
        public ActionResult Delete(long id,Product p)
        {
            ComponyDbContext db = new ComponyDbContext();
            Product existingProduct = db.Products.Where(Temp => Temp.ProductId == id).FirstOrDefault();
            db.Products.Remove(existingProduct);
            return View("Index","Product");
        }

    }
}