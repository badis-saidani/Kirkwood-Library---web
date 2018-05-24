using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataTransferObjects;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebPresentation.Models;

namespace WebPresentation.Controllers
{
    public class BookController : Controller
    {
        private BookManager _bkMgr = new BookManager();
        private LibraryManager _lbMgr = new LibraryManager();

        

        // GET: Book
        public ActionResult Index()
        {
            return View(_bkMgr.RetrieveBookList());
        }

        // GET: Book
        public ActionResult FiltredBooks(string txtGo)
        {
            return View(_bkMgr.RetrieveBookListByWord(txtGo));
            //return View(_bkMgr.RetrieveBookList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            Book bk = (_bkMgr.RetrieveBookList().Find(e => e.BookID == id));
            //BookDetailImproved eqi = _bkMgr.RetrieveImprovedBookDetail(eq);
            return View(bk);
            //return View();
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var libraries = _lbMgr.RetrieveLibraryList();
            var status = _bkMgr.RetrieveStatusList();
            var categories = _bkMgr.RetrieveCategoryList();
            var authors = _bkMgr.RetrieveAuthorList(); //(List<Author>)_bkMgr

            ViewBag.Libraries = libraries;
            ViewBag.Status = status;
            ViewBag.Categories = categories;
            ViewBag.Authors = authors;
            // additional code for dropdowns, etc.
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book book)
        {

            if (ModelState.IsValid)
            {
                _bkMgr.SaveNewBook(book);
                //_bkMgr.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            Book book = _bkMgr.RetrieveBookByID(id);

            var libraries = _lbMgr.RetrieveLibraryList();
            var status = _bkMgr.RetrieveStatusList();
            var categories = _bkMgr.RetrieveCategoryList();
            var authors = _bkMgr.RetrieveAuthorList(); //(List<Author>)_bkMgr

            ViewBag.Libraries = libraries;
            ViewBag.Status = status;
            ViewBag.Categories = categories;
            ViewBag.Authors = authors;


            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
            //return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                Book oldBook = _bkMgr.RetrieveBookByID(book.BookID);
                _bkMgr.EditBook(book, oldBook, book.StatusID);
                return RedirectToAction("Index");
            }
            return View(book);

           
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            Book book = _bkMgr.RetrieveBookByID(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _bkMgr.RetrieveBookByID(id);
            //Book bk = book;
            bool res = _bkMgr.DeactivateBook(book);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Book/Delete/5
        public ActionResult Hold(int id)
        {
            if (User.IsInRole("Student"))
            {
                Book book = _bkMgr.RetrieveBookByID(id);
            
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Hold")]
        //[ValidateAntiForgeryToken]
        public ActionResult HoldConfirmed(int id)
        {
            //var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //ApplicationUser applicationUser = userManager.FindById(id);
            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            //if (applicationUser == null)
            //{
            //    return HttpNotFound();
            //}
            Book book = _bkMgr.RetrieveBookByID(id);
            var userName = User.Identity.GetUserName();
            
            //Book bk = book;
            bool res = _bkMgr.HoldBook(book, userName);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
