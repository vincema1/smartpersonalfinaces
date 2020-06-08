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

            var container= builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //config.DependencyResolver = new AutofacDependencyResolver(container);


        }
    }
}
