using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication1.Services;
using WebApplication1.DataAccess;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            RegisterDependencies(container);
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void RegisterDependencies(UnityContainer container)
        {
            container.RegisterType<IDataAccessFactory, DataAccessFactory>();
            container.RegisterType<ILoanUnitOfWork, LoanUnitOfWork>();
            container.RegisterType<IMemberUnitOfWork, MemberUnitOfWork>();
            container.RegisterType<IMemberDataAccess, MemberDataAccess>();
            container.RegisterType<IBookLoanService, BookLoanService>();
            container.RegisterType<IMemberService, MemberService>();
        }
    }
}
