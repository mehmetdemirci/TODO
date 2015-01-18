using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MD.TODO.Web.Startup))]
namespace MD.TODO.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
