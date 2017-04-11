using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ShortLink.Startup))]
namespace ShortLink
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}