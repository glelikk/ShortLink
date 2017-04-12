using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;

namespace ShortLink
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected  void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            var client =  HttpContext.Current.Request.Cookies["clientId"];
            if (client == null)
            {
                client = new HttpCookie("clientId");
                client.Value = Guid.NewGuid().ToString();
                client.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Request.Cookies.Add(client);
                HttpContext.Current.Response.Cookies.Add(client);
            }
        }

    }
}