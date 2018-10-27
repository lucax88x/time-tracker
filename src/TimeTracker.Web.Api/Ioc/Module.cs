using System;
using Autofac;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.Extensions.Configuration;
using TimeTracker.Web.Api.GraphQL;
using TimeTracker.Web.Api.GraphQL.Types;
using TimeTracker.Web.Api.GraphQL.Types.Inputs;

namespace TimeTracker.Web.Api.Ioc
{
    public class Module : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public Module(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new Config.Ioc.Module(_configuration));
            builder.RegisterModule(new Application.Ioc.Module());

            RegisterGraphQL(builder);
        }

        private void RegisterGraphQL(ContainerBuilder builder)
        {
            builder.RegisterInstance(new DocumentExecuter()).As<IDocumentExecuter>();
            builder.RegisterInstance(new DocumentWriter()).As<IDocumentWriter>();

            builder.RegisterType<TimeTrackType>()
                .AsSelf();
            builder.RegisterType<TimeTrackInputType>()
                .AsSelf();           
            builder.RegisterType<CreateOrUpdateType>()
                .AsSelf();

            builder.RegisterType<TimeTrackQuery>()
                .AsSelf();
            builder.RegisterType<TimeTrackMutation>()
                .AsSelf();
            builder.RegisterType<TimeTrackerSchema>()
                .As<ISchema>();

            builder.Register<Func<Type, GraphType>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return t =>
                {
                    var res = context.Resolve(t);
                    return (GraphType) res;
                };
            });

            builder.Register<IDependencyResolver>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return new FuncDependencyResolver(type => context.Resolve(type));
            });
        }
    }
}