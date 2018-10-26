using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeTracker.Domain.TimeTrack.Commands;
using TimeTracker.Infra.Write;

namespace TimeTracker.Application.TimeTrack
{
    public class TimeTrackService : IRequestHandler<TrackTime>
    {
        private readonly IWriteRepository _writeRepository;
        private readonly IMediator _mediator;

        public TimeTrackService(IMediator mediator, IWriteRepository writeRepository)
        {
            _mediator = mediator;
            _writeRepository = writeRepository;
        }

        public async Task<Unit> Handle(TrackTime request, CancellationToken cancellationToken)
        {
            var trackTime = Domain.TimeTrack.TimeTrack.Create(request.Id, request.When);

            var events = await _writeRepository.Save(trackTime);

            foreach(var evt in events) await _mediator.Publish(evt, cancellationToken);

            return Unit.Value;
        }
    }
}