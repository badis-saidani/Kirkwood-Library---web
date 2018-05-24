﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebPresentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Session_Start(object sender, EventArgs e)
        {
            // you may set a session variable at this point, or not

             HttpContext.Current.Session["test"] = DateTime.Now.ToLongDateString();
           // HttpContext.Current.Session["test"] = User.ToString();
            System.Web.HttpContext.Current.Session["test2"] = "Weird Message";
        }
    }
}