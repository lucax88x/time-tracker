using System;
using System.Threading.Tasks;
using FluentAssertions;
using TimeTracker.Application.Ioc;
using TimeTracker.Application.TimeTrack.Query;
using TimeTracker.Core;
using TimeTracker.Domain.TimeTrack.Commands;
using TimeTracker.Domain.TimeTrack.Events;
using TimeTracker.Test.Common;
using TimeTracker.Test.Infra.Common;
using Xunit;

namespace TimeTracker.Application.Test.TimeTrack
{
    [Trait("Type", "Integration")]
    [Trait("Category", "Redis")]
    public class TimeTrackProjectionTest : IDisposable
    {
        private readonly Sandbox _sandbox;

        public TimeTrackProjectionTest()
        {
            var configBuilder = new ConfigBuilder();

            _sandbox = new Sandbox(new SandboxCassandraOptions(false), configBuilder.BuildModule(), new Module());
        }

        [Fact]
        public async Task should_add_when_we_get_TimeTracked()
        {
            // GIVEN
            var id = Guid.NewGuid();
            var evt = new TimeTracked(id, DateTimeOffset.UtcNow);

            // WHEN           
            await _sandbox.Mediator.Publish(evt);

            // THEN
            _sandbox.Should.Mediator.Be("TimeTracked");
            await _sandbox.Should.Redis.Exists("timetrack", id);
        }

        [Fact]
        public async Task should_get_by_id()
        {
            // TODO: start adding a scenario for adding some data
            // GIVEN
            var id = Guid.NewGuid();
            var when = DateTimeOffset.UtcNow;
            
            var query = new GetTimeTrackById(id);

            // WHEN           
            var dto = await _sandbox.Mediator.Send(query);

            // THEN
            _sandbox.Should.Mediator.Be("GetTimeTrackById");
            
            dto.Id.Should().Be(id);
            dto.When.Should().Equals(when);
        }

        public void Dispose()
        {
            _sandbox?.Dispose();
        }
    }
}