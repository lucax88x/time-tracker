using System;
using TimeTracker.Application.TimeTrack;
using TimeTracker.Test.Common;
using Xunit;

namespace TimeTracker.Application.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(new Config.Ioc.Module(configBuilder.Build()), new Application.Ioc.Module());
        }
        
        [Fact]
        public void should_resolve_TimeTrackService()
        {
            _scopeResolver.IsSingleInstance<TimeTrackService>();
        }
        
        [Fact]
        public void should_resolve_TimeTrackProjection()
        {
            _scopeResolver.IsSingleInstance<TimeTrackProjection>();
        }

        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}