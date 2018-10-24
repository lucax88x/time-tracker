using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Core;
using TimeTracker.Core.Exceptions.Technical;

namespace TimeTracker.Infra.Write
{
    public interface IEventStore
    {
        Task Save<T>(T aggregate) where T : AggregateRoot;
        Task<T> GetById<T>(Guid id) where T : AggregateRoot, new();
    }

    public class EventStore : IEventStore
    {
        private readonly IWriteRepository _writeRepository;

        public EventStore(IWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task Save<T>(T aggregate) where T : AggregateRoot
        {
            var events = aggregate.GetUncommittedChanges();

            if (!events.Any()) throw new ConcurrencyException();

            await _writeRepository.Save(aggregate.Id, events, aggregate.Version);
            
            aggregate.MarkChangesAsCommitted();
        }

        public async Task<T> GetById<T>(Guid id) where T : AggregateRoot, new()
        {
            var (version, events) = await _writeRepository.GetById(id);

            var aggregateRoot = new T();

            aggregateRoot.LoadsFromHistory(events, version);

            return aggregateRoot;
        }
    }
}