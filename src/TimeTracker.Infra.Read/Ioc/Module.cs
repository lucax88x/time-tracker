using Autofac;
using TimeTracker.Infra.Read.TimeTrack;

namespace TimeTracker.Infra.Read.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            builder.RegisterType<TimeTrackReadRepository>()
                .As<ITimeTrackReadRepository>()
                .SingleInstance();
        }
    }
}