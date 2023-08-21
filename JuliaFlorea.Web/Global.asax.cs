using Autofac;
using Autofac.Integration.Mvc;
using JuliaFlorea.DataModel;
using JuliaFlorea.Domain;
using JuliaFlorea.Domain.CorrelationService;
using JuliaFlorea.Domain.DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JuliaFlorea.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<DepotCorrelationService>().AsSelf().InstancePerRequest();
            builder.RegisterType<DepotInventoryService>().AsSelf().InstancePerRequest();
            builder.RegisterType<ISiteDistributionService>().AsSelf().InstancePerRequest();
            builder.RegisterType<SiteDistributionService>().As<ISiteDistributionService>().InstancePerRequest();

            builder.RegisterType<SiteInventoryDbHandler>().AsSelf().InstancePerRequest();
            builder.RegisterType<AppDbContext>().AsSelf().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
