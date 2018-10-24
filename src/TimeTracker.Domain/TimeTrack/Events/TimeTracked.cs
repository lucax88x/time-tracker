using System;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Domain.TimeTrack.Events
{
    public class TimeTracked : Event
    {
        public DateTimeOffset When { get; }

        public TimeTracked(Guid id, DateTimeOffset when) : base(id)
        {
            When = when;
        }
    }
}