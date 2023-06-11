using Autofac;
using Data.Repository;
using Data.UnitOfWork;

namespace Service.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<IUnitOfWork>().As<UnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<ICategoryRepository>().As<CategoryRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ICategoryService>().As<CategoryService>().InstancePerLifetimeScope();
        }
    }
}