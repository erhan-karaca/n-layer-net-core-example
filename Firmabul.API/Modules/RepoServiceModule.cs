using System.Reflection;
using Autofac;
using Firmabul.Caching;
using Firmabul.Core.Repositories;
using Firmabul.Core.Services;
using Firmabul.Core.UnitOfWorks;
using Firmabul.Repository;
using Firmabul.Repository.Repositories;
using Firmabul.Repository.UnitOfWorks;
using Firmabul.Service.Mapping;
using Firmabul.Service.Services;
using Module = Autofac.Module;

namespace Firmabul.API.Modules;

public class RepoServiceModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>))
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

        var apiAssembly = Assembly.GetExecutingAssembly();
        var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
        var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
        
        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

        builder.RegisterType<CompanyServiceWithCaching>().As<ICompanyService>();

    }
}