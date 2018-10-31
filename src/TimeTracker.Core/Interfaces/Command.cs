namespace TimeTracker.Core.Interfaces
{
    public abstract class Command<T> : IRequestType<T>
    {
        public EventTypes EventType => EventTypes.Command;
    }
}