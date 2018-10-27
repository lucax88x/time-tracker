using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Core;
using TimeTracker.Core.Exceptions.Technical;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Infra.Write
{
    public interface IWriteRepository
    {
        Task<ImmutableList<Event>> Save<T>(T aggregate) where T : AggregateRoot;
        Task<bool> Exists(Guid id);
        Task<T> Get<T>(Guid id) where T : AggregateRoot, new();
    }

    public class WriteRepository : IWriteRepository
    {
        private readonly IEventStore _eventStore;

        public WriteRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<ImmutableList<Event>> Save<T>(T aggregate) where T : AggregateRoot
        {
            var events = aggregate.GetUncommittedChanges();

            if (!events.Any()) throw new ConcurrencyException();

            await _eventStore.Save(aggregate.Id, events, aggregate.Version);

            aggregate.MarkChangesAsCommitted();

            return events;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _eventStore.Exists(id);
        }        
        
        public async Task<T> Get<T>(Guid id) where T : AggregateRoot, new()
        {
            var (version, events) = await _eventStore.GetById(id);

            var aggregateRoot = new T();

            aggregateRoot.LoadsFromHistory(events, version);

            return aggregateRoot;
        }
    }
}