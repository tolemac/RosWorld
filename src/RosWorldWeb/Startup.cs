using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RosWorldWeb.Startup))]
namespace RosWorldWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
