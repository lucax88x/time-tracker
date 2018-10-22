using System;
using FluentAssertions;
using TimeTracker.Config.Ioc;
using TimeTracker.Test.Common;
using Xunit;

namespace TimeTracker.Config.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(new Module(configBuilder.Build()));
        }

        [Fact]
        public void should_resolve_Cors()
        {
            _scopeResolver.IsSingleInstance<Cors>();
            _scopeResolver.Resolve<Cors>().Enabled.Should().BeTrue();
        }

        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}