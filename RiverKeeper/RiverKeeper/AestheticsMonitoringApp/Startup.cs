using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AestheticsMonitoringApp.Startup))]
namespace AestheticsMonitoringApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
