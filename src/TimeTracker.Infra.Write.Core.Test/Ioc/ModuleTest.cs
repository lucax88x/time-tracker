using System;
using TimeTracker.Config.Ioc;
using TimeTracker.Test.Common;
using Xunit;

namespace TimeTracker.Infra.Write.Core.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(new Module(configBuilder.Build()), new Core.Ioc.Module());
        }

        [Fact]
        public void should_resolve_IConnectionFactory()
        {
            _scopeResolver.IsSingleInstance<IConnectionFactory, ConnectionFactory>();
        }        
        
        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}