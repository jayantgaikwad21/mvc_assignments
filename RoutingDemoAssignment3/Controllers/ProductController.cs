using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoutingDemoAssignment3.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [Route("Products/Details/{id:int}")]
        public ActionResult Details(int id)
        {
            var Products = new[]
            {
                new { productId = 1, productName = "LCD", cost = 50000 },
                new { productId = 2, productName = "AC", cost = 80000 },
                new { productId = 3, productName = "Laptop", cost = 100000 }

            };
            string ProductName = "";
            foreach (var pro in Products)
            {
                if (pro.productId == id)
                    ProductName = pro.productName;
            }
            return Content(ProductName);
        }
        
[Route("Products/GetProductId/{ProductName:string}")]
        public ActionResult GetProductId(string ProductName)
        {
            var Products = new[]
            {
                new { productId = 1, productName = "LCD", cost = 50000 },
                new { productId = 2, productName = "AC", cost = 80000 },
                new { productId = 3, productName = "Laptop", cost = 100000 }

            };
            if (ProductName == null)
                return Content("please pass id");
            else
            {
                int ProductId = 0;
                foreach (var pro in Products)
                {
                    if (pro.productName == ProductName)
                        ProductId = pro.productId;
                }
                return Content(ProductId.ToString());
            }
        }

    }
}
