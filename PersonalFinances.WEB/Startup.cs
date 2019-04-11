using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalFinances.WEB.Startup))]
namespace PersonalFinances.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
