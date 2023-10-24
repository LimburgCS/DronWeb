using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DronWeb.Startup))]
namespace DronWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }


    }
}
