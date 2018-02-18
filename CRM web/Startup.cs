using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRM_web.Startup))]
namespace CRM_web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
