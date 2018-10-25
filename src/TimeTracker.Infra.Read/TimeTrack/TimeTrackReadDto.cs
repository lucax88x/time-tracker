using System;

namespace TimeTracker.Infra.Read.TimeTrack
{
    public class TimeTrackReadDto
    {
        public Guid Id { get; }
        public DateTimeOffset When { get; }

        public TimeTrackReadDto(Guid id, DateTimeOffset when)
        {
            Id = id;
            When = when;
        }
    }
}