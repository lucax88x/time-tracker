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
        private readonly SandboxCassandraOptions _cassandraOptions;
        private readonly IContainer _container;
        private MediatorSniffer _mediatorSniffer;
        private Migrator _migrator;
        private ISession _connection;
        private string _keyspace;

        public FluentSandboxAssertion Should { get; private set; }
        public IMediator Mediator { get; private set; }

        public Sandbox(SandboxCassandraOptions cassandraOptions, params Module[] modules)
        {
            _cassandraOptions = cassandraOptions;

            var builder = new ContainerBuilder();

            builder.RegisterModule(new TimeTracker.Infra.Write.Migrations.Ioc.Module());
            builder.RegisterModule(new TimeTracker.Infra.Write.Ioc.Module());


            foreach (var module in modules)
                builder.RegisterModule(module);

            RegisterMediatorSniffers(builder);
            RegisterFluentAssertions(builder);

            _container = builder.Build();

            ResolveMediator();
            ResolveCassandra();
            ResolveFluentAssertions();

            SetupCassandra();
            SetupRedis();
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
            if (_cassandraOptions.Setup && _cassandraOptions.Isolate)
            {
                DeleteKeyspace();
                _connection.Dispose();
            }

            _mediatorSniffer?.Dispose();
            _container?.Dispose();
        }

        private void SetupCassandra()
        {
            if (_cassandraOptions.Setup && _cassandraOptions.Isolate)
            {
                CreateKeyspace();
            }

            if (_cassandraOptions.Setup)
            {
                _migrator.Up();
            }
        }

        private void SetupRedis()
        {
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

        private void RegisterFluentAssertions(ContainerBuilder builder)
        {
            builder.RegisterType<FluentSandboxAssertion>().AsSelf().SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<FluentMediatorAssertion>().AsSelf().SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<FluentCassandraAssertion>().AsSelf().SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<FluentRedisAssertion>().AsSelf().SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private void ResolveMediator()
        {
            Mediator = _container.Resolve<IMediator>();
            _mediatorSniffer = _container.Resolve<MediatorSniffer>();
        }

        private void ResolveCassandra()
        {
            if (_cassandraOptions.Setup)
            {
                _migrator = _container.Resolve<Migrator>();
            }

            if (_cassandraOptions.Setup && _cassandraOptions.Isolate)
            {
                var connectionFactory = _container.Resolve<IWriteConnectionFactory>();
                _keyspace = Guid.NewGuid().ToString().Replace("-", "_");
                _connection = connectionFactory.Connect();
            }
        }

        private void ResolveFluentAssertions()
        {
            Should = _container.Resolve<FluentSandboxAssertion>();
        }

        private void CreateKeyspace()
        {
            _connection.CreateKeyspace(_keyspace, durableWrites: false);
            _connection.ChangeKeyspace(_keyspace);
        }

        private void DeleteKeyspace()
        {
            _connection.DeleteKeyspace(_keyspace);
        }
    }

    public class SandboxCassandraOptions
    {
        public bool Setup { get; }
        public bool Isolate { get; }

        public SandboxCassandraOptions(bool setup, bool isolate = false)
        {
            Setup = setup;
            Isolate = isolate;
        }
    }
}