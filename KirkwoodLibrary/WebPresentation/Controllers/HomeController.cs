using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //System.Web.HttpContext.Current.Session["test"] = "Weird Message";
            System.Web.HttpContext.Current.Session["test2"] = "Weird Message";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}