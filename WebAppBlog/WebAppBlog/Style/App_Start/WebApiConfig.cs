using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAppBlog.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {

            configuration.MapHttpAttributeRoutes();


            configuration.Routes.MapHttpRoute(
             name: "DefaultApi",
             routeTemplate: "api/{controller}/{id}",
             defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}