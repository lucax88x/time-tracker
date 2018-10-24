using System;
using TimeTracker.Test.Common;
using Xunit;

namespace TimeTracker.Infra.Write.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(new Config.Ioc.Module(configBuilder.Build()), new Infra.Write.Ioc.Module());
        }
        
        [Fact]
        public void should_resolve_IWriteRepository()
        {
            _scopeResolver.IsSingleInstance<IWriteRepository, WriteRepository>();
        }
        
        [Fact]
        public void should_resolve_IEventStore()
        {
            _scopeResolver.IsSingleInstance<IEventStore, EventStore>();
        }
        
        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}