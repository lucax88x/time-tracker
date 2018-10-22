using System;

namespace TimeTracker.Domain.Exceptions
{
    public class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }
}