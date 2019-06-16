using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StationeryWebApp.Startup))]
namespace StationeryWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
