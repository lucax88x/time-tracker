using Autofac;

namespace TimeTracker.Infra.Write.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new TimeTracker.Infra.Write.Core.Ioc.Module());
            builder.RegisterModule(new Utils.Ioc.Module());
            
            builder.RegisterType<EventStore>()
                .As<IEventStore>()
                .SingleInstance();
            
            builder.RegisterType<WriteRepository>()
                .As<IWriteRepository>()
                .SingleInstance();
        }
    }
}