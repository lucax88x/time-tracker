using System;
using FluentValidation;
using TimeTracker.Core.Interfaces;
using TimeTracker.Domain.TimeTrack;

namespace TimeTracker.Application.TimeTrack.Commands
{
    public class CreateTimeTrack : Command<Guid>
    {
        public Guid Id { get; }
        public DateTimeOffset When { get; }
        public int Type { get; }

        public CreateTimeTrack(DateTimeOffset when, int type, Guid? id = null)
        {
            Id = !id.HasValue || id.Value == Guid.Empty ? Guid.NewGuid() : id.Value;
            When = when;
            Type = type;
        }
    }

    public class CreateTimeTrackValidator : AbstractValidator<CreateTimeTrack>
    {
        public CreateTimeTrackValidator()
        {
            RuleFor(item => item.Type).Must(BeValidType).WithMessage("Please specify a valid type");
        }

        private bool BeValidType(int type)
        {
            return Enum.IsDefined(typeof(TimeTrackType), type);
        }
    }
}