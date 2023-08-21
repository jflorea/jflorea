using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JuliaFlorea.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DepotCorrelationData",
                url: "DepotCorrelationData/{action}/{id}",
                defaults: new { controller = "DepotCorrelationData", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Country",
               url: "Country/{action}/{id}",
               defaults: new { controller = "Country", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Depot",
              url: "Depot/{action}/{id}",
              defaults: new { controller = "Depot", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DrugUnit",
              url: "DrugUnit/{action}/{id}",
              defaults: new { controller = "DrugUnit", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DrugType",
              url: "DrugType/{action}/{id}",
              defaults: new { controller = "DrugType", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "Site",
              url: "Site/{action}/{id}",
              defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
          );
            
            routes.MapRoute(
                name: "DrugUnitsRequested",
                url: "Site/{action}/{id}",
                defaults: new { controller = "Site", action = "RequestDrugs", id = UrlParameter.Optional }
                );
            routes.MapRoute(
               name: "Weight",
               url: "Weight/{action}/{id}",
               defaults: new { controller = "Weight", action = "Index", id = UrlParameter.Optional }
               );
            routes.MapRoute(
              name: "ToGroupedDrugUnits",
              url: "ToGroupedDrugUnits/{action}/{id}",
              defaults: new { controller = "ToGroupedDrugUnits", action = "GroupedDrugUnits", id = UrlParameter.Optional }
              );
            routes.MapRoute(
              name: "DepotInventory",
              url: "DepotInventory/{action}/{id}",
              defaults: new { controller = "DepotInventory", action = "Index", id = UrlParameter.Optional }
              );
        }
    }
}
