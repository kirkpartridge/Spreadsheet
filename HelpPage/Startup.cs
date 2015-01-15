using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpPage.Startup))]
namespace HelpPage
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
