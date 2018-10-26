using Autofac;

namespace TimeTracker.Infra.Read.Core.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new Utils.Ioc.Module());

            builder.RegisterType<ReadConnectionFactory>()
                .As<IReadConnectionFactory>()
                .SingleInstance();

            builder.RegisterType<ReadRepositoryFactory>()
                .AsSelf()
                .SingleInstance();
        }
    }
}