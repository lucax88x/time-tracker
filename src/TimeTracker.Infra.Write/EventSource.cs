using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using TimeTracker.Core.Exceptions.Technical;
using TimeTracker.Core.Interfaces;
using TimeTracker.Infra.Write.Core;
using TimeTracker.Utils;

namespace TimeTracker.Infra.Write
{
    public interface IEventStore
    {
        Task Save(Guid aggregateId, IReadOnlyCollection<Event> events, int expectedVersion);
        Task<bool> Exists(Guid id);
        Task<(int Version, IReadOnlyCollection<Event> Events)> GetById(Guid id);
    }

    public class EventStore : IEventStore
    {
        private readonly IWriteConnectionFactory _writeConnectionFactory;
        private readonly ISerializer _serializer;

        public EventStore(IWriteConnectionFactory writeConnectionFactory, ISerializer serializer)
        {
            _writeConnectionFactory = writeConnectionFactory;
            _serializer = serializer;
        }

        public async Task Save(Guid aggregateId, IReadOnlyCollection<Event> events, int expectedVersion)
        {
            using (var session = _writeConnectionFactory.Connect())
            {
                // TODO: use something different than a integer for the version
                var lastVersion = await GetLastVersion(session, aggregateId);
                
                if (expectedVersion != -1 && lastVersion != expectedVersion)
                {
                    throw new ConcurrencyException();
                }

                var insert = await session.PrepareAsync("INSERT INTO event (id, version, type, payload) VALUES (?, ?, ?, ?)");

                var batch = new BatchStatement();

                var increasingVersion = lastVersion;
                foreach (var evt in events)
                {
                    batch.Add(insert.Bind(
                        aggregateId,
                        ++increasingVersion,
                        evt.GetType().AssemblyQualifiedName,
                        _serializer.Serialize(evt)));
                }

                await session.ExecuteAsync(batch);
            }
        }

        public async Task<bool> Exists(Guid id)
        {
            using (var session = _writeConnectionFactory.Connect())
            {
                var query = await session.PrepareAsync(
                    "SELECT count(id) FROM event WHERE id = ?");

                var queryResult = await session.ExecuteAsync(query.Bind(id));

                var row = queryResult.SingleOrDefault();

                if (row == null) return false;

                return true;
            }
        }
        
        public async Task<(int Version, IReadOnlyCollection<Event> Events)> GetById(Guid id)
        {
            using (var session = _writeConnectionFactory.Connect())
            {
                var query = await session.PrepareAsync(
                    "SELECT type, payload, version FROM event WHERE id = ?");

                var queryResult = await session.ExecuteAsync(query.Bind(id));

                var rows = queryResult.ToList();

                if (!rows.Any()) throw new NotFoundItemException();

                var events = new List<Event>();
                var highestVersion = -1;
                foreach (var row in rows)
                {
                    var type = row.GetValue<string>("type");
                    var payload = row.GetValue<string>("payload");
                    var version = row.GetValue<int>("version");

                    var deserialized = _serializer.Deserialize(Type.GetType(type), payload);
                    if (deserialized is Event evt) events.Add(evt);

                    if (version > highestVersion) highestVersion = version;
                }

                return (highestVersion, events);
            }
        }

        private async Task<int> GetLastVersion(ISession session, Guid id)
        {
            var query = await session.PrepareAsync("SELECT MAX(version) as max_version FROM event WHERE id = ?");

            var queryResult = await session.ExecuteAsync(query.Bind(id));

            var rows = queryResult.ToList();

            if (!rows.Any()) return -1;

            var row = rows.Single();
            
            var maxVersion = row.GetValue<int?>("max_version");

            return maxVersion ?? -1;
        }
    }
}