using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DSLNG.PEAR.Web.Startup))]
namespace DSLNG.PEAR.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
