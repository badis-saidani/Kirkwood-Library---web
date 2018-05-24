using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPresentation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace WebPresentation.Controllers
{
    public class AdminController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        [Authorize(Roles = "Administrator")] // all the methods are automatcally markedup with this
        public ActionResult Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var users = userManager.Users.OrderBy(u => u.LastName).ToList();

            //var userRoles = userManager.GetRoles();
            ViewBag.userManager = userManager;

            return View(users);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);
            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var roles = userManager.GetRoles(id).ToList();
            ViewBag.Roles = roles;
            ViewBag.Id = id;
            ViewBag.FirstName = applicationUser.FirstName;
            ViewBag.LastName = applicationUser.LastName;

            List<string> allRoles = new List<string>();
            allRoles.Add("Student");

            var unusedRoles = allRoles.Except(roles).OrderBy(s => s).ToList();
            ViewBag.UnusedRoles = unusedRoles;

            return View(applicationUser);
        }


        public ActionResult Remove(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            char[] seps = { ',' };
            string[] parts = id.Split(seps);

            string role = parts[0];
            string userId = parts[1];

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(userId);
            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            userManager.RemoveFromRole(userId, role);
            return RedirectToAction("Edit", "Admin", new { id = userId });
            // return View(applicationUser);
        }


        public ActionResult Add(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            char[] seps = { ',' };
            string[] parts = id.Split(seps);

            string role = parts[0];
            string userId = parts[1];

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(userId);
            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            userManager.AddToRole(userId, role);
            return RedirectToAction("Edit", "Admin", new { id = userId });
            // return View(applicationUser);
        }

        // GET: Admin/Delete/5

        public ActionResult Delete(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            ApplicationUser applicationUser = userManager.FindById(id);
            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            ApplicationUser applicationUser = userManager.FindById(id);
            userManager.Delete(applicationUser);

            return RedirectToAction("Index");
        }

        //// GET: User/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicationUser);
        //}

        //// GET: User/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: User/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ApplicationUsers.Add(applicationUser);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(applicationUser);
        //}

        //// GET: User/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicationUser);
        //}

        //// POST: User/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(applicationUser).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(applicationUser);
        //}

        //// GET: User/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicationUser);
        //}

        //// POST: User/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
        //    db.ApplicationUsers.Remove(applicationUser);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
