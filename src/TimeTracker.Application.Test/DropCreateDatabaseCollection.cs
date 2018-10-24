using TimeTracker.Test.Infra.Common.Cassandra;
using Xunit;

namespace TimeTracker.Application.Test
{
    [CollectionDefinition("DropCreateDatabase Collection")]
    public class DropCreateDatabaseCollection : ICollectionFixture<DropCreateDatabase>
    {
    }
}