using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SAT.MVC.UI.Startup))]
namespace SAT.MVC.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
