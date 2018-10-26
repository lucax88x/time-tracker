using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.KeyspaceIsolation;
using TimeTracker.Core.Exceptions.Technical;
using TimeTracker.Utils;

namespace TimeTracker.Infra.Read.Core
{
    public interface IReadRepository
    {
        Task Set<T>(RedisKey key, T obj, string path = ".", CommandFlags flags = CommandFlags.None);
        Task<T> Get<T>(RedisKey key, string path = ".", CommandFlags flags = CommandFlags.None);
    }

    public class ReadRepository : IReadRepository
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly ISerializer _serializer;
        private readonly IDatabase _database;
        private readonly string _prefix;

        public ReadRepository(IConnectionMultiplexer connection, ISerializer serializer, int database, string prefix)
        {
            _connection = connection;
            _serializer = serializer;
            _prefix = prefix;

            _database = _connection.GetDatabase(database).WithKeyPrefix(_prefix);
        }

        public async Task Set<T>(RedisKey key, T obj, string path = ".", CommandFlags flags = CommandFlags.None)
        {
            var json = _serializer.Serialize(obj);
            
            var value = await _database.ExecuteAsync("JSON.SET", new object[] {key, path, json}, flags);

            if (value.IsNull) throw new NotFoundItemException();
        }
        
        public async Task<T> Get<T>(RedisKey key, string path = ".", CommandFlags flags = CommandFlags.None)
        {
            var value = await _database.ExecuteAsync("JSON.GET", new object[] {key, path}, flags);

            if (value.IsNull) throw new NotFoundItemException();

            return _serializer.Deserialize<T>(Encoding.UTF8.GetString((byte[])value));
        }
    }
}