using System;

namespace TimeTracker.Core.Exceptions.Domain
{
    public class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }
}