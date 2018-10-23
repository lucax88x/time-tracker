using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using MediatR;
using TimeTracker.Test.Infra.Common.FluentAssertion;
using TimeTracker.Test.Infra.Common.Mediator;

namespace TimeTracker.Test.Infra.Common
{
    public class Sandbox : IDisposable
    {
        private readonly IContainer _container;
        private readonly MediatorSniffer _mediatorSniffer;

        public FluentSandboxAssertion Should { get; set; }
        public IMediator Mediator { get; }

        public Sandbox(params Module[] modules)
        {
            var builder = new ContainerBuilder();

            foreach (var module in modules)
                builder.RegisterModule(module);

            RegisterMediatorSniffers(builder);

            _container = builder.Build();

            Mediator = _container.Resolve<IMediator>();
            _mediatorSniffer = _container.Resolve<MediatorSniffer>();

            BuilderFluentAssertions();
        }

        private static void RegisterMediatorSniffers(ContainerBuilder builder)
        {
            builder.RegisterType<MediatorSnifferInterceptor>();
            builder.RegisterType<MediatorSniffer>().AsSelf().SingleInstance();
            builder.RegisterGeneric(typeof(MediatorSnifferBehavior<,>)).AsImplementedInterfaces();

            builder
                .RegisterType<MediatR.Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(MediatorSnifferInterceptor));
        }

        private void BuilderFluentAssertions()
        {
            var mediator = new FluentMediatorAssertion(_mediatorSniffer);
            Should = new FluentSandboxAssertion(mediator);
        }

        public void Dispose()
        {
            _mediatorSniffer?.Dispose();
            _container?.Dispose();
        }

        public void ClearMediator()
        {
            _mediatorSniffer.Clear();
        }
    }
}