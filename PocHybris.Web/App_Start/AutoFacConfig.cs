using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Autofac;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using PocHybris.Data.Infrastructure;
using PocHybris.Data.Repository;
using PocHybris.Services;
using PocHybris.Services.IServices;

namespace PocHybris.Web.App_Start
{
    public static class AutoFacConfig
    {
        public static void ConfigureContainer()
        {
            // Configure AutoFac (IOC)

            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();

            // Inject Repositories
            builder.RegisterAssemblyTypes(typeof(ProductCatalogRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerRequest();

            // Inject Services
            builder.RegisterType<ProductCatalogService>().As<IProductCatalogService>().InstancePerRequest();

            Autofac.IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
