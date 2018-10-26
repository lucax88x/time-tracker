using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Cassandra;
using MediatR;
using TimeTracker.Infra.Write.Core;
using TimeTracker.Infra.Write.Migrations;
using TimeTracker.Test.Infra.Common.FluentAssertion;
using TimeTracker.Test.Infra.Common.Mediator;

namespace TimeTracker.Test.Infra.Common
{
    public class Sandbox : IDisposable
    {
        private readonly IContainer _container;
        private MediatorSniffer _mediatorSniffer;
        private Migrator _migrator;
        private ISession _connection;
        private string _keyspace;

        public FluentSandboxAssertion Should { get; private set; }
        public IMediator Mediator { get; set; }

        public Sandbox(params Module[] modules)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new TimeTracker.Infra.Write.Migrations.Ioc.Module());

            foreach (var module in modules)
                builder.RegisterModule(module);

            RegisterMediatorSniffers(builder);

            _container = builder.Build();

            ResolveMediator();
            ResolveCassandra();

            CreateKeyspace();

            BuilderFluentAssertions();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void ClearMediator()
        {
            _mediatorSniffer.Clear();
        }

        public void Dispose()
        {
            DeleteKeyspace();
            
            _mediatorSniffer?.Dispose();
            _container?.Dispose();
        }

        private static void RegisterMediatorSniffers(ContainerBuilder builder)
        {
            builder.RegisterType<MediatorSnifferInterceptor>();
            builder.RegisterType<MediatorSniffer>().AsSelf().SingleInstance();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder
                .RegisterType<MediatR.Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(MediatorSnifferInterceptor));
        }

        private void BuilderFluentAssertions()
        {
            var mediator = new FluentMediatorAssertion(_mediatorSniffer);
            Should = new FluentSandboxAssertion(mediator);
        }

        private void ResolveMediator()
        {
            Mediator = _container.Resolve<IMediator>();
            _mediatorSniffer = _container.Resolve<MediatorSniffer>();
        }

        private void ResolveCassandra()
        {
            _keyspace = Guid.NewGuid().ToString().Replace("-", "_");
            var connectionFactory = _container.Resolve<IConnectionFactory>();
            _migrator = _container.Resolve<Migrator>();
            _connection = connectionFactory.Connect();
        }

        private void CreateKeyspace()
        {
            _connection.CreateKeyspace(_keyspace, durableWrites: false);
            _connection.ChangeKeyspace(_keyspace);
            _migrator.Up();
        }

        private void DeleteKeyspace()
        {
            _connection.DeleteKeyspace(_keyspace);
        }
    }
}