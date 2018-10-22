using Autofac;
using AutofacSerilogIntegration;

namespace TimeTracker.Application.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new TimeTracker.Utils.Ioc.Module());
            
            builder.RegisterLogger();
            
            RegisterMediatr(builder);
        }

        private void RegisterMediatr(ContainerBuilder builder)
        {
//            builder
//                .RegisterType<AuditingInterceptor>()
//                .InstancePerLifetimeScope();
//
//            builder
//                .RegisterType<Mediator>()
//                .As<IMediator>()
//                .InstancePerLifetimeScope()
//                .EnableClassInterceptors()
//                .InterceptedBy(typeof(AuditingInterceptor));

//            builder
//                .Register<SingleInstanceFactory>(ctx =>
//                {
//                    var c = ctx.Resolve<IComponentContext>();
//                    return t => c.TryResolve(t, out var o) ? o : null;
//                })
//                .InstancePerLifetimeScope();
//
//            builder
//                .Register<MultiInstanceFactory>(ctx =>
//                {
//                    var c = ctx.Resolve<IComponentContext>();
//                    return t => (IEnumerable<object>) c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
//                })
//                .InstancePerLifetimeScope();
//
//            builder.RegisterGeneric(typeof(AuditingBehavior<,>)).AsImplementedInterfaces();
//
//            builder.RegisterAssemblyTypes(typeof(Module).Assembly).AsImplementedInterfaces();
        }
    }
}