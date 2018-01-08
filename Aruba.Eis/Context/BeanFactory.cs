using System;
using System.Web;
using log4net;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Advanced;
using System.Collections.Generic;
using Aruba.Eis.Models;
using Aruba.Eis.Services;
using Aruba.Eis.Services.Impl;
using Aruba.Eis.Controllers;
using System.Reflection;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using System.Web.Http;
using Aruba.Eis.EntityFramework;
using Owin;

namespace Aruba.Eis.Context
{
    public class BeanFactory
    {
        /// <summary>
        /// Log manager
        /// </summary>
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// SimpleInjector Container
        /// </summary>
        private static Container _container;

        /// <summary>
        /// Initialize SimpleInjector Container
        /// </summary>
        public static Container Initialize(IAppBuilder app)
        {
            try
            {
                _container = new Container();

                _container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

                // IoC for ASP.NET Identity
                _container.RegisterSingleton<IAppBuilder>(app);
                _container.Register<IUserStore<ApplicationUser>>(
                    () => new UserStore<ApplicationUser>(
                        _container.GetInstance<ApplicationDbContext>()),
                    Lifestyle.Scoped);
                _container.Register<ApplicationUserManager>(Lifestyle.Scoped);
                _container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
                _container.Register<ApplicationDbContext>(
                    () => new ApplicationDbContext(),
                    Lifestyle.Scoped);
                _container.Register<IAuthenticationManager>(() =>
                        AdvancedExtensions.IsVerifying(_container)
                            ? new OwinContext(new Dictionary<string, object>()).Authentication
                            : HttpContext.Current.GetOwinContext().Authentication,
                    Lifestyle.Scoped);

                /*
                _container.Register<IUserStore<ApplicationUser>>(
                    () => new UserStore<ApplicationUser>(),
                        Lifestyle.Scoped);
                _container.Register<ApplicationUserManager>(Lifestyle.Scoped);
                _container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
                _container.Register<IAuthenticationManager>(() =>
                        AdvancedExtensions.IsVerifying(_container)
                            ? new OwinContext(new Dictionary<string, object>()).Authentication
                            : HttpContext.Current.GetOwinContext().Authentication,
                    Lifestyle.Scoped);

                //_container.Register<TeamController>(Lifestyle.Scoped);
                */

                _container.RegisterSingleton<ITeamService, TeamService>();
                _container.RegisterSingleton<IActivityService, ActivityService>();
                _container.RegisterSingleton<IUserService, UserService>();
                _container.RegisterSingleton<IMapperService, MapperService>();
                _container.RegisterSingleton<IScheduleService, ScheduleService>();

                // _container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
                _container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

                _container.Verify();

                DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));

                return _container;
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Get an Object Instance
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetInstance<TService>() where TService : class
        {
            return _container.GetInstance<TService>();
        }
    }
}