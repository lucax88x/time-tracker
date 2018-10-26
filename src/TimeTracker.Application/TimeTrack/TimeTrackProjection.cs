using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeTracker.Application.TimeTrack.Query;
using TimeTracker.Domain.TimeTrack.Events;
using TimeTracker.Infra.Read.TimeTrack;

namespace TimeTracker.Application.TimeTrack
{
    public class TimeTrackProjection : 
        INotificationHandler<TimeTracked>,
        IRequestHandler<GetTimeTrackById, TimeTrackReadDto>
    {
        private readonly ITimeTrackReadRepository _timeTrackReadRepository;

        public TimeTrackProjection(ITimeTrackReadRepository timeTrackReadRepository)
        {
            _timeTrackReadRepository = timeTrackReadRepository;
        }

        public async Task Handle(TimeTracked notification, CancellationToken cancellationToken)
        {
            await _timeTrackReadRepository.Add(notification.Id, notification.When);
        }

        public async Task<TimeTrackReadDto> Handle(GetTimeTrackById request, CancellationToken cancellationToken)
        {
            return await _timeTrackReadRepository.GetById(request.Id);
        }
    }
}