using Cassandra;

namespace TimeTracker.Infra.Write.Core
{
    public interface IConnectionFactory
    {
        ISession Connect();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        private readonly Config.Cassandra _cassandra;

        public ConnectionFactory(Config.Cassandra cassandra)
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