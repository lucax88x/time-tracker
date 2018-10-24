using TimeTracker.Infra.Write.Core;

namespace TimeTracker.Infra.Write.Migrations
{
    public class Migrator
    {
        private readonly IConnectionFactory _connectionFactory;

        public Migrator(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Up()
        {
            using (var session = _connectionFactory.Connect())
            {
                var dropStatement = session.Prepare("DROP TABLE IF EXISTS es.event");
                session.Execute(dropStatement.Bind());
                                
                var statement =
                    session.Prepare(
                        "CREATE TABLE es.event (id UUID, version INT, type TEXT, payload TEXT, PRIMARY KEY (id, version)) WITH CLUSTERING ORDER BY (version ASC)");
                session.Execute(statement.Bind());
            }
        }
    }
}