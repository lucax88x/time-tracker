using System;
using Autofac;

namespace TimeTracker.Test.Common
{
    public class AutofacSandbox : IDisposable
    {
        private readonly IContainer _container;

        public AutofacSandbox(params Module[] modules)
        {
            var builder = new ContainerBuilder();

            foreach (var module in modules)
                builder.RegisterModule(module);

            _container = builder.Build();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}