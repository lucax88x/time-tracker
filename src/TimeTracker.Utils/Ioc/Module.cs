using Autofac;

namespace TimeTracker.Utils.Ioc
{
    public class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CaseConverter>()
                .As<ICaseConverter>()
                .SingleInstance();            
            
            builder.RegisterType<Serializer>()
                .As<ISerializer>()
                .SingleInstance();
        }
    }
}