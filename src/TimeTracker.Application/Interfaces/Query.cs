﻿namespace TimeTracker.Application.Interfaces
{
    public abstract class Query<T> : IRequestType<T>
    {
        public EventTypes EventType => EventTypes.Query;
    }
}