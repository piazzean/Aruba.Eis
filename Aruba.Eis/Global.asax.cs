using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.DynamicData;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using Aruba.Eis.Context;
using Aruba.Eis.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Lifestyles;

namespace Aruba.Eis
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Log4Net Configuration
            Console.WriteLine("Configuring Aruba.Eis...");
            log4net.Config.XmlConfigurator.Configure();
            
            // Area Registration
            AreaRegistration.RegisterAllAreas();
            
            // Configure Web API
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            // Configure Web
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
