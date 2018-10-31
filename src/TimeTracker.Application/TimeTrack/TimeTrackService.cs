using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeTracker.Domain.TimeTrack;
using TimeTracker.Application.TimeTrack.Commands;
using TimeTracker.Infra.Write;

namespace TimeTracker.Application.TimeTrack
{
    public class TimeTrackService : IRequestHandler<CreateTimeTrack, Guid>
    {
        private readonly IWriteRepository _writeRepository;
        private readonly IMediator _mediator;

        public TimeTrackService(IMediator mediator, IWriteRepository writeRepository)
        {
            _mediator = mediator;
            _writeRepository = writeRepository;
        }

        public async Task<Guid> Handle(CreateTimeTrack request, CancellationToken cancellationToken)
        {
            var trackTime = Domain.TimeTrack.TimeTrack.Create(request.Id, request.When, (TimeTrackType)request.Type);

            var events = await _writeRepository.Save(trackTime);

            foreach(var evt in events) await _mediator.Publish(evt, cancellationToken);

            return trackTime.Id;
        }
    }
}