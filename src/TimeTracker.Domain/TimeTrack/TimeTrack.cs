using System;
using TimeTracker.Core;
using TimeTracker.Core.Interfaces;
using TimeTracker.Domain.TimeTrack.Events;

namespace TimeTracker.Domain.TimeTrack
{
    public class TimeTrack : AggregateRoot
    {
        public DateTimeOffset When { get; private set; }

        protected override void Apply(Event @event)
        {
            switch (@event)
            {
                case TimeTracked timeTracked:
                {
                    Id = timeTracked.Id;
                    When = timeTracked.When;
                    return;
                }
            }
        }

        public static TimeTrack Create(Guid id, DateTimeOffset when)
        {
            var instance = new TimeTrack();

            instance.ApplyChange(new TimeTracked(id, when));

            return instance;
        }
    }
}