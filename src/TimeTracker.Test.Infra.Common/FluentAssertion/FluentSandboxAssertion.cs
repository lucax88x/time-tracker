namespace TimeTracker.Test.Infra.Common.FluentAssertion
{
    public class FluentSandboxAssertion
    {
        public FluentMediatorAssertion Mediator { get; }

        public FluentSandboxAssertion(FluentMediatorAssertion mediator)
        {
            Mediator = mediator;
        }
    }
}