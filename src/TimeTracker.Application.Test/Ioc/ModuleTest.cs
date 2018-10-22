using System;
using TimeTracker.Test.Common;

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

        public void Dispose()
        {
            _scopeResolver.Dispose();
        }
    }
}