﻿using System;
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
    public interface IWriteRepository
    {
        Task Save(Guid aggregateId, IReadOnlyCollection<Event> events, int expectedVersion);
        Task<(int Version, IReadOnlyCollection<Event> Events)> GetById(Guid aggregateId);
    }

    public class WriteRepository : IWriteRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ISerializer _serializer;

        public WriteRepository(IConnectionFactory connectionFactory, ISerializer serializer)
        {
            _connectionFactory = connectionFactory;
            _serializer = serializer;
        }

        public async Task Save(Guid aggregateId, IReadOnlyCollection<Event> events, int expectedVersion)
        {
            using (var session = _connectionFactory.Connect())
            {
                // TODO: use something different than a integer for the version
                var lastVersion = await GetLastVersion(session, aggregateId);
                
                if (expectedVersion != -1 && lastVersion != expectedVersion)
                {
                    throw new ConcurrencyException();
                }

                var insert = await session.PrepareAsync("INSERT INTO es.event (id, version, type, payload) VALUES (?, ?, ?, ?)");

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

        public async Task<(int Version, IReadOnlyCollection<Event> Events)> GetById(Guid aggregateId)
        {
            using (var session = _connectionFactory.Connect())
            {
                var query = await session.PrepareAsync(
                    "SELECT type, payload, version FROM es.event WHERE id = ?");

                var queryResult = await session.ExecuteAsync(query.Bind(aggregateId));

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

        private async Task<int> GetLastVersion(ISession session, Guid aggregateId)
        {
            var query = await session.PrepareAsync("SELECT MAX(version) as max_version FROM es.event WHERE id = ?");

            var queryResult = await session.ExecuteAsync(query.Bind(aggregateId));

            var rows = queryResult.ToList();

            if (!rows.Any()) return -1;

            var row = rows.Single();
            
            var maxVersion = row.GetValue<int?>("max_version");

            return maxVersion ?? -1;
        }
    }
}