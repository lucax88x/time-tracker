using Autofac;

namespace TimeTracker.Infra.Write.Core.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WriteConnectionFactory>()
                .As<IWriteConnectionFactory>()
                .SingleInstance();
        }
    }
}