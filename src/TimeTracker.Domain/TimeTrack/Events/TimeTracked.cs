using System;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Domain.TimeTrack.Events
{
    public class TimeTracked : Event
    {
        public DateTimeOffset When { get; }
        public TimeTrackType Type { get; }

        public TimeTracked(Guid id, DateTimeOffset when, TimeTrackType type) : base(id)
        {
            When = when;
            Type = type;
        }
    }
}