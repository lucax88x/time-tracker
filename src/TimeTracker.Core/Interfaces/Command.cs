namespace TimeTracker.Core.Interfaces
{
    public abstract class Command : IRequestType
    {
        public EventTypes EventType => EventTypes.Command;
    }
}