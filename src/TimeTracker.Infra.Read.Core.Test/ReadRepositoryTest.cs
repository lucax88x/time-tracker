using System;
using System.Threading.Tasks;
using FluentAssertions;
using TimeTracker.Core.Exceptions.Technical;
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

            _sandbox = new Sandbox(new SandboxCassandraOptions(false), configBuilder.BuildModule(), new Module());

            _sut = _sandbox.Resolve<ReadRepositoryFactory>().Build("read");
        }
        
        [Fact]
        public async Task should_get_not_found_when_no_item_found()
        {
            // WHEN            
            Func<Task> action = async () => await _sut.Get<SampleObject>("not-exists");
            
            // THEN               
            action.Should().Throw<NotFoundItemException>();
        }
        
        [Fact]
        public async Task should_get_false_when_does_not_exists()
        {
            // WHEN            
            var result = await _sut.Exists("not-exists");
            
            // THEN               
            result.Should().BeFalse();
        }
        
        [Fact]
        public async Task should_get_true_when_does_exists()
        {
            // GIVEN            
            await _sut.Set("key", new SampleObject("sample-text"));
            
            // WHEN            
            var result = await _sut.Exists("key");
            
            // THEN               
            result.Should().BeTrue();
        }
        
        [Fact]
        public async Task should_set_value_and_read_value()
        {
            // WHEN            
            await _sut.Set("key", new SampleObject("sample-text"));
            
            // THEN            
            var obj = await _sut.Get<SampleObject>("key");
            obj.Text.Should().Be("sample-text");
        }

        public void Dispose()
        {
            _sandbox?.Dispose();
        }
    }
}