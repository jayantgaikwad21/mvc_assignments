using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelDemo.Models;
namespace ModelDemo.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            List<Product> product = new List<Product>()
            {
                new Product{ProductId=1,ProductName="Laptop",ProductRate=60000},
                 new Product{ProductId=2,ProductName="BMW",ProductRate=90000},
                  new Product{ProductId=3,ProductName="Bike",ProductRate=70000}

            };
            ViewBag.product = product;
            return View();

        }
          public ActionResult Details(int id)
        {
            List<Product> product = new List<Product>()
            {
                new Product{ProductId=1,ProductName="Laptop",ProductRate=60000},
                 new Product{ProductId=2,ProductName="BMW",ProductRate=90000},
                  new Product{ProductId=3,ProductName="Bike",ProductRate=70000}

            };
            Product matchingProd=null;
            foreach(var item in product)
            {
                if (item.ProductId == id)
                {
                    matchingProd = item;
                }
             }
            ViewBag.MatchingProduct= matchingProd;
            return View();
        }

    }
}