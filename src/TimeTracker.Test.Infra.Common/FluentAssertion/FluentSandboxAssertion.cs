namespace TimeTracker.Test.Infra.Common.FluentAssertion
{
    public class FluentSandboxAssertion
    {
        public FluentMediatorAssertion Mediator { get; set; }
        public FluentCassandraAssertion Cassandra { get; set; }
        public FluentRedisAssertion Redis { get; set; }
    }
}