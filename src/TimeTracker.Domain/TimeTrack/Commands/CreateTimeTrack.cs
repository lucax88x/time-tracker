using System;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Domain.TimeTrack.Commands
{
    public class CreateTimeTrack : Command
    {
        public Guid Id { get; }
        public DateTimeOffset When { get; }

        public CreateTimeTrack(DateTimeOffset when, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            When = when;
        }
    }
}