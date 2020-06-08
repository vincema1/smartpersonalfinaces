using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(PersonalFinances.WEB.Startup))]
namespace PersonalFinances.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var config = new HttpConfiguration();



            //config.DependencyResolver = null;


        }
    }
}
