using System;

namespace TimeTracker.Infra.Read.TimeTrack
{
    public class TimeTrackReadDto
    {
        public Guid Id { get; }
        public DateTimeOffset When { get; }
        public int Type { get; }

        public TimeTrackReadDto(Guid id, DateTimeOffset when, int type)
        {
            Id = id;
            When = when;
            Type = type;
        }
    }
}