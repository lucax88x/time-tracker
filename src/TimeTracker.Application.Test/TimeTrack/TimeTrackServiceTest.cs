using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using TimeTracker.Application.Ioc;
using TimeTracker.Application.TimeTrack.Commands;
using TimeTracker.Domain.TimeTrack;
using TimeTracker.Infra.Read.TimeTrack;
using TimeTracker.Test.Common;
using TimeTracker.Test.Infra.Common;
using Xunit;

namespace TimeTracker.Application.Test.TimeTrack
{
    [Trait("Type", "Integration")]
    [Trait("Category", "Database")]
    public class TimeTrackServiceTest : IDisposable
    {
        private readonly Sandbox _sandbox;

        public TimeTrackServiceTest()
        {
            var configBuilder = new ConfigBuilder();

            _sandbox = new Sandbox(new SandboxCassandraOptions(true, true), configBuilder.BuildModule(), new Module());
        }

        [Fact]
        public async Task should_not_allow_to_track_with_invalid_type()
        {
            // GIVEN
            var command = new CreateTimeTrack(DateTimeOffset.UtcNow, 0);

            // WHEN           
            Func<Task> action = async () => await _sandbox.Mediator.Send(command);

            // THEN
            action.Should().Throw<ValidationException>();
            _sandbox.Should.Mediator.Be("CreateTimeTrack");
        }

        [Fact]
        public async Task should_track_time()
        {
            // GIVEN
            var command = new CreateTimeTrack(DateTimeOffset.UtcNow, (int) TimeTrackType.In);

            // WHEN           
            var id = await _sandbox.Mediator.Send(command);

            // THEN
            _sandbox.Should.Mediator.Be("CreateTimeTrack -> TimeTracked");
            await _sandbox.Should.Cassandra.Exists(id);
            await _sandbox.Should.Redis.Exists.Set("timetrack", id);
            await _sandbox.Should.Redis.Exists.SortedSet<TimeTrackReadDto>("timetrack", "by-when", 1);
        }

        public void Dispose()
        {
            _sandbox?.Dispose();
        }
    }
}