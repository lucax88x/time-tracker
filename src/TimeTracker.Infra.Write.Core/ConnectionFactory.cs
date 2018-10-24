using Cassandra;

namespace TimeTracker.Infra.Write.Core
{
    public interface IConnectionFactory
    {
        ISession Connect();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        // TODO: check single instance etc
        public ISession Connect()
        {
            var cluster = Cluster.Builder()
                .AddContactPoints("127.0.0.1")
                .WithDefaultKeyspace("es")
                .Build();

            return cluster.ConnectAndCreateDefaultKeyspaceIfNotExists();
        }
    }
}