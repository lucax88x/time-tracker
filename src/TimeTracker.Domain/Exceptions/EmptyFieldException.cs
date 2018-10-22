namespace TimeTracker.Domain.Exceptions
{
    public class EmptyFieldException : DomainException
    {
        public EmptyFieldException(string fieldName) : base($"Field {fieldName} is missing")
        {
        }
    }
}