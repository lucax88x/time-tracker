using System;
using System.Collections.Generic;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Core
{
    public abstract class AggregateRoot
    {
        private List<Event> _changes = new List<Event>();

        public Guid Id { get; protected set; }
        public int Version { get; protected set; } = -1;
        public bool HasChanges => _changes.Count != 0;

        public IReadOnlyCollection<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            if (HasChanges)
            {
                _changes = new List<Event>();
            }
        }

        public void LoadsFromHistory(IReadOnlyCollection<Event> history, int finalVersion)
        {
            foreach (var e in history) ApplyChange(e, false);

            Version = finalVersion;
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            Apply(@event);
            if (isNew) _changes.Add(@event);
        }

        protected abstract void Apply(Event @event);
    }
}