using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeTracker.Application.TimeTrack.Commands;

namespace TimeTracker.Application.TimeTrack
{
    public class TimeTrackService : IRequestHandler<TrackTime>
    {
        public async Task<Unit> Handle(TrackTime request, CancellationToken cancellationToken)
        {
            await Task.FromResult(true);

            return Unit.Value;
        }
    }
}