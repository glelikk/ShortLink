using System.Configuration;
using ShortLink.Application.Preferences;
using ShortLink.Application.Services;
using SimpleInjector;

namespace ShortLink.Modules
{
    public static class ApplicationModule
    {
        public static void Load(Container container)
        {
            container.Register<IUniqueIdGenerator>(UniqueIdGenerator.GetInstance, Lifestyle.Singleton);

            var currentDomain = ConfigurationManager.AppSettings["CurrentDomain"];

            container.Register<LinkServicePreferences>(() => new LinkServicePreferences {CurrentDomain = currentDomain}, Lifestyle.Scoped);
            container.Register<ILinkService, LinkService>(Lifestyle.Scoped);
        }
    }
}