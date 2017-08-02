using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BarbaraDoces.Startup))]
namespace BarbaraDoces
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
