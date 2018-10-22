using System;
using TimeTracker.Test.Common;
using Xunit;

namespace TimeTracker.Utils.Test.Ioc
{
    public class ModuleTest : IDisposable
    {
        private readonly ScopeResolver _scopeResolver;

        public ModuleTest()
        {
            _scopeResolver = new ScopeResolver();

            var configBuilder = new ConfigBuilder();

            _scopeResolver.BuildContainer(new Config.Ioc.Module(configBuilder.Build()), new TimeTracker.Utils.Ioc.Module());
        }

        [Fact]
        public void should_resolve_ICaseConverter()
        {
            _scopeResolver.IsSingleInstance<ICaseConverter, CaseConverter>();
        }  
        
        [Fact]
        public void should_resolve_ISerializer()
        {
            _scopeResolver.IsSingleInstance<ISerializer, Serializer>();
        }

        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}