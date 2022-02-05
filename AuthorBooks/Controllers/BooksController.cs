using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorBooks.Models;
namespace AuthorBooks.Controllers
{
    public class BooksController : Controller
    {
        AuthorBooksDBEntities db = new AuthorBooksDBEntities();
        public ActionResult Index(string BookName="",string AuthorName="", string SortColumn = "BookName", string IconClass = "fa-sort-asc", int PageNo = 1)
        {
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();
            List<Book> Books = db.Books.ToList();
            
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (ViewBag.SortColumn == "BookId")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    Books = Books.OrderBy(temp => temp.BookId).ToList();
                else
                    Books = Books.OrderByDescending(temp => temp.BookId).ToList();
            }
            if (ViewBag.SortColumn == "BookName")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    Books = Books.OrderBy(temp => temp.BookName).ToList();
                else
                    Books = Books.OrderByDescending(temp => temp.BookName).ToList();
            }
            if (ViewBag.SortColumn == "Price")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    Books = Books.OrderBy(temp => temp.BookISBN).ToList();
                else
                    Books = Books.OrderByDescending(temp => temp.BookName).ToList();
            }
            if (ViewBag.SortColumn == "DateOfPurchase")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    Books = Books.OrderBy(temp => temp.DateOfPublish).ToList();
                else
                    Books = Books.OrderByDescending(temp => temp.BookName).ToList();
            }

            if (ViewBag.SortColumn == "AuthorId")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    Books = Books.OrderBy(temp => temp.AuthorId).ToList();
                else
                    Books = Books.OrderByDescending(temp => temp.AuthorId).ToList();
            }
            /*Pging*/
            int NoOfRecordsPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Books.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            Books = Books.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();


            if (BookName != "" && AuthorName != "")
            {
                List<Book> blist = db.Books.Where(temp => temp.BookName.Contains(BookName) && temp.Author.AuthorName.Contains(AuthorName)).ToList();
                int NoOfRecPerPage1 = 7;
                int NoOfPages1 = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(blist.Count) / Convert.ToDouble(NoOfRecPerPage1)));
                int NoOfRecToSkip1 = (PageNo - 1) * NoOfRecPerPage1;
                ViewBag.pageno = PageNo;
                ViewBag.noofpages = NoOfPages1;
                ViewBag.BookName = BookName;
                ViewBag.AuthorName = AuthorName;
                return View(blist);
            }
            else if (BookName != "")
            {
                List<Book> blist = db.Books.Where(temp => temp.BookName.Contains(BookName)).ToList();
                int NoOfRecPerPage1 = 7;
                int NoOfPages1 = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(blist.Count) / Convert.ToDouble(NoOfRecPerPage1)));
                int NoOfRecToSkip1 = (PageNo - 1) * NoOfRecPerPage1;
                ViewBag.pageno = PageNo;
                ViewBag.noofpages = NoOfPages1;
                ViewBag.BookName = BookName;

                return View(blist);
            }
            else if (AuthorName != "")
            {
                List<Book> blist = db.Books.Where(temp =>temp.Author.AuthorName.Contains(AuthorName)).ToList();
                int NoOfRecPerPage1 = 7;
                int NoOfPages1 = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(blist.Count) / Convert.ToDouble(NoOfRecPerPage1)));
                int NoOfRecToSkip1 = (PageNo - 1) * NoOfRecPerPage1;
                ViewBag.pageno = PageNo;
                ViewBag.noofpages = NoOfPages1;

                ViewBag.AuthorName =AuthorName;
                return View(blist);
            }

            return View(Books);


        }

        public ActionResult Details(int id)
        {
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();
            Book b = db.Books.Where(temp => temp.BookId == id).FirstOrDefault();
            return View(b);
        }

        public ActionResult create()
        {
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();
            ViewBag.Authors = db.Authors.ToList();


            return View();
        }
        [HttpPost]

        public ActionResult create(Book b)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var Base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    b.Photo = Base64String;
                }

                db.Books.Add(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Authors = db.Authors.ToList();
                return View();
            }

        }


        //public ActionResult create(Book b)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        AuthorBooksDBEntities db = new AuthorBooksDBEntities();
        //        HttpFileCollectionBase file = Request.Files;
        //        string path = Path.Combine(Server.MapPath("~/images"),Path.GetFileName(file[0].FileName));
        //        file[0].SaveAs(path);
        //        b.Photo = "~/images/" + file[0].FileName;
        //        db.Books.Add(b);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ViewBag.Authors = db.Authors.ToList();
        //        return View();
        //    }


        //}





        public ActionResult edit(int id)
        {
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();
            Book existingBook = db.Books.Where(temp => temp.BookId == id).FirstOrDefault();
            ViewBag.Authors = db.Authors.ToList();
            ViewBag.Books = db.Books.ToList();
            return View(existingBook);
        }
        [HttpPost]

        public ActionResult edit(Book b)
        {
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();
            Book existingBook = db.Books.Where(temp => temp.BookId == b.BookId).FirstOrDefault();
            existingBook.BookName = b.BookName;
            existingBook.BookISBN = b.BookISBN;
            existingBook.DateOfPublish = b.DateOfPublish;
            existingBook.AuthorId = b.AuthorId;
          
            
            db.SaveChanges();
            return RedirectToAction("Index", "Books");
        }

        //public ActionResult edit(Book b)
        //{


        //    Book existingBook = db.Books.Where(temp => temp.BookId == b.BookId).FirstOrDefault();
        //    existingBook.BookName = b.BookName;
        //    existingBook.BookISBN = b.BookISBN;
        //    existingBook.DateOfPublish = b.DateOfPublish;


        //    //HttpFileCollectionBase file = Request.Files;

        //    //string path = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(file[0].FileName));
        //    //file[0].SaveAs(path);
        //    //b.Photo = "~/images/" + file[0].FileName;
        //    existingBook.Photo = b.Photo;
        //    //db.Update(b);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        public JsonResult Delete(long ID)
        {
            bool result = false;
            AuthorBooksDBEntities db = new AuthorBooksDBEntities();
            Book bk = db.Books.Where(temp => temp.BookId == ID).FirstOrDefault();
            db.Books.Remove(bk);
            db.SaveChanges();
            result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}