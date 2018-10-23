using System;
using TimeTracker.Application.Interfaces;

namespace TimeTracker.Application.TimeTrack.Commands
{
    public class TrackTime : Command
    {
        public TrackTime(DateTimeOffset @when)
        {
            When = when;
        }

        public DateTimeOffset When { get; }
    }
}