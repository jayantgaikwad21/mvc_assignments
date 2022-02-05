using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorBooks.Models;
namespace AuthorBooks.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();
            List<Author> Author = db.Authors.ToList();
;            return View(Author);
        }

        public ActionResult create()
        {
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();


            return View();
        }
        [HttpPost]
        public ActionResult create(Author a)
        {
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();
            db.Authors.Add(a);
            db.SaveChanges();
            //return View();
            return RedirectToAction("Index");
        }

        //public ActionResult Delete(int id)
        //{
        //    AuthorBooksDBEntities db = new AuthorBooksDBEntities();
        //    Author existingProduct = db.Authors.Where(temp => temp.AuthorId == id).FirstOrDefault();
        //    return View(existingProduct);

        //}
        //[HttpPost]
        //public ActionResult Delete(long id, Author a)
        //{
        //    AuthorBooksDBEntities db = new AuthorBooksDBEntities();
        //     Author existingProduct = db.Authors.Where(temp => temp.AuthorId == id).FirstOrDefault();
        //    db.Authors.Remove(existingProduct);
        //    db.SaveChanges();

        //    return RedirectToAction("Index", "Author");

        //}

        public JsonResult Delete(long ID)
        {
            bool result = false;
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();
            Author Au = db.Authors.Where(temp => temp.AuthorId == ID).FirstOrDefault();
            db.Authors.Remove(Au);
            db.SaveChanges();
            result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
    
}