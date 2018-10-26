using TimeTracker.Infra.Write.Core;

namespace TimeTracker.Infra.Write.Migrations
{
    public class Migrator
    {
        private readonly IWriteConnectionFactory _writeConnectionFactory;

        public Migrator(IWriteConnectionFactory writeConnectionFactory)
        {
            _writeConnectionFactory = writeConnectionFactory;
        }

        public void Up()
        {
            using (var session = _writeConnectionFactory.Connect())
            {
                var dropStatement = session.Prepare("DROP TABLE IF EXISTS event");
                session.Execute(dropStatement.Bind());

                var statement =
                    session.Prepare(
                        "CREATE TABLE event (id UUID, version INT, type TEXT, payload TEXT, PRIMARY KEY (id, version)) WITH CLUSTERING ORDER BY (version ASC)");
                session.Execute(statement.Bind());
            }
        }
    }
}