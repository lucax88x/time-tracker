using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeTracker.Domain.TimeTrack.Commands;
using TimeTracker.Infra.Write;

namespace TimeTracker.Application.TimeTrack
{
    public class TimeTrackService : IRequestHandler<TrackTime>
    {
        private readonly IEventStore _eventStore;

        public TimeTrackService(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(TrackTime request, CancellationToken cancellationToken)
        {
            var trackTime = new Domain.TimeTrack.TimeTrack(request.Id, request.When);

            await _eventStore.Save(trackTime);

            return Unit.Value;
        }
    }
}