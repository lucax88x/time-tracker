namespace TimeTracker.Test.Infra.Common.FluentAssertion
{
    public class FluentRedisAssertion
    {
        public FluentRedisAssertion(FluentRedisExistsAssertion fluentRedisExistsAssertion)
        {
            Exists = fluentRedisExistsAssertion;
        }

        public FluentRedisExistsAssertion Exists { get; }
    }
}