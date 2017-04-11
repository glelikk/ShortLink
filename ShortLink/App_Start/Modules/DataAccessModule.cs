using System.Data.Entity;
using ShortLink.DataAccess;
using ShortLink.DataAccess.Repositories;
using SimpleInjector;

namespace ShortLink.Modules
{
    public static class DataAccessModule
    {
        public static void Load(Container container)
        {
            container.Register<DbContext>(() => new LinkDataContext(), Lifestyle.Scoped);
            container.Register<ILinkRepository, LinkRepository>(Lifestyle.Scoped);
            container.Register<IClientRepository, ClientRepository>(Lifestyle.Scoped);
            container.Register<IClickRepository, ClickRepository>(Lifestyle.Scoped);
        }
    }
}