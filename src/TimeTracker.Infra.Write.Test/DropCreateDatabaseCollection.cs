using TimeTracker.Test.Infra.Common.Cassandra;
using Xunit;

namespace TimeTracker.Infra.Write.Test
{
    [CollectionDefinition("DropCreateDatabase Collection")]
    public class DropCreateDatabaseCollection : ICollectionFixture<DropCreateDatabase>
    {
    }
}