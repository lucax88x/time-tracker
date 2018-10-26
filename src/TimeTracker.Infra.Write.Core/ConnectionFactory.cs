using Cassandra;

namespace TimeTracker.Infra.Write.Core
{
    public interface IWriteConnectionFactory
    {
        ISession Connect();
    }

    public class WriteConnectionFactory : IWriteConnectionFactory
    {
        private readonly Config.Cassandra _cassandra;

        public WriteConnectionFactory(Config.Cassandra cassandra)
        {
            _cassandra = cassandra;
        }

        // TODO: check single instance etc
        public ISession Connect()
        {
            var cluster = Cluster.Builder()
                .AddContactPoint(_cassandra.ContactPoint)                
                .WithDefaultKeyspace(_cassandra.Keyspace)
                .Build();

            return cluster.ConnectAndCreateDefaultKeyspaceIfNotExists();
        }
    }
}