using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using PersonalFinances.DATA.DataModel;
using System.Data.Entity;
using System.Web.Mvc;
using System.Reflection;
using PersonalFinances.BUSINESS.Services.Interfaces;
using PersonalFinances.BUSINESS.Services.Implementations;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PersonalFinances.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(PersonalFinances.WEB.Startup))]
namespace PersonalFinances.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var config = new HttpConfiguration();

            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()); //Register MVC Controllers
            builder.Register(c => new PersonalFinancesDBEntities("name=PersonalFinancesDBEntities")).AsSelf().As<DbContext>().InstancePerRequest();

            builder.RegisterType<RecordExtractorFromExcel>().As<IRecordsExtractor>();
            builder.RegisterType<BulkImportFileCreator>().As<IBulkImportFileCreator>();
            builder.RegisterType<RecordsImporter>().As<IRecordsImporter>();
            builder.RegisterType<ExcelImportProcessor>().As<IImportProcessor>();


        //https://stackoverflow.com/questions/24391885/how-to-plug-my-autofac-container-into-asp-net-identity-2-1
        //https://www.cnblogs.com/shiningrise/p/5573029.html

            var x = new ApplicationDbContext();
            builder.Register(c => x);
            builder.Register(c => new UserStore<ApplicationUser>(x)).AsImplementedInterfaces();
            builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>()
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("PersonalFinances")
            });
            builder.RegisterType<ApplicationUserManager>();


         //   builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();


       //     builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
       //     builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
        //    builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
         //   builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();



            //builder.Register(c => new IdentityFactoryOptions<ApplicationSignInManager>() { });
            //builder.RegisterType<ApplicationSignInManager>();



            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        //    app.UseAutofacMiddleware(container);
        

        }
    }
}
