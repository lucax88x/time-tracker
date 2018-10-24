using Autofac;

namespace TimeTracker.Infra.Write.Migrations.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new TimeTracker.Infra.Write.Core.Ioc.Module());
            
            builder.RegisterType<Migrator>()
                .AsSelf()
                .SingleInstance();
        }
    }
}