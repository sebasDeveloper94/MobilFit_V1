using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MobilFit.Backend.Startup))]
namespace MobilFit.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
