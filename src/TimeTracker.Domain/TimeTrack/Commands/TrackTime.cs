using System;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Domain.TimeTrack.Commands
{
    public class TrackTime : Command
    {
        public Guid Id { get; }
        public DateTimeOffset When { get; }

        public TrackTime(DateTimeOffset when)
        {
            Id = Id == Guid.Empty ? Guid.NewGuid() : Id;
            When = when;
        }
    }
}