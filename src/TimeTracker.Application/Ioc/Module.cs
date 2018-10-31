using System.Reflection;
using Autofac;
using AutofacSerilogIntegration;
using MediatR;
using FluentValidation;
using MediatR.Pipeline;
using TimeTracker.Application.Behaviors;

namespace TimeTracker.Application.Ioc
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new TimeTracker.Utils.Ioc.Module());
            builder.RegisterModule(new TimeTracker.Infra.Write.Ioc.Module());
            builder.RegisterModule(new TimeTracker.Infra.Read.Ioc.Module());

            builder.RegisterLogger();

            RegisterMediatr(builder);
        }

        private void RegisterMediatr(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>)
            };

            var assembly = typeof(Module).GetTypeInfo().Assembly;

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }


            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IValidator<>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}