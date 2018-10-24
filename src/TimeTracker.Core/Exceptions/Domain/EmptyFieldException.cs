namespace TimeTracker.Core.Exceptions.Domain
{
    public class EmptyFieldException : DomainException
    {
        public EmptyFieldException(string fieldName) : base($"Field {fieldName} is missing")
        {
        }
    }
}