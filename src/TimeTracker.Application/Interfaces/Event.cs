namespace TimeTracker.Application.Interfaces
{
    public abstract class Event: INotificationType
    {
        public EventTypes EventType => EventTypes.Event;
    }
}