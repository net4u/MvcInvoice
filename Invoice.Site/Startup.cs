using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Invoice.Site.Startup))]
namespace Invoice.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
