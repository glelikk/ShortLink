using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ShortLink.Startup))]

namespace ShortLink
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Bootstrapper.Initialize(app);
        }
    }
}
