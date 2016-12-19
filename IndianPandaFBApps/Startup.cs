using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IndianPandaFBApps.Startup))]
namespace IndianPandaFBApps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
