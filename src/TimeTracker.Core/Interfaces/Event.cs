﻿using System;

namespace TimeTracker.Core.Interfaces
{
    public abstract class Event : INotificationType
    {
        public EventTypes EventType => EventTypes.Event;

        public Guid Id { get; }

        protected Event(Guid id)
        {
            Id = id;
        }
    }
}