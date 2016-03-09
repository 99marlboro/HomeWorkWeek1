using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeWorkWeek1.Startup))]
namespace HomeWorkWeek1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
