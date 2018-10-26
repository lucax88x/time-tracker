using System;
using System.Threading.Tasks;
using TimeTracker.Application.Ioc;
using TimeTracker.Core;
using TimeTracker.Domain.TimeTrack.Commands;
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

            _sandbox = new Sandbox(configBuilder.BuildModule(), new Module());
        }

        [Fact]
        public async Task should_track_time()
        {            
            // found a way to reset the db at migrations state (transaction rollback etc?)
            
            // GIVEN
            var command = new TrackTime(DateTimeOffset.UtcNow);

            // WHEN           
            await _sandbox.Mediator.Send(command);
            
            // THEN
            _sandbox.Should.Mediator.Be("TrackTime -> TimeTracked");
        }

        public void Dispose()
        {
            _sandbox?.Dispose();
        }
    }
}