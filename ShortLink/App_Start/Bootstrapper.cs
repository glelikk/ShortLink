using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Owin;
using ShortLink.Modules;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace ShortLink
{
    public static class Bootstrapper
    {
        public static void Initialize(IAppBuilder app)
        {
            DIConfiguration(app);
        }

        private static void DIConfiguration(IAppBuilder app)
        {
            var mvcContainer = new Container();
            var apiContainer = new Container();

            mvcContainer.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            apiContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            DataAccessModule.Load(apiContainer);

            ApplicationModule.Load(apiContainer);

            mvcContainer.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            apiContainer.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            mvcContainer.Verify();
            apiContainer.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(mvcContainer));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(apiContainer);
        }
    }
}