using System;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using ShortLink.Application.Exceptions;
using ShortLink.Filters;

namespace ShortLink
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Filters.Add(new WebApiExceptionFilter(typeof(InvalidArgumentException), HttpStatusCode.BadRequest, config.Formatters.JsonFormatter));
        }
    }
}
