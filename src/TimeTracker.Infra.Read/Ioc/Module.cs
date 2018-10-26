using Autofac;
using TimeTracker.Infra.Read.TimeTrack;

namespace TimeTracker.Infra.Read.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new Core.Ioc.Module());

            builder.RegisterType<TimeTrackReadRepository>()
                .As<ITimeTrackReadRepository>()
                .SingleInstance();
        }
    }
}