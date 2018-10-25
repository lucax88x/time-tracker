using System;
using TimeTracker.Core.Interfaces;
using TimeTracker.Infra.Read.TimeTrack;

namespace TimeTracker.Application.TimeTrack.Query
{
    public class GetTimeTrackById : Query<TimeTrackReadDto>
    {
        public Guid Id { get; }

        public GetTimeTrackById(Guid id)
        {
            Id = id;
        }
    }
}