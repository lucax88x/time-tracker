using System;
using System.Threading.Tasks;
using TimeTracker.Infra.Read.Core.Ioc;
using TimeTracker.Test.Common;
using TimeTracker.Test.Infra.Common;
using Xunit;

namespace TimeTracker.Infra.Read.Core.Test
{
    public class ReadRepositoryTest: IDisposable
    {
        private readonly Sandbox _sandbox;
        private readonly IReadRepository _sut;

        public ReadRepositoryTest()
        {
            var configBuilder = new ConfigBuilder();

            _sandbox = new Sandbox(configBuilder.BuildModule(), new Module());

            _sut = _sandbox.Resolve<IReadRepository>();
        }
        
        [Fact]
        public async Task should_set_value()
        {
            // WHEN            
            await _sut.Set("key", new SampleObject());
        }

        public void Dispose()
        {
            _sandbox?.Dispose();
        }
    }
}