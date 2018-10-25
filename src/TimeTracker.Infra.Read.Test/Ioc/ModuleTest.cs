using System;
using TimeTracker.Infra.Read.TimeTrack;
using TimeTracker.Test.Common;
using Xunit;

namespace TimeTracker.Infra.Read.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(new Config.Ioc.Module(configBuilder.Build()), new Infra.Read.Ioc.Module());
        }
        
        [Fact]
        public void should_resolve_ITimeTrackReadRepository()
        {
            _scopeResolver.IsSingleInstance<ITimeTrackReadRepository, TimeTrackReadRepository>();
        }
        
        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}