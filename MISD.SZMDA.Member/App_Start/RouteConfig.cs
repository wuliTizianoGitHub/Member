﻿using System.Web.Mvc;
using System.Web.Routing;

namespace MISD.SZMDA.Member
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{BackUrl}",
                defaults: new { controller = "Account", action = "Login", BackUrl = UrlParameter.Optional }
            );
        }
    }
}
