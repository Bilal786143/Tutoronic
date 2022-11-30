using Autofac;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tutoronic.IOC;
using Tutoronic.Models;
using Tutoronic.Services.Implementation;
using Tutoronic.Services.Interface;

namespace Tutoronic
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacSetup.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class DataModule : Module
    {
        private string connStr;
        public DataModule(string connString)
        {
            this.connStr = connString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new Model1(this.connStr)).InstancePerLifetimeScope();
            builder.RegisterType<Home>().As<IHome>().InstancePerLifetimeScope();
            builder.RegisterType<Students>().As<IStudents>().InstancePerLifetimeScope();
            builder.RegisterType<Teachers>().As<ITeachers>().InstancePerLifetimeScope();
            builder.RegisterType<AdminService>().As<IAdminService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<SubCategoryService>().As<ISubCategoryService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
