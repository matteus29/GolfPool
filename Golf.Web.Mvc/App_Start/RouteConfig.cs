﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Golf.Web.Mvc {
  public class RouteConfig {
    public static void RegisterRoutes(RouteCollection routes) {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


      routes.MapRoute(
          name: "Account",
          url: "Account/{action}",
          defaults: new { controller = "Account", action = "Index" }
      );

      //routes.MapRoute(
      //    name: "Simple",
      //    url: "{controller}/{id}",
      //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
      //);


      routes.MapRoute(
          name: "Default",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
      );

    }
  }
}