using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDBFirstDemoExample.Models;
using EFDBFirstDemoExample.filter;

namespace EFDBFirstDemoExample.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        //public ActionResult Index(string search = "")
        //{
        //    EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();

        //    // List<Product>products=db.Products.Where(temp=>temp.CategoryId==1 && temp.Price>=30000).ToList();
        //    /*List<Product> products = db.Products.Where(temp =>temp.ProductName.Contains(search)).ToList();
        //    ViewBag.search = search;
        //    return View(products);*/
        //    List<Product> products = db.Products.Where(temp => temp.ProductName.Contains(search)).ToList();
        //    ViewBag.search = search;
        //    return View(products);

        //}
       
        public ActionResult Index(string search = "",string SortColumn="ProductName",string IconClass="fa-sort-asc",int PageNo=1)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();

            // List<Product>products=db.Products.Where(temp=>temp.CategoryId==1 && temp.Price>=30000).ToList();
            /*List<Product> products = db.Products.Where(temp =>temp.ProductName.Contains(search)).ToList();
            ViewBag.search = search;
            return View(products);*/
            List<Product> products = db.Products.Where(temp => temp.ProductName.Contains(search)).ToList();
            ViewBag.search = search;
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if(ViewBag.SortColumn=="ProductId")
            {
                if(ViewBag.IconClass=="fa-sort-asc")
                    products=products.OrderBy(temp => temp.ProductId).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductId).ToList();
            }
            if (ViewBag.SortColumn == "ProductName")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.ProductName).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductName).ToList();
            }
            if (ViewBag.SortColumn == "Price")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.Price).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductName).ToList();
            }
            if (ViewBag.SortColumn == "DateOfPurchase")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.DateOfPurchase).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductName).ToList();
            }
            if (ViewBag.SortColumn == "AvailabilityStatus")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.AvailabilityStatus).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductName).ToList();
            }
            if (ViewBag.SortColumn == "CategoryId")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.CategoryId).ToList();
                else
                    products = products.OrderByDescending(temp => temp.CategoryId).ToList();
            }
            if (ViewBag.SortColumn == "BrandId")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.BrandId).ToList();
                else
                    products = products.OrderByDescending(temp => temp.BrandId).ToList();
            }
            /*Pging*/
            int NoOfRecordsPerPage=5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) /Convert.ToDouble(NoOfRecordsPerPage)));
                int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            products=products.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();



            return View(products);


        }

        public ActionResult Details(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product p = db.Products.Where(temp => temp.ProductId == id).FirstOrDefault();
            return View(p);
        }
        //[MyAuthanticationFilter]
        public ActionResult create()
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            ViewBag.categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult create([Bind(Include ="ProductId,ProductName,Price,DataOfPurchase,AvailabilityStatus,CategoryId,BrabdId,Active,Photo")]Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var Base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    p.Photo = Base64String;

                }


                db.Products.Add(p);
                db.SaveChanges();
                //return View();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }


        }
        public ActionResult edit(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductId == id).FirstOrDefault();
            ViewBag.categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View(existingProduct);
        }
        [HttpPost]
        public ActionResult edit(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductId == p.ProductId).FirstOrDefault();
            existingProduct.ProductName = p.ProductName;
            existingProduct.Price = p.Price;
            existingProduct.DateOfPurchase = p.DateOfPurchase;
            existingProduct.CategoryId = p.CategoryId;
            existingProduct.BrandId = p.BrandId;
            existingProduct.AvailabilityStatus = p.AvailabilityStatus;
            existingProduct.Active = p.Active;
            db.SaveChanges();

            return RedirectToAction("Index","Products");
        }
        public ActionResult delete(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductId ==id).FirstOrDefault();
            return View(existingProduct);

        }
        [HttpPost]
        public ActionResult delete(long id,Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductId == id).FirstOrDefault();
            db.Products.Remove(existingProduct);
            db.SaveChanges();
           
            return RedirectToAction("Index", "Products");

        }

    }
}