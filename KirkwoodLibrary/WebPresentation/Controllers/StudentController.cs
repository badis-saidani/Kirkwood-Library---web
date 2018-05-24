using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataTransferObjects;

namespace WebPresentation.Controllers
{
    public class StudentController : Controller
    {
        private StudentManager _stMgr = new StudentManager();
        // GET: Student
        public ActionResult Index()
        {
            return View(_stMgr.RetrieveStudentList());
        }

        // GET: Student/Details/5
        public ActionResult Details(string id)
        {
            Student st = (_stMgr.RetrieveStudentList().Find(e => e.StudentID == id));
            //BookDetailImproved eqi = _bkMgr.RetrieveImprovedBookDetail(eq);
            return View(st);
            //return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(string id)
        {
            Student student = (_stMgr.RetrieveStudentList().Find(e => e.StudentID == id));
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = (_stMgr.RetrieveStudentList().Find(e => e.StudentID == id));
            //Book bk = book;
            bool res = _stMgr.DeactivateStudent(student);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
