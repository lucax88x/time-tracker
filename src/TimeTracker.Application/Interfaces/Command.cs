namespace TimeTracker.Application.Interfaces
{
    public abstract class Command : IRequestType
    {
        public EventTypes EventType => EventTypes.Command;
    }
}