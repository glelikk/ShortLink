using System;
using System.Configuration;
using ShortLink.Application.Preferences;
using ShortLink.Application.Services;
using ShortLink.DataAccess;
using ShortLink.DataAccess.Repositories;
using SimpleInjector;

namespace ShortLink.Modules
{
    public static class ApplicationModule
    {
        public static void Load(Container container)
        {
            string firstId = String.Empty;
            using (var linkRepository = new LinkRepository(new LinkDataContext()))
            {
                var link = linkRepository.LastOrDefault();
                if (link != null)
                {
                    firstId = link.Id;
                }
            }
            
            container.Register<IUniqueIdGenerator>(() => new UniqueIdGenerator(firstId), Lifestyle.Singleton);

            var currentDomain = ConfigurationManager.AppSettings["CurrentDomain"];
            container.Register<LinkServicePreferences>(() => new LinkServicePreferences {CurrentDomain = currentDomain}, Lifestyle.Scoped);

            container.Register<ILinkService, LinkService>(Lifestyle.Scoped);
        }
    }
}