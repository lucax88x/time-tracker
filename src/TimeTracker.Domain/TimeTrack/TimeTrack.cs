using System;
using TimeTracker.Core;
using TimeTracker.Core.Interfaces;
using TimeTracker.Domain.TimeTrack.Events;

namespace TimeTracker.Domain.TimeTrack
{
    public enum TimeTrackType
    {
        In = 1,
        Out = -1
    }

    public class TimeTrack : AggregateRoot
    {
        public DateTimeOffset When { get; private set; }
        public TimeTrackType Type { get; private set; }

        protected override void Apply(Event @event)
        {
            switch (@event)
            {
                case TimeTracked timeTracked:
                {
                    Id = timeTracked.Id;
                    When = timeTracked.When;
                    Type = timeTracked.Type;
                    return;
                }
            }
        }

        public static TimeTrack Create(Guid id, DateTimeOffset when, TimeTrackType type)
        {
            var instance = new TimeTrack();

            instance.ApplyChange(new TimeTracked(id, when, type));

            return instance;
        }
    }
}