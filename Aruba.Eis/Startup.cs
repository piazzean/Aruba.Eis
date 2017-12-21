using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Aruba.Eis.Context;
using Aruba.Eis.Controllers;
using Aruba.Eis.EntityFramework;
using Aruba.Eis.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using Aruba.Eis.Services;

[assembly: OwinStartup(typeof(Aruba.Eis.Startup))]

namespace Aruba.Eis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Dependency Config
            BeanFactory.Initialize(app);

            // Run Mapper Initialization
            var mapperService = BeanFactory.GetInstance<IMapperService>();
            mapperService.Initialize();

            ConfigureAuth(app);
        }
    }
}
