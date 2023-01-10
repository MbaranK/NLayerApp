using Autofac;
using NLayer.Caching;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitofWork;
using NLayer.Repository;
using NLayer.Repository.Repository;
using NLayer.Repository.UnitofWork;
using NLayer.Service.Mapping;
using NLayer.Service.Service;
using System.Reflection;
using Module = Autofac.Module;

namespace NLayer.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();


            builder.RegisterType<UnitofWork>().As<IUnitofWork>();

            var apiAssembly = Assembly.GetExecutingAssembly(); //üzerinde çalıştığımız assemblyi al.
            // Diğer katmanlardan herhangi bir classı seçtik. Bu aşamada Hangi class olduğu farketmez.
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ProductServicewithCaching>().As<IProductService>();

            // InstancePerLifetimeScope => Scope
            // InstancePerDependency => transient


        }
    }
}
