﻿using eUseControl.App_Start;
using eUseControl.BusinessLogic.DBIntializer;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace eUseControl
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
           // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
           BundleConfig.RegisterBundles(BundleTable.Bundles);

          }
    }
}