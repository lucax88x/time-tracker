using System;
using TimeTracker.Test.Common;
using Xunit;

namespace TimeTracker.Infra.Read.Core.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(new Config.Ioc.Module(configBuilder.Build()), new Infra.Read.Core.Ioc.Module());
        }
        
        [Fact]
        public void should_resolve_IReadConnectionFactory()
        {
            _scopeResolver.IsSingleInstance<IReadConnectionFactory, ReadConnectionFactory>();
        }
        
        [Fact]
        public void should_resolve_ReadRepositoryFactory()
        {
            _scopeResolver.IsSingleInstance<ReadRepositoryFactory>();
        }
        
        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}