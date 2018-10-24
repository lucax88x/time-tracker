using System;
using TimeTracker.Core;
using TimeTracker.Core.Interfaces;
using TimeTracker.Domain.TimeTrack.Events;

namespace TimeTracker.Domain.TimeTrack
{
    public class TimeTrack : AggregateRoot
    {
        public DateTimeOffset When { get; private set; }

        public TimeTrack(Guid id, DateTimeOffset when)
        {            
            ApplyChange(new TimeTracked(id, when));
        }

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
    }
}